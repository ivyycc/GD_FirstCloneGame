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

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        isGrounded = true;
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
        if (!isJumping)
        {
            player.velocity = move * moveSpeed;
        }
        else
        {
            player.velocity = new Vector2(moveX * moveSpeed, player.velocity.y);
        }

        //Vector2 move = new Vector2(moveX, moveY).normalized;
        //player.velocity = move * moveSpeed;
    }

    void Jump()
    {
        Physics2D.IgnoreLayerCollision((LayerMask.NameToLayer("Oblivion")), (LayerMask.NameToLayer("Default")), true);

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
            Physics2D.IgnoreLayerCollision((LayerMask.NameToLayer("Oblivion")), (LayerMask.NameToLayer("Default")), false);
            isJumping = false;
            isGrounded = true;
            Die();
        }

        if (isJumping && Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, originalPosition.y + jumpHeight, transform.position.z);
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
            isGrounded = true;
            Debug.Log("On Platform");
        }

        if (collision.gameObject.CompareTag("Oblivion"))
        {   
            Debug.Log("On Oblivion");
            Die();
        }
    }
}



