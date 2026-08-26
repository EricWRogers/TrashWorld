using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public float spawnChance;
    public GameObject trashPrefab;


    public void SpawnTrash()
    {
        Instantiate(trashPrefab, transform.position, Quaternion.identity);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnTrash();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value < spawnChance * Time.deltaTime)
        {
            SpawnTrash();
        }
    }
}
