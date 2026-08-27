using UnityEngine;

public class Fire : MonoBehaviour
{
    public CoinSpawner coinSpawner;
    public GameObject CoinPrefab;
    
    private float fireBase = 0.3f;
    private float growthRate = 0.5f;
    private float maxFireSize = 5f;
    private float currentFire;
    private int burnedTrash = 0;
    private float targetSize;
    private int fireLevel = 1;
    private int maxFireLevel = 10;
    private bool Kindling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFire = fireBase;
        transform.localScale = new Vector3(currentFire, currentFire, currentFire);
    }

    // Update is called once per frame
    void Update()
    {
        if (Kindling)
        {
            GrowFire(growthRate * Time.deltaTime);
            if(currentFire >= targetSize)
            {
                Kindling = false;
            }

        }
        if (currentFire >= maxFireSize)
        {
            currentFire = maxFireSize;
            Kindling = false;
        }
        
    }

    private void GrowFire(float amount)
    {
        currentFire += amount;
        transform.localScale = new Vector3(currentFire, currentFire, currentFire);
       
    }
    private void LevelUp()
    {
        fireLevel++;
        if (fireLevel <= maxFireLevel)
        {
            coinSpawner.SpawnCoins(CoinPrefab, transform.position, 5);
            
        }
        if (fireLevel == maxFireLevel)
        {
            fireLevel = maxFireLevel;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            burnedTrash++;
            if(burnedTrash == 3 && fireLevel < maxFireLevel)
            {
                LevelUp();
                Kindling = true;
                targetSize = Mathf.Clamp(currentFire + growthRate, fireBase, maxFireSize);
                burnedTrash = 0; // reset count

            }
            

            
        }
    }
}
