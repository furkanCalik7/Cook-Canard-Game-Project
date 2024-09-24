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
        cuttingCounter.OnCuttingActionPerformed += ContainerCounter_OnCuttingActionPerformed;
    }

    private void ContainerCounter_OnCuttingActionPerformed(object sender, CuttingCounter.OnCuttingActionPerformedEventArgs e)
    {
        if (e.normalizedCuttingProgress <= 0) return;
        animator.SetTrigger(CUT);
    }
}
