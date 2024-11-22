using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Animator playerAnimator;
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float kickForce = 10f;
    [SerializeField] protected Transform footTransform;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform goalTarget; // Assign the respective goal Transform for each player

    protected Rigidbody2D rb2d;
    protected bool isGrounded;
    protected GameObject ball;
    private float groundCheckRadius = 0.2f;

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleMovement();
        HandleKicking();
    }

    private void HandleMovement()
    {
        float horizontal = GetHorizontalInput();
        bool jump = GetJumpInput();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        MoveCharacter(horizontal, jump);
        PlayMovementAnimation(horizontal);
    }

    private void HandleKicking()
    {
        if (GetKickInputStraight())
        {
            KickBall(Vector2.right, kickForce);
        }
        else if (GetKickInputChip())
        {
            KickBall(new Vector2(1f, 1f).normalized, kickForce); // Chip direction
        }
        else if (GetKickInputSmash())
        {
            KickBall(Vector2.down, kickForce); // Smash direction
        }
    }

    protected virtual float GetHorizontalInput() => 0f;
    protected virtual bool GetJumpInput() => false;
    protected virtual bool GetKickInputStraight() => false;
    protected virtual bool GetKickInputChip() => false;
    protected virtual bool GetKickInputSmash() => false;

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

    protected virtual void PlayMovementAnimation(float horizontal)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Flip the player based on movement direction
        Vector3 scale = transform.localScale;
        scale.x = horizontal < 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;

        // Handle jumping animation
        playerAnimator.SetBool("Jump", !isGrounded);
    }

    private void KickBall(Vector2 direction, float force)
    {
        if (ball != null)
        {
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                // Determine direction towards the goal area
                direction = (goalTarget.position - footTransform.position).normalized * direction;
                ballRb.AddForce(direction * force, ForceMode2D.Impulse);
            }
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
