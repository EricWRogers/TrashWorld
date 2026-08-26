using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameTimeInSec = 300.0f;

    float timeRemaining;

    private void Start()
    {
        timeRemaining = gameTimeInSec;
    }

    private void Update()
    {
        if (timeRemaining <= 0f)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            TimerFinished();
        }
    }

    private void TimerFinished()
    {
        Debug.Log("Day Done");
    }

    public void AddBackBoard()
    {
        
    }
}