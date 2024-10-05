using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footstepSoundTimer = 0f;
    private float footstepSoundTimerMax = 0.1f;
    void Awake() {
        player = GetComponent<Player>();
    }

    void Update() {
        footstepSoundTimer += Time.deltaTime;
        if (footstepSoundTimer > footstepSoundTimerMax) {
            if(player.IsWalking()) {
                PlayFootstepSound();
                PlayFootstepSound();
            }
            footstepSoundTimer = 0f;
        }
    }

    private void PlayFootstepSound() {
        
        SoundManager.Instance.PlayFootstepSound(player.transform.position);
    }
}
