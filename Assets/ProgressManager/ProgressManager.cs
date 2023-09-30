using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour {
    // Start is called before the first frame update

    public static ProgressManager instance;

    public event Action<float> onLevelUp;

    public int currentLevel { get; private set; }
    public float currentExp { get; private set; }
    public float expNeededToLevel { get; private set; }
    public int currentWave { get; private set; }

    public void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void addExp(float exp) {
        currentExp += exp;

        while(currentExp > expNeededToLevel) {
            currentExp -= expNeededToLevel;
            expNeededToLevel *= 1.7f;
            currentLevel += 1;
            onLevelUp(currentLevel);
        }
    }
}
