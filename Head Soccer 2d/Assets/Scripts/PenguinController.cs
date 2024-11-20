using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    //[SerializeField] private Animator playerAnimator;
    // [SerializeField] private float speed;
    // [SerializeField] private float jumpForce;

    // private Rigidbody2D rb2d;
    // private bool isGrounded;
    // [SerializeField] private Transform groundCheck; // Assign this in the Unity Editor
    // [SerializeField] private LayerMask groundLayer; // Assign the Ground layer
    // private float groundCheckRadius = 0.2f;
    // private void Awake()
    // {
    //     rb2d = GetComponent<Rigidbody2D>();
    // }

    // private void Update()
    // {
    //     // Use WASD keys for Penguin
    //     float horizontal = 0f;
    //     if (Input.GetKey(KeyCode.A)) horizontal = -1f; // Move left
    //     if (Input.GetKey(KeyCode.D)) horizontal = 1f;  // Move right
    //     bool jump = Input.GetKeyDown(KeyCode.W);       // Jump with W
    //     isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    //     MoveCharacter(horizontal, jump);
    //     PlayMovementAnimation(horizontal, jump);
    // }

    // private void MoveCharacter(float horizontal, bool jump)
    // {
    //     Vector3 position = transform.position;
    //     position.x += horizontal * speed * Time.deltaTime;
    //     transform.position = position;

    //     if (jump && isGrounded)
    //     {
    //         rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    //     }
    // }

    // private void PlayMovementAnimation(float horizontal, bool jump)
    // {
    //     playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

    //     // Flipping the Penguin based on movement direction
    //     Vector3 scale = transform.localScale;
    //     scale.x = horizontal < 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
    //     transform.localScale = scale;

    //     // Handle jumping animation
    //     playerAnimator.SetBool("Jump", !isGrounded);
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float kickForce = 10f; // Force applied to the ball when kicked
    [SerializeField] private Transform footTransform;
    private Rigidbody2D rb2d;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck; // Assign this in the Unity Editor
    [SerializeField] private LayerMask groundLayer; // Assign the Ground layer
    private float groundCheckRadius = 0.2f;

    private GameObject ball; // Reference to the ball object

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Use WASD keys for Penguin
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A)) horizontal = -1f; // Move left
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;  // Move right
        bool jump = Input.GetKeyDown(KeyCode.W);       // Jump with W
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        MoveCharacter(horizontal, jump);
        PlayMovementAnimation(horizontal);

        // Kick functionality
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            KickBall();
        }
    }

    private void MoveCharacter(float horizontal, bool jump)
    {
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        if (jump && isGrounded)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void PlayMovementAnimation(float horizontal)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Flipping the Penguin based on movement direction
        Vector3 scale = transform.localScale;
        scale.x = horizontal < 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;

        // Handle jumping animation
        playerAnimator.SetBool("Jump", !isGrounded);
    }

    private void KickBall()
    {
        if (ball != null)
        {
            Debug.Log("Player is kicking the ball!");
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                Vector2 direction = (ball.transform.position - footTransform.position).normalized; // Use footTransform
                ballRb.AddForce(direction * kickForce, ForceMode2D.Impulse);
                Debug.Log($"Kick applied! Force: {kickForce}, Direction: {direction}, Ball Velocity: {ballRb.velocity}");
            }
            else
            {
                Debug.LogError("No Rigidbody2D found on the ball!");
            }
        }
        else
        {
            Debug.LogWarning("No ball in range to kick!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            ball = collision.gameObject; // Assign the ball object when in contact
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            ball = null; // Remove reference to the ball when out of contact
        }
    }
}
