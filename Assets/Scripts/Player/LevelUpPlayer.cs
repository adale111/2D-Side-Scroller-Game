using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class LevelUpPlayer : MonoBehaviour
{
    private int Exp;
    public TextMeshProUGUI ExpText;
    public int Level;
    public int ExptoLevelup;
    public Image expBar;
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI LevelUpText;
    public GameObject overlay;
    public GameObject LevelUpUI;
    public GameObject ChooseUpgrade1Button;
    public GameObject ChooseUpgrade2Button;
    public GameObject ChooseUpgrade3Button;
    public TextMeshProUGUI Upgrade1Description;
    public TextMeshProUGUI Upgrade2Description;
    public TextMeshProUGUI Upgrade3Description;
    public int health;
    public TextMeshProUGUI HealthText;

    public bool hasPlasmaGun;
    public bool hasShield;
    public bool hasRocketLauncher;



    // Start is called before the first frame update
    void Start()
    {

        Level = 1;

        ExptoLevelup = 5;
        ExpText.text = "Exp: " + Exp + "/" + ExptoLevelup;


        CurrentLevelText.text = "Level: " + Level;

        LevelUpText.enabled = false;
        overlay.SetActive(false);
        UpgradeButtonDisabled();

        UpdateExpBar();


        hasPlasmaGun = false;
        hasShield = false;
        hasRocketLauncher = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Exp >= ExptoLevelup)
        {

            LevelUp();

        }

        if (LevelUpText.enabled == true)
        {
            Time.timeScale = 0;

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


        LevelUpText.enabled = true;
        overlay.SetActive(true);
        UpgradeButtonEnabled();

        string[] upgrades = { "Upgrade Fire Rate", "Upgrade Damage", "Upgrade Health", "Upgrade Speed", "Plasma Gun", "Shield", "Rocket Launcher"};


        // Explanation of the below:
        // Used System.Linq which gives more options for arrays, lists, databases, XML docs etc
        // If you have one of the upgrade below, the upgrades (x), where x does not equal Plasma Gun...
        // ...are added to an array which stops you having the same upgrade twice
        if (hasPlasmaGun == true)
{
            upgrades = upgrades.Where(x => x != "Plasma Gun").ToArray();
        }

        if (hasShield == true)
        {
            upgrades = upgrades.Where(x => x != "Shield").ToArray();
        }

if (hasRocketLauncher == true)
        {
            upgrades = upgrades.Where(x => x != "Rocket Launcher").ToArray();
        }


        Shuffle(upgrades);
        var upgradeFunctions = new Dictionary<string, string>() {
            { "Upgrade Fire Rate", nameof(UpgradeFireRate) },
            { "Upgrade Damage", nameof(UpgradeDamage) },
            { "Upgrade Health", nameof(UpgradeHealth) },
            { "Upgrade Speed", nameof(UpgradeSpeed) },
            { "Plasma Gun", nameof(PlasmaGun) },
            { "Shield", nameof(Shield) },
            { "Rocket Launcher", nameof(RocketLauncher) }
        };

   

        if (ChooseUpgrade1Button.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            ChooseUpgrade1Button.GetComponentInChildren<TextMeshProUGUI>().text = upgrades[0];
        }
        if (ChooseUpgrade2Button.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            ChooseUpgrade2Button.GetComponentInChildren<TextMeshProUGUI>().text = upgrades[1];
        }
        if (ChooseUpgrade3Button.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            ChooseUpgrade3Button.GetComponentInChildren<TextMeshProUGUI>().text = upgrades[2];
        }

        var upgradeDescriptions = new Dictionary<string, string>()
        {
             { "Upgrade Fire Rate", "Increases the rate of fire for your weapon by 10%." },
             { "Upgrade Damage", "Increases the amount of damage dealt by your weapon by 5." },
             { "Upgrade Health", "Increases the maximum health of your player by 10." },
            { "Upgrade Speed", "Increase speed by 10%" },
            { "Plasma Gun", "Unlock a high damage but slow firing Plasma Gun" },
            {"Shield", "Provides a shield that blocks an instance of damage, recharges after 10 seconds" },
            {"Rocket Launcher", "Gives a Rocket Launcher that deals high damage and homes in on enemies" }
        };

        if (Upgrade1Description != null)
        {
            Upgrade1Description.text = upgradeDescriptions[upgrades[0]];
        }
        if (Upgrade2Description != null)
        {
            Upgrade2Description.text = upgradeDescriptions[upgrades[1]];
        }
        if (Upgrade3Description != null)
        {
            Upgrade3Description.text = upgradeDescriptions[upgrades[2]];
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

        var buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;
        switch (buttonText)
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

        LevelUpText.enabled = false;
        overlay.SetActive(false);
        UpgradeButtonDisabled();
        Time.timeScale = 1;
    }

    void Shuffle(string[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            string temp = arr[i];
            arr[i] = arr[r];
            arr[r] = temp;
        }
    }

    public void UpgradeButtonDisabled()
    {
        LevelUpUI.SetActive(false);
    }

    public void UpgradeButtonEnabled()
    {
        LevelUpUI.SetActive(true);
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
        hasPlasmaGun = true;
        
    }

    public void Shield()
    {
        
        hasShield = true;
    }

    public void RocketLauncher()
    {
        PlayerShoots playerShoots = GetComponent<PlayerShoots>();
        playerShoots.rocketLauncher.SetActive(true);
        hasRocketLauncher = true;
    }
}


    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2;

        [SerializeField]
        private int damage = 2;

        public void SetDamage(int damage)
        {
            this.damage = damage;
        }

        private void Update()
        {
            // Make the bullet move
        }
    }

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private int bulletDamage = 5;

    public void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, this.transform).GetComponent<Bullet>();
        bullet.SetDamage(bulletDamage);
    }
}



