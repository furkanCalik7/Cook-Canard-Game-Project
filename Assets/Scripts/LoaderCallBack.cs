using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;

    void Update()
    {
        if(isFirstUpdate) {
            isFirstUpdate = false;

            Loader.LoaderCallBack();
        }  
    }
}
