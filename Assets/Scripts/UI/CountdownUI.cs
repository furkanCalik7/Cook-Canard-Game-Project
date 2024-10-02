using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    void Start()
    {
        countdownText.gameObject.SetActive(false);
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    void Update()
    {
        UpdateVisual();
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.OnGameStateChangedEventArgs e)
    {
        if (e.gameState == GameManager.GameState.CountdownToStart)
        {
            countdownText.gameObject.SetActive(true);
        } else {
            countdownText.gameObject.SetActive(false);
        }
    }

    void UpdateVisual()
    {
        countdownText.text = GetFormattedCountdownString();
    }

    private string GetFormattedCountdownString()
    {
        return Mathf.Ceil(GameManager.Instance.GetCountdown()).ToString();
    }
}
