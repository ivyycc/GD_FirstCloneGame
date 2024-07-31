using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    public int landCount;
    public string PlayerStateEvent = "";

    public bool hasJumped;

    FMOD.Studio.EventInstance playerState;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerState = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);
        playerState.start();
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
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.playerLand, this.transform.position);


            landCount++;

            if (landCount == 1)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerLand, this.transform.position);
            }
            else if (landCount > 1)
            {
                playerState.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }

        

        
        
    }
}
