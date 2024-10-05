using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterAudio : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }
    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.OnStoveStateChangedEventArgs e) {
        bool playSound = e.currentState == StoveCounter.StoveState.COOKING;
        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Stop();
        }

    }
}
