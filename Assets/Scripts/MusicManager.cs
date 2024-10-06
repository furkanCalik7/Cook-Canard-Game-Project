using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static string PLAYER_PERFS_MUSIC_VOLUME_LEVEL = "MusicVolumeLevel";
    public static MusicManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(PLAYER_PERFS_MUSIC_VOLUME_LEVEL, .3f);
    }

    public void ChangeVolume()
    {
        audioSource.volume += 0.1f;
        if (audioSource.volume >= 1f)
        {
            audioSource.volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PERFS_MUSIC_VOLUME_LEVEL, audioSource.volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }

}
