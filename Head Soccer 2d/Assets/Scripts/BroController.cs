using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroController : PlayerController
{
    //[SerializeField] private Animator playerAnimator;
    //[SerializeField] private float speed;
    //[SerializeField] private float jumpForce;
    //[SerializeField] private float kickForce = 10f;
    //private Rigidbody2D rb2d;
    //private bool isGrounded;
    //[SerializeField] private Transform groundCheck; // Assign this in the Unity Editor
    //[SerializeField] private LayerMask groundLayer; // Assign the Ground layer
    //private float groundCheckRadius = 0.2f;
    //[SerializeField] private Transform footTransform;
    //private GameObject ball;
    //private void Awake()
    //{
    //    rb2d = GetComponent<Rigidbody2D>();
    //}

    //private void Update()
    //{
    //    // Use Arrow Keys for Bro
    //    float horizontal = 0f;
    //    if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f; // Move left
    //    if (Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;  // Move right
    //    bool jump = Input.GetKeyDown(KeyCode.UpArrow);          // Jump with Up Arrow

    //    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    //    MoveCharacter(horizontal, jump);
    //    PlayMovementAnimation(horizontal, jump);

    //    // Kick functionality
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        KickBall();
    //    }
    //}

    //private void MoveCharacter(float horizontal, bool jump)
    //{
    //    Vector3 position = transform.position;
    //    position.x += horizontal * speed * Time.deltaTime;
    //    transform.position = position;

    //    if (jump && isGrounded)
    //    {
    //        rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    //    }
    //}

    //private void PlayMovementAnimation(float horizontal, bool jump)
    //{
    //    playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

    //    // Flipping the Bro based on movement direction (Inverted logic)
    //    Vector3 scale = transform.localScale;
    //    if (horizontal < 0)
    //    {
    //        scale.x = Mathf.Abs(scale.x); // Face left
    //    }
    //    else if (horizontal > 0)
    //    {
    //        scale.x = -Mathf.Abs(scale.x); // Face right
    //    }
    //    transform.localScale = scale;

    //    // Handle jumping animation
    //    playerAnimator.SetBool("Jump", !isGrounded);
    //}

    //private void KickBall()
    //{
    //    if (ball != null)
    //    {
    //        Debug.Log("Player is kicking the ball!");
    //        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
    //        if (ballRb != null)
    //        {
    //            Vector2 direction = (ball.transform.position - footTransform.position).normalized; // Use footTransform
    //            ballRb.AddForce(direction * kickForce, ForceMode2D.Impulse);
    //            Debug.Log($"Kick applied! Force: {kickForce}, Direction: {direction}, Ball Velocity: {ballRb.velocity}");
    //        }
    //        else
    //        {
    //            Debug.LogError("No Rigidbody2D found on the ball!");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No ball in range to kick!");
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }

    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        ball = collision.gameObject; // Assign the ball object when in contact
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }

    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        ball = null; // Remove reference to the ball when out of contact
    //    }
    //}


    protected override float GetHorizontalInput()
    {
        // Reverse horizontal input to invert Bro's controls
        if (Input.GetKey(KeyCode.LeftArrow)) return -1f;  // Move right
        if (Input.GetKey(KeyCode.RightArrow)) return 1f; // Move left
        return 0f;
    }

    protected override void PlayMovementAnimation(float horizontal)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Flip Bro's sprite based on inverted controls
        Vector3 scale = transform.localScale;
        if (horizontal > 0) // Moving right
        {
            scale.x = -Mathf.Abs(scale.x); // Face left (default)
        }
        else if (horizontal < 0) // Moving left
        {
            scale.x = Mathf.Abs(scale.x); // Face right
        }
        transform.localScale = scale;

        // Handle jumping animation
        playerAnimator.SetBool("Jump", !isGrounded);
    }

    protected override bool GetJumpInput() => Input.GetKeyDown(KeyCode.UpArrow);

    protected override bool GetKickInputStraight() => Input.GetKeyDown(KeyCode.Space);

    protected override bool GetKickInputChip() => Input.GetKeyDown(KeyCode.Keypad0);

    protected override bool GetKickInputSmash() => Input.GetKeyDown(KeyCode.Keypad1);

}
