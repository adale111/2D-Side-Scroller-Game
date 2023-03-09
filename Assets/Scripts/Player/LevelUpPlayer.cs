using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Upgrade
{
    [SerializeField]
    private string name;
    public string Name => name;
    [SerializeField]
    private string description;
    public string Description => description;

    [SerializeField]
    private int level = 0;
    [SerializeField]
    private int maxLevel = 1;

    public bool CanBeUpgraded()
    {
        return level < maxLevel;
    }

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
        }
    }
    public Upgrade(string name, string description)
    {
        this.name = name;
        this.description = description;
        this.maxLevel = 1;
    }

    public Upgrade(string name, string description, int maxLevel)
    {
        this.name = name;
        this.description = description;
        this.maxLevel = maxLevel;
    }
}

public class LevelUpPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Upgrade> upgradeList = new List<Upgrade>
    {
        new Upgrade("Upgrade Fire Rate", "Increases the rate of fire for your weapon by 10%.", 1),
        new Upgrade("Upgrade Damage", "Increases the amount of damage dealt by your weapon by 5.", 1),
        new Upgrade("Upgrade Health", "Increases the maximum health of your player by 10.", 1),
        new Upgrade("Upgrade Speed", "Increase speed by 10%", 1),
        new Upgrade("Plasma Gun", "Unlock a high damage but slow firing Plasma Gun" ),
        new Upgrade("Shield", "Provides a shield that blocks an instance of damage, recharges after 10 seconds"),
        new Upgrade("Rocket Launcher", "Gives a Rocket Launcher that deals high damage and homes in on enemies")
    };


    private int Exp;
    public TextMeshProUGUI ExpText;
    public int Level;
    public int ExptoLevelup;
    public Image expBar;
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI LevelUpText;
    public GameObject overlay;
    public GameObject LevelUpUI;

    [SerializeField]
    private List<ChooseUpgradeButton> chooseUpgradeButtons = new List<ChooseUpgradeButton>();

    public int health;
    public TextMeshProUGUI HealthText;


    // Start is called before the first frame update
    void Start()
    {

        Level = 1;

        ExptoLevelup = 5;
        ExpText.text = "Exp: " + Exp + "/" + ExptoLevelup;

        CurrentLevelText.text = "Level: " + Level;

        SetUpgradeMenuActive(false);

        UpdateExpBar();

    }

    // Update is called once per frame
    void Update()
    {
        if (Exp >= ExptoLevelup)
        {

            LevelUp();

        }


    }

    public void DeclareUpgradeDictionary()
    {

    }
    public void LevelUp()
    {
        Level += 1;
        CurrentLevelText.text = "Level: " + Level;
        Exp = 0;
        ExptoLevelup += 2;
        ExpText.text = "Exp: " + Exp + "/" + ExptoLevelup;
        UpdateExpBar();

        SetUpgradeMenuActive(true);

        List<Upgrade> possibleUpgrades = upgradeList.Where(upgrade => upgrade.CanBeUpgraded()).ToList();

        int numberOfChoices = possibleUpgrades.Count < 3 ? possibleUpgrades.Count : 3;
        for (int i = 0; i < numberOfChoices; i++)
        {
            int rand = (int)UnityEngine.Random.Range(0, possibleUpgrades.Count);
            chooseUpgradeButtons[i].gameObject.SetActive(true);
            chooseUpgradeButtons[i].SetUpgrade(possibleUpgrades[rand]);
            possibleUpgrades.RemoveAt(rand);


        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("exp"))
        {
            Exp++;
            ExpText.text = "Exp: " + Exp + "/" + ExptoLevelup;
            UpdateExpBar();
        }
    }

    private void UpdateExpBar()
    {
        expBar.fillAmount = (float)Exp / ExptoLevelup;

    }

    public void ChooseUpgrade(GameObject button)
    {

        var upgradeSelected = button.GetComponentInChildren<ChooseUpgradeButton>().Upgrade;
        upgradeSelected.LevelUp();
        switch (upgradeSelected.Name)
        {
            case "Upgrade Fire Rate":
                UpgradeFireRate();
                break;
            case "Upgrade Damage":
                UpgradeDamage();
                break;
            case "Upgrade Health":
                UpgradeHealth();
                break;
            case "Upgrade Speed":
                UpgradeSpeed();
                break;
            case "Plasma Gun":
                PlasmaGun();
                break;
            case "Shield":
                Shield();
                break;
            case "Rocket Launcher":
                RocketLauncher();
                break;
        }

        SetUpgradeMenuActive(false);
    }

    public void SetUpgradeMenuActive(bool value)
    {
        LevelUpText.enabled = value;
        overlay.SetActive(value);
        LevelUpUI.SetActive(value);
        Time.timeScale = value ? 0 : 1;

        if (!value)
        {
            foreach (ChooseUpgradeButton button in chooseUpgradeButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void UpgradeFireRate()
    {
        /* Accesses the PlayerShoots script and makes the time between shots
         less by 0.4 seconds */
        PlayerShoots playerShoots = GetComponent<PlayerShoots>();
        playerShoots.fireRate *= 0.9f;
        playerShoots.plasmaGunFirerate *= 0.9f;
    }
    public void UpgradeDamage()
    {
        PlayerShoots playerShoots = GetComponent<PlayerShoots>();
        playerShoots.damage += 5;
        playerShoots.plasmaGunDamage += 5;

    }

    public void UpgradeHealth()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.playerHealth += 10;
        HealthText.text = ("Health: " + playerMovement.playerHealth);
    }

    public void UpgradeSpeed()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.speed *= 1.1f;
    }

    public void PlasmaGun()
    {
        PlayerShoots playerShoots = GetComponent<PlayerShoots>();
        playerShoots.plasmaGun.SetActive(true);

    }

    public void Shield()
    {
        PlayerShoots playerShoots = GetComponent<PlayerShoots>();
        playerShoots.EquiptShield();
    }

    public void RocketLauncher()
    {
        PlayerShoots playerShoots = GetComponent<PlayerShoots>();
        playerShoots.rocketLauncher.SetActive(true);
    }
}



