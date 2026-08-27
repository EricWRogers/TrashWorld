using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private int upgradeCost = 10;

    [SerializeField] private Button speed1Button;
    [SerializeField] private Button speed2Button;
    [SerializeField] private Button speed3Button;

    [SerializeField] private Button throw1Button;
    [SerializeField] private Button throw2Button;
    [SerializeField] private Button throw3Button;

    [SerializeField] private Button money1Button;
    [SerializeField] private Button money2Button;
    [SerializeField] private Button money3Button;

    [SerializeField] private Button timer1Button;
    [SerializeField] private Button timer2Button;
    [SerializeField] private Button timer3Button;

    

    private void Start(){
        //upgrad button
        upgradeButton.onClick.AddListener(BuyUpgrade);

        //speed buttons
        speed1Button.onClick.AddListener(() => BuySpeedUpgrade(1));
        speed2Button.onClick.AddListener(() => BuySpeedUpgrade(2));
        speed3Button.onClick.AddListener(() => BuySpeedUpgrade(3));

        //throw buttons
        throw1Button.onClick.AddListener(() => BuyThrowUpgrade(1));
        throw2Button.onClick.AddListener(() => BuyThrowUpgrade(2));
        throw3Button.onClick.AddListener(() => BuyThrowUpgrade(3));

        //money buttons
        money1Button.onClick.AddListener(() => BuyMoneyUpgrade(1));
        money2Button.onClick.AddListener(() => BuyMoneyUpgrade(2));
        money3Button.onClick.AddListener(() => BuyMoneyUpgrade(3));

        //timer buttons
        timer1Button.onClick.AddListener(() => BuyTimerUpgrade(1));
        timer2Button.onClick.AddListener(() => BuyTimerUpgrade(2));
        timer3Button.onClick.AddListener(() => BuyTimerUpgrade(3));

        exitButton.onClick.AddListener(NextRound);

        UpdateButtons();
    }

    //upgrade button logic
    private void BuyUpgrade(){
        if (!GameManager.instance.SpendCoins(upgradeCost))
            return;
        
        GameManager.instance.upgradesUnlocked = true;
        UpdateButtons();
    }

    //speed upgrade logic
    private void BuySpeedUpgrade(int level){
        if (!GameManager.instance.upgradesUnlocked)
            return;
        
        if (level != GameManager.instance.speedLevel + 1)
            return;

        int cost = GetCost(level);

        if (!GameManager.instance.SpendCoins(cost))
            return;

        GameManager.instance.speedLevel = level;

        StarterAssets.FirstPersonController player =
            FindFirstObjectByType<StarterAssets.FirstPersonController>();

        if (player != null)
        {
            player.ApplySpeedUpgrade(GameManager.instance.speedLevel);
        }

        UpdateButtons();
    }

    //throw upgrade logic
    private void BuyThrowUpgrade(int level){
        if (!GameManager.instance.upgradesUnlocked)
            return;
        
        if (level != GameManager.instance.throwLevel + 1)
            return;

        int cost = GetCost(level);

        if (!GameManager.instance.SpendCoins(cost))
            return;

        GameManager.instance.throwLevel = level;

        GrabSystem grabSystem = FindFirstObjectByType<GrabSystem>();

        if (grabSystem != null)
        {
            grabSystem.ApplyThrowForceUpgrade(GameManager.instance.throwLevel);
        }

        UpdateButtons();
    }

    //money upgrade logic
    private void BuyMoneyUpgrade(int level){
        if (!GameManager.instance.upgradesUnlocked)
            return;

        if (level != GameManager.instance.moneyLevel + 1)
            return;

        int cost = GetCost(level);

        if (!GameManager.instance.SpendCoins(cost))
            return;

        GameManager.instance.moneyLevel = level;

        UpdateButtons();
    }

    //timer upgrade logic
    private void BuyTimerUpgrade(int level){
        if (!GameManager.instance.upgradesUnlocked)
            return;
        
        if (level != GameManager.instance.timerLevel + 1)
            return;

        int cost = GetCost(level);

        if (!GameManager.instance.SpendCoins(cost))
            return;

        GameManager.instance.timerLevel = level;
        GameManager.instance.ApplyTimerUpgrade(GameManager.instance.timerLevel);

        UpdateButtons();
    }

    //button locking
    private void UpdateButtons(){
        //this locks everything until the upgrade shop is bought
        speed1Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.speedLevel == 0;
        speed2Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.speedLevel == 1;
        speed3Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.speedLevel == 2;

        throw1Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.throwLevel == 0;
        throw2Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.throwLevel == 1;
        throw3Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.throwLevel == 2;

        money1Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.moneyLevel == 0;
        money2Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.moneyLevel == 1;
        money3Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.moneyLevel == 2;

        timer1Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.timerLevel == 0;
        timer2Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.timerLevel == 1;
        timer3Button.interactable = GameManager.instance.upgradesUnlocked && GameManager.instance.timerLevel == 2;
    }

    //cost logic
    private int GetCost(int level){
        switch(level){
            case 1:
                return 15;
            case 2:
                return 20;
            case 3:
                return 30;
            default:
                return 0;
        }
    }
    private void NextRound()
    {
        Debug.Log("EXIT BUTTON PRESSED");
        GameManager.instance.StartNextRound();
    }
}