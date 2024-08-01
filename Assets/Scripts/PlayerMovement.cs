using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 1f;
    public float jumpDuration = 0.8f;
    public float fallSpeed = 2f;

    private float jumpStartTime;

    public bool isJumping = false;
    public bool isGrounded = false;
    public bool isFalling = false;

    private Vector3 originalPosition;

    private Collider2D playerCollider;

    public Rigidbody2D player;

    public int landCount;
    public string PlayerStateEvent = "";

    FMOD.Studio.EventInstance playerState;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        isGrounded = true;
        playerState = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);
        playerState.start();
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump, this.transform.position);
            Jump();
        }

        if (isJumping)
        {
        }
    }

    void Move()
    {

        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        Vector2 move = new Vector2(moveX, moveY).normalized;

        if (isJumping && 1==0)
        {
            
        }
        else
        {
            player.velocity = move * moveSpeed;
        }
    }

    void Jump()
    {
        Physics2D.IgnoreLayerCollision((LayerMask.NameToLayer("Oblivion")), (LayerMask.NameToLayer("Default")), true);

        isJumping = true;
        isGrounded = false;
        jumpStartTime = Time.time;
        originalPosition = transform.position;

        StartCoroutine(StartJump());
    }

    IEnumerator StartJump()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = Color.black;
        Physics2D.IgnoreLayerCollision((LayerMask.NameToLayer("Oblivion")), (LayerMask.NameToLayer("Default")), false);
        isJumping = false;


    }

    void HandleJump()
    {
        float elapsedTime = Time.time - jumpStartTime;
        float progress = elapsedTime / jumpDuration;

        if (progress < 1f)
        {
             float height = Mathf.Sin(Mathf.PI * progress) * jumpHeight;
             transform.position = new Vector3(transform.position.x, originalPosition.y + height, transform.position.z);
        }
        else
        {
            Physics2D.IgnoreLayerCollision((LayerMask.NameToLayer("Oblivion")), (LayerMask.NameToLayer("Default")), false);
            isJumping = false;
            isGrounded = true;
            Die();
        }

    }

    void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Oblivion")))
        {
            Debug.Log("DEAD");
            StartCoroutine(ReloadSceneAfterDelay(1f)); 
        }
    }

    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground"))
        {
            landCount++;
            isGrounded = true;

            if(landCount==1)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerLand, this.transform.position);
            }
            else if(landCount>1)
            {
                playerState.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            Debug.Log("On Platform");
        }

        if (collision.gameObject.CompareTag("Oblivion"))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerOblivion, this.transform.position);
            Debug.Log("On Oblivion");
            Die();
        }
    }
}



