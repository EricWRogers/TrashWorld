using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    public GameObject trashPrefab;

    public float timeLeft, originalTime;

    // Trash will spawn at random spots that are empty
    void Update()
    {
        // place trash at different spots over time
        timeLeft -= Time.deltaTime;
        // timeLeft = timeLeft - Time.deltaTime;
        if (timeLeft<=0)
        { 
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-8f, 8f), 5, Random.Range(-4f, 4f));
            Instantiate(trashPrefab,randomSpawnPosition,Quaternion.identity);

            // Reset time
            timeLeft = originalTime;
        }
    }
}
