using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] public float gameTimeInSec = 300.0f;
    [SerializeField] private GameObject upgradeShop;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text coinText;

    float timeRemaining;
    public int coins;
    public int moneyLevel;
    public int speedLevel;
    public int throwLevel;
    public int timerLevel;
    public bool upgradesUnlocked;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("GAME MANAGER CREATED: " + gameObject.name);
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
             Debug.LogWarning("DUPLICATE GAME MANAGER DESTROYED: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timeRemaining = gameTimeInSec;
        Debug.Log("GameManager Start - Timer: " + timeRemaining);

        if (upgradeShop != null)
        {
            upgradeShop.SetActive(false);
        }
    }

    private void Update()
    {
        if (timeRemaining > 0f){
            timeRemaining -= Time.deltaTime;

            if(timeRemaining <= 0f){
                timeRemaining = 0f;
                Debug.Log("Timer Done");
                TimerFinished();
            }
            UpdateTimerText();
            UpdateCoinText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + coins;
    }

    public void ApplyTimerUpgrade(int level)
    {
        gameTimeInSec = 300.0f + (level * 30.0f);
    }

    private void TimerFinished()
    {
        Debug.Log("Timer Done");
        if (upgradeShop != null)
        {
            upgradeShop.SetActive(true);
        }
        StarterAssets.FirstPersonController player =
        FindFirstObjectByType<StarterAssets.FirstPersonController>();
        if (player != null)
        {
            player.enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    public void StartNextRound()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddCoin()
    {
        int coinsToAdd = 1 + moneyLevel; 
        coins += coinsToAdd;
        Debug.Log(coins);
    }

    private void ShowUpgradeShop()
    {
         if (upgradeShop != null)
        {
            upgradeShop.SetActive(true);
            Debug.Log("Upgrade Shop ACTIVATED!");
        }
        else
        {
            Debug.LogError("Upgrade Shop reference is NULL!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject newTimerText = GameObject.FindGameObjectWithTag("TimerText");
        GameObject newCoinText = GameObject.FindGameObjectWithTag("CoinText");
        if (newTimerText != null)
        {
            timerText = newTimerText.GetComponent<TMP_Text>();
        }

        if (newCoinText != null)
        {
            coinText = newCoinText.GetComponent<TMP_Text>();
        }
        UpgradeShop shop = FindFirstObjectByType<UpgradeShop>(FindObjectsInactive.Include);

        if (shop != null)
        {
            upgradeShop = shop.gameObject;
            upgradeShop.SetActive(false);
            shop.gameObject.SetActive(false);
        }

        timeRemaining = gameTimeInSec;

        StarterAssets.FirstPersonController player =
            FindFirstObjectByType<StarterAssets.FirstPersonController>();

        if (player != null)
        {
            player.ApplySpeedUpgrade(speedLevel);
        }

        GrabSystem grabSystem = FindFirstObjectByType<GrabSystem>();

        if (grabSystem != null)
        {
            grabSystem.ApplyThrowForceUpgrade(throwLevel);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (player != null)
        {
            player.enabled = true;
            player.ApplySpeedUpgrade(speedLevel);
        }
        UpdateTimerText();
        UpdateCoinText();
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    //spending coins logic 
    public bool SpendCoins(int amount){
        if(coins < amount){
            return false;
        }
        coins -= amount;
        return true; 
    }
}