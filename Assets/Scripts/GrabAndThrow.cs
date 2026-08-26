using UnityEngine;
using System.Collections;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class GrabSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform holdPoint;

    [Header("Grab Settings")]
    [SerializeField] private float grabDistance = 3f;
    [SerializeField] private float holdSpeed = 15f;
    [SerializeField] private LayerMask grabbableLayers;
    [SerializeField] private float throwForce = 10f;

    bool originalUseGravity;
    float originalLinearDamping;
    float originalAngularDamping;
    RigidbodyInterpolation originalInterpolation;

    private Rigidbody grabbedObject;

    //I miss the new input system but we're using imported controls
    //so we're stuck with the old system :(
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (grabbedObject == null)
                TryGrab();
            else
                Drop();
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (grabbedObject != null)
                Throw();
        }

    }

    private void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            MoveGrabbedObject();
        }
    }

    private void TryGrab()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayers, QueryTriggerInteraction.Ignore))
        {
            Rigidbody rb = hit.collider.GetComponentInParent<Rigidbody>();

            if (rb == null)
                return;

            grabbedObject = rb;

            originalUseGravity = rb.useGravity;
            originalLinearDamping = rb.linearDamping;
            originalAngularDamping = rb.angularDamping;
            originalInterpolation = rb.interpolation;


            grabbedObject.useGravity = false;
            grabbedObject.linearDamping = 10f;
            grabbedObject.angularDamping = 10f;
            grabbedObject.interpolation = RigidbodyInterpolation.Interpolate;

        }
    }

    private void MoveGrabbedObject()
    {
        Vector3 offset = holdPoint.position - grabbedObject.position;

        grabbedObject.linearVelocity = offset * holdSpeed;
    }

    private void Drop()
    {
        if (grabbedObject == null)
            return;

        grabbedObject.useGravity = originalUseGravity;
        grabbedObject.linearDamping = originalLinearDamping;
        grabbedObject.angularDamping = originalAngularDamping;
        grabbedObject.interpolation = originalInterpolation;

        grabbedObject = null;
    }

    private void Throw()
    {
        if (grabbedObject == null)
            return;

        Rigidbody rb = grabbedObject;

        rb.useGravity = originalUseGravity;
        rb.linearDamping = originalLinearDamping;
        rb.angularDamping = originalAngularDamping;
        rb.interpolation = originalInterpolation;

        rb.linearVelocity = playerCamera.transform.forward * throwForce;

        TrailRenderer trail = grabbedObject.GetComponentInParent<TrailRenderer>();
        if (trail != null)
        {
            StartCoroutine(StopTrail(trail));
        }


        grabbedObject = null;

    }

    public void AddThrowForce()
    {
        throwForce += 5.0f;
    }

    public void AddGrabDistance()
    {
        grabDistance++;
    }

    private IEnumerator StopTrail(TrailRenderer trail)
    {
        trail.emitting = true;

        yield return new WaitForSeconds(2.5f);

        if (trail == null) yield break;

        trail.emitting = false;
    }
}