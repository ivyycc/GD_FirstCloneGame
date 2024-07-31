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
        bool isMoving = Mathf.Abs(playerMovement.player.velocity.x) > 0.1f || Mathf.Abs(playerMovement.player.velocity.y) > 0.1f;
        bool isJumping = playerMovement.isJumping;

        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isJumping", isJumping);
    }
}
