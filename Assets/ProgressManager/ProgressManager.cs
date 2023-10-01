using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour {
    // Start is called before the first frame update

    public static ProgressManager instance;

    public event Action<float> onLevelUp;
    public event Action<float> onGameOver;

    [SerializeField] private Slider expSlider;
    [SerializeField] private Canvas upgradeCanvas;

    public int currentLevel { get; private set; } = 0;
    public float currentExp { get; private set; } = 0f;
    public float expNeededToLevel { get; private set; } = 40f;
    public int currentWave { get; private set; } = 0;

    public void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        expSlider.minValue = 0f;
        expSlider.maxValue = expNeededToLevel;
        expSlider.value = expSlider.minValue;

        SpawnManager.instance.onWaveEnd += () => StartCoroutine(BeginNextWave());

        BeginGame();
    }

    public void BeginGame() => StartCoroutine(BeginNextWave());

    public void addExp(float exp) {
        currentExp += exp;

        while(currentExp >= expNeededToLevel) {
            currentExp -= expNeededToLevel;
            expNeededToLevel *= 1.7f;
            currentLevel += 1;
            onLevelUp?.Invoke(currentLevel);
        }

        expSlider.maxValue = expNeededToLevel;
        expSlider.value = currentExp;
    }

    private IEnumerator BeginNextWave(float delay = 5f) {
        yield return new WaitForSeconds(delay);

        currentWave += 1;

        SpawnManager.instance.BeginWave(currentWave);
    }
}
