using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform dropOffPoint;

    [Header("Collector Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float grabDistance = 0.5f;
    [SerializeField] private float dropDistance = 0.5f;
    [SerializeField] private float holdSpeed = 15.0f;
    [SerializeField] private float turnSpeed = 360.0f;
    [SerializeField] private float facingThreshold = 5.0f;

    private Rigidbody targetTrash;
    private Rigidbody carriedTrash;
    private Animator animator;

    private bool originalUseGravity;
    private float originalLinearDamping;
    private float originalAngularDamping;
    private RigidbodyInterpolation originalInterpolation;

    private enum CollectorState
    {
        LookingForTrash,
        TurningToTrash,
        GoingToTrash,
        TurningToDropOff,
        GoingToDropOff
    }

    private CollectorState state = CollectorState.LookingForTrash;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        switch (state)
        {
            case CollectorState.LookingForTrash:

                animator.SetBool("isWalking", false);

                if (targetTrash == null)
                {
                    FindTrash();
                }

                if (targetTrash != null)
                {
                    state = CollectorState.TurningToTrash;
                }

                break;

            case CollectorState.TurningToTrash:
                
                animator.SetBool("isWalking", false);

                if (targetTrash == null)
                {
                    state = CollectorState.LookingForTrash;
                    return;
                }

                if (TurnToTarget(targetTrash.position))
                {
                    state = CollectorState.GoingToTrash;
                }

                break;

            case CollectorState.GoingToTrash:

            animator.SetBool("isWalking", true);

                if (targetTrash == null)
                {
                    state = CollectorState.LookingForTrash;
                    return;
                }

                MoveForward();

                float trashDistance = Vector3.Distance(transform.position, targetTrash.position);

                if (trashDistance <= grabDistance)
                {
                    GrabTrash();
                    state = CollectorState.TurningToDropOff;
                }

                break;

            case CollectorState.TurningToDropOff:

                animator.SetBool("isWalking", false);

                if (dropOffPoint == null)
                    return;

                if (TurnToTarget(dropOffPoint.position))
                {
                    state = CollectorState.GoingToDropOff;
                }

                break;

            case CollectorState.GoingToDropOff:

                animator.SetBool("isWalking", true);

                if (dropOffPoint == null)
                    return;

                MoveForward();

                float dropDistanceFromPoint = Vector3.Distance(transform.position, dropOffPoint.position);

                if (dropDistanceFromPoint <= dropDistance)
                {
                    DropTrash();
                    state = CollectorState.LookingForTrash;
                }

                break;
        }
    }

    private void FixedUpdate()
    {
        if (carriedTrash != null)
        {
            MoveCarriedTrash();
        }
    }

    private void FindTrash()
    {
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");

        if (trashObjects.Length == 0) return;

        GameObject closestTrash = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject trash in trashObjects)
        {
            Rigidbody rb = trash.GetComponentInParent<Rigidbody>();

            if (rb == null)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                rb.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTrash = trash;
            }
        }

        if (closestTrash != null)
        {
            targetTrash = closestTrash.GetComponentInParent<Rigidbody>();
        }
    }
    private bool TurnToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return true;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        return angle <= facingThreshold;
    }

    private void MoveForward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void GrabTrash()
    {
        carriedTrash = targetTrash;
        targetTrash = null;

        originalUseGravity = carriedTrash.useGravity;
        originalLinearDamping = carriedTrash.linearDamping;
        originalAngularDamping = carriedTrash.angularDamping;
        originalInterpolation = carriedTrash.interpolation;


        carriedTrash.useGravity = false;
        carriedTrash.linearDamping = 10f;
        carriedTrash.angularDamping = 10f;
        carriedTrash.interpolation = RigidbodyInterpolation.Interpolate;

        carriedTrash.linearVelocity = Vector3.zero;
        carriedTrash.angularVelocity = Vector3.zero;
    }

    private void MoveCarriedTrash()
    {
        if (carriedTrash == null)
            return;

        Vector3 offset = holdPoint.position - carriedTrash.position;

        carriedTrash.linearVelocity = offset * holdSpeed;
    }

    private void DropTrash()
    {
        if (carriedTrash == null) return;
        carriedTrash.useGravity = originalUseGravity;
        carriedTrash.linearDamping = originalLinearDamping;
        carriedTrash.angularDamping = originalAngularDamping;
        carriedTrash.interpolation = originalInterpolation;

        carriedTrash.linearVelocity = Vector3.zero;
        carriedTrash.angularVelocity = Vector3.zero;

        carriedTrash = null;
    }
}