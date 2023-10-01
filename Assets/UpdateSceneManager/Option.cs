using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    [SerializeField] UpgradeCard upgradeCard;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI desc;

    public UnityEvent<UpgradeType> OnChoose;
    

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    public void SetUpgradeCard(UpgradeCard uc) {
        upgradeCard = uc;
        SetText();
    }

    public void OnValidate() {
        SetText();
    }

    public void SetText() {
        title.text = upgradeCard.name;
        desc.text = upgradeCard.desc;
    }
    public void OnClick() {
        OnChoose.Invoke(upgradeCard.upgradeType);
    }
}
