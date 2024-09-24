using UnityEngine;

public class CounterSelected : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisualGameObjects;
    void Start()
    {
        Player.Instance.OnSelectedClearCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedClearCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject gameObject in selectedVisualGameObjects)
        {
            gameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject gameObject in selectedVisualGameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
