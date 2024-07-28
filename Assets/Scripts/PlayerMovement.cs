using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 1f;
    public float jumpDuration = 0.8f;
    public float fallSpeed = 2f;
    private float jumpStartTime;

    private bool isJumping = false;
    private bool isGrounded = false;
    private bool isFalling = false;

    private Vector3 originalPosition;

    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }

        if (isJumping)
        {
            HandleJump();
        }

        if (isGrounded)
        {
            Die();
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

        Vector3 move = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        isJumping = true;
        isGrounded = false;
        jumpStartTime = Time.time;
        originalPosition = transform.position;
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
            isJumping = false;
            isGrounded = true;
        }
    }

    void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Oblivion")))
        {
            Debug.Log("Player hit the Oblivion layer and will be destroyed.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            Debug.Log("Player is grounded.");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
            Debug.Log("Player left the ground.");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Oblivion") && isGrounded)
        {
            Debug.Log("Player is touching the Oblivion layer while grounded and will be destroyed.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}



