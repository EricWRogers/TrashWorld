using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    public int coinSpawn = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
        public void SpawnCoin(GameObject CoinPrefab, Vector3 position)
    {
       
        Instantiate(CoinPrefab, position, Quaternion.identity);
        

    }
    public void SpawnCoins(GameObject CoinPrefab, Vector3 position, int coinSpawn)
    {
        float radius = 1.2f;
        for (int i = 0; i < coinSpawn; i++)
        {
            float angle = i * Mathf.PI * 2f / coinSpawn;
            Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, 1.25f, Mathf.Sin(angle) * radius);
            Vector3 spawnPosition = position + offset;
            Instantiate(CoinPrefab, spawnPosition, Quaternion.Euler(90f, 0f, 0f));
        }
        

    }
}
