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
<<<<<<< Updated upstream
        bool isMoving = Mathf.Abs(playerMovement.player.velocity.x) > 0.1f || Mathf.Abs(playerMovement.player.velocity.y) > 0.1f;
        bool isJumping = playerMovement.isJumping;

        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isJumping", isJumping);
=======
        // Determine if the player is running based on WASD input
        bool isRunning = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Determine if the player is jumping based on the PlayerMovement state
        bool isJumping = playerMovement.isJumping;

        // Set the animator parameters accordingly
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);

        // If not running or jumping, transition to idle
        if (!isRunning && !isJumping)
        {
            animator.SetBool("isRunning", false);
        }
>>>>>>> Stashed changes
    }
}
