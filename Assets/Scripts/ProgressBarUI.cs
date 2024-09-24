using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image image;
    private void Start()
    {
        cuttingCounter.OnCuttingActionPerformed += CuttingCounter_OnCuttingActionPerformed;

        Hide();
    }

    private void CuttingCounter_OnCuttingActionPerformed(object sender, CuttingCounter.OnCuttingActionPerformedEventArgs e)
    {
        image.fillAmount = e.normalizedCuttingProgress;

        if (e.normalizedCuttingProgress >= 0f && e.normalizedCuttingProgress < 1f)
        {
            Show();
        } else {
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
