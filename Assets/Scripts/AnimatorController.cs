using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    public int landCount;
   // public string PlayerStateEvent = "";

    public bool hasJumped;

    //FMOD.Studio.EventInstance playerState;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();


        /*
        FMODUnity.RuntimeManager.LoadBank("Master");

        // Log the event path
        Debug.Log("PlayerStateEvent: " + PlayerStateEvent);

        playerState = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);


        if (playerState.isValid())
        {
            playerState.start();
        }
        else
        {
            Debug.LogError("Failed to create event instance for " + PlayerStateEvent);
        }*/
       
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
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.playerLand, this.transform.position);
            }
            else if (landCount > 1)
            {
               // playerState.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}
