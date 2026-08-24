using UnityEngine;


public class Fire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float baseSize = 1f;
    public float growthRate = 0.5f;
    public float maxSize = 5f;

    private float currentSize;
    private float Targetgrowth;//setting a target for the growth for each interation
    private int TrashBurned = 0;
    private bool Kindling = false;

    void Start()
    {
        
        currentSize = baseSize;
        transform.localScale = new Vector3(currentSize, currentSize, currentSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Kindling)
        {
            GrowFire(growthRate * Time.deltaTime);
            if (currentSize >= Targetgrowth)
            {
            Kindling = false; //reset kindling when growth hits target
            }
        }
        if (currentSize >= maxSize)
        {
            currentSize = maxSize;
            Kindling = false; //stop growth past max size
        }
    }
    private void GrowFire(float amount)
    {
        currentSize += amount;
        transform.localScale = new Vector3(currentSize, currentSize, currentSize);
         
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            TrashBurned++;
            if(TrashBurned >= 3)//grow the fire after 3 trash objects are destroyed
            {
                Targetgrowth = Mathf.Clamp(currentSize + growthRate, baseSize, maxSize);
                Kindling = true;
                TrashBurned = 0; //reset counter
            }
            
            
            
        }
    }
}
