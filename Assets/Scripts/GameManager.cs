using UnityEngine;

public class GameManager : MonoBehaviour
{

    static public GameManager instance;
    [SerializeField] private float gameTimeInSec = 300.0f;

    float timeRemaining;
    public int coins;

    private void Start()
    {
        if (instance == null || instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
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
        Debug.Log("Timer Done");
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log(coins);
    }

}