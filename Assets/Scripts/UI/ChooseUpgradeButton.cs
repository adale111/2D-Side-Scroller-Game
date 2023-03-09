
using TMPro;
using UnityEngine;

public class ChooseUpgradeButton : MonoBehaviour
{
    private Upgrade upgrade;
    public Upgrade Upgrade => upgrade;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        titleText.text = upgrade.Name;
        descriptionText.text = upgrade.Description;
    }
}
