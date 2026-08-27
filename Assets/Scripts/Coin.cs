using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject PlayerCapsulePrefab;

    public float rotationSpeed = 50f; // rotation speed
    public float bobbingSpeed = 0.5f;
    public float bobbingHeight = 0.5f;
    public float startY;

    private void Start()
    {
        startY = transform.position.y;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);//spinning the coin
        float newY = startY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;//bobs obove the ground
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)//destorys coin on contact
    {
       if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
