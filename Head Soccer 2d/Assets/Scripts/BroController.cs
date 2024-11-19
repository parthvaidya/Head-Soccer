using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb2d;
    private bool isGrounded;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Use Arrow Keys for Bro
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f; // Move left
        if (Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;  // Move right
        bool jump = Input.GetKeyDown(KeyCode.UpArrow);          // Jump with Up Arrow

        MoveCharacter(horizontal, jump);
        PlayMovementAnimation(horizontal, jump);
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

    private void PlayMovementAnimation(float horizontal, bool jump)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Flipping the Bro based on movement direction (Inverted logic)
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = Mathf.Abs(scale.x); // Face left
        }
        else if (horizontal > 0)
        {
            scale.x = -Mathf.Abs(scale.x); // Face right
        }
        transform.localScale = scale;

        // Handle jumping animation
        playerAnimator.SetBool("Jump", !isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
