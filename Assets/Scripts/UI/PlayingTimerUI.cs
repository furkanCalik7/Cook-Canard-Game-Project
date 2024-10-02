using UnityEngine;
using UnityEngine.UI;

public class PlayingTimerUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;   

    void Update() {
        timerImage.fillAmount = GameManager.Instance.GetPlayingTimerNormalized();
    }
}
