using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject[] visualEffects;

    private void Awake()
    {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }

    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.OnStoveStateChangedEventArgs e)
    {
        if (e.currentState != StoveCounter.StoveState.COOKING)
        {
            Hide();
            return;
        }
        Show();

    }

    private void Show()
    {
        foreach (GameObject gameObject in visualEffects)
        {
            gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject gameObject in visualEffects)
        {
            gameObject.SetActive(false);
        }
    }

}
