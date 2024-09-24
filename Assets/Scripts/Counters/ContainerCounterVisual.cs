using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        containerCounter.OnPlayerGrapAnItem += ContainerCounter_OnInteract;
    }

    private void ContainerCounter_OnInteract(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
