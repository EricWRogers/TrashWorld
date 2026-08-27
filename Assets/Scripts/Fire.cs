using UnityEngine;

public class Fire : MonoBehaviour
{
    public CoinSpawner coinSpawner;
    public GameObject CoinPrefab;
    

    private Light FireLight;
    public float fireBase = 2f;
    public float growthRate = 1f;
    public  float maxFireSize = 5f;
    private float currentFire;
    private int burnedTrash = 0;
    private float targetSize;
    private int fireLevel = 1;
    public  int maxFireLevel = 5;
    private bool Kindling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFire = fireBase;
        FireLight = GetComponentInChildren<Light>();
        FireLight.intensity = currentFire * 20f; // adjusting intensity from fire size
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
        FireLight.intensity = currentFire * 1.5f;
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
                targetSize = Mathf.Clamp((currentFire * 1.5f) + growthRate, fireBase, maxFireSize);
                burnedTrash = 0; // reset count

            }
            

            
        }
    }
}