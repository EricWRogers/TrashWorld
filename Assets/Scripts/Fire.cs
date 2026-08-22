using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float baseSize = 1f;
    public float growthRate = 0.1f;
    public float maxSize = 5f;

    private float currentSize;
    private int TrashBurned = 0;

    void Start()
    {
        
        currentSize = transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void GrowFire(float amount)
    {
        currentSize += amount;
        if (currentSize > maxSize)//limit the fire size
        {
            currentSize = maxSize;
        }
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
                GrowFire(0.5f);
                TrashBurned = 0; //reset counter
            }
            
            
        }
    }
}
