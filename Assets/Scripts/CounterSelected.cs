using UnityEngine;

public class CounterSelected : MonoBehaviour
{
    void Start()
    {
        Debug.Log("here");
        PlayerMovement.Instance.OnSelectedClearCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, PlayerMovement.OnSelectedClearCounterChangedEventArgs e)
    {
        Debug.Log("selected");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
