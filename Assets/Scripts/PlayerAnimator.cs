using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
    }

}
