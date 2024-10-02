using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cuttingCounter.OnProgressChanged += ContainerCounter_OnCuttingActionPerformed;
    }

    private void ContainerCounter_OnCuttingActionPerformed(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if (e.progressChanged <= 0) return;
        animator.SetTrigger(CUT);
    }
}
