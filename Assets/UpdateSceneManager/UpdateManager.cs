using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UpdateManager : MonoBehaviour {
    public static UpdateManager instance;
    [SerializeField] Transform canvas;
    List<Option> options;
    int cardCount = 3;

    List<UpgradeCard> upgradeCards = new List<UpgradeCard>();

    public UnityEvent<UpgradeType> OnUpdateChoose;

    void Start() {
        UpdateManager.instance = this;

        upgradeCards = Resources.LoadAll<UpgradeCard>("Upgrades/").ToList();
        options = GetComponentsInChildren<Option>().ToList();

        Open();
    }

    public void Open() {
        canvas.gameObject.SetActive(true);

        List<UpgradeCard> tempCards = ThreeRandomCardsData();

        for (int i = 0; i < cardCount; i++) {
            options[i].SetUpgradeCard(tempCards[i]);
        }
        Debug.Log(tempCards.Count);
    }

    public void Select(UpgradeType upgradeType) {
        canvas.gameObject.SetActive(false);
        OnUpdateChoose.Invoke(upgradeType);
    }

    private List<UpgradeCard> ThreeRandomCardsData() {
        List<UpgradeCard> compy = new List<UpgradeCard>(upgradeCards);
        List<UpgradeCard> returnList = new List<UpgradeCard>();

        for(int i = 0; i < cardCount; i++) {
            Debug.Log(compy.Count);
            int rand = Random.Range(0, compy.Count);
            returnList.Add(compy[rand]);
            compy.RemoveAt(rand);
        }

        return returnList;
    }
}
