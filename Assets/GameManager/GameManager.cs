using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float alliesDmgMultip { get; private set; }
    public float gainExpMultip { get; private set; }
    public float alliesMovementSpeedMultip { get; private set; }
    public float alliesAttackSpeedMultip { get; private set; }
    public float tricDmgMultip { get; private set; }
    public float stegDmgMultip { get; private set; }
    public float brachDmgMultip { get; private set; }
    public float brachTailRangeAdd { get; private set; }
    public float stegoSpikeRangeAdd { get; private set; }
    public float tricSlowRangeAdd { get; private set; }
    public float wallTimeRegen { get; private set; }
    public bool wallHeal { get; private set; }

    void Start()
    {
        alliesDmgMultip = 1f;
        gainExpMultip = 1f;
        alliesMovementSpeedMultip = 1f;
        alliesAttackSpeedMultip = 1f;
        tricDmgMultip = 1f;
        stegDmgMultip = 1f;
        brachDmgMultip = 1f;
        brachTailRangeAdd = 0f;
        stegoSpikeRangeAdd = 0f;
        tricSlowRangeAdd = 0f;
        wallTimeRegen = 0f;
        wallHeal = false;
    }

    public void Upgrade(UpgradeType upgradeType) {
        switch (upgradeType) {
            case UpgradeType.AlliesDmg:
                alliesDmgMultip *= 1.1f;
                break;
            case UpgradeType.GainExp:
                gainExpMultip *= 1.03f;
                break;
            case UpgradeType.AlliesMovementSpeed:
                alliesMovementSpeedMultip *= 1.1f;
                break;
            case UpgradeType.AlliesAttackSpeed:
                alliesAttackSpeedMultip *= 1.1f;
                break;
            case UpgradeType.TricDmg:
                tricDmgMultip *= 1.2f;
                break;
            case UpgradeType.StegDmg:
                stegDmgMultip *= 1.2f;
                break;
            case UpgradeType.BrachDmg:
                brachDmgMultip *= 1.2f;
                break;
            case UpgradeType.BrachTailRange:
                brachTailRangeAdd += 1f;
                break;
            case UpgradeType.StegoSpikeRange:
                stegoSpikeRangeAdd += 1f;
                break;
            case UpgradeType.TricSlowRange:
                tricSlowRangeAdd += 1f;
                break;
            case UpgradeType.WallTimeRegen:
                wallTimeRegen = 10f;
                break;
            case UpgradeType.WallHeal:
                wallHeal = true;
                break;
        }
    }
}
