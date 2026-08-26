using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    public int coinSpawn = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Instantiate(CoinPrefab, transform.position, Quaternion.identity);
        
    }
}
