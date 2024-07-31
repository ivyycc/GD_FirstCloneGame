using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleAnimations();
    }

    void HandleAnimations()
    {
        
        bool isRunning = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        
        bool isJumping = playerMovement.isJumping;

       
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);

        
        if (!isRunning && !isJumping)
        {
            animator.SetBool("isRunning", false);
        }
    }
}
