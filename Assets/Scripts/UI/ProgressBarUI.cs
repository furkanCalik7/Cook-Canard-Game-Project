using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressableGameObject;
    [SerializeField] private Image image;
    private IHasProgress progressableObject;
    private void Start()
    {
        progressableObject = progressableGameObject.GetComponent<IHasProgress>();
        if (progressableObject == null)
        {
            return;
        }
        progressableObject.OnProgressChanged += ProgressableObject_OnProgressChanged;

        Hide();
    }

    private void ProgressableObject_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        image.fillAmount = e.progressChanged;

        if (e.progressChanged > 0f && e.progressChanged < 1f)
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
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
