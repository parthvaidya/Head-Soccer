using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //[SerializeField] private float crazyForce = 150f; // Adjust this to control bounce intensity
    [SerializeField] private float minBounceForce = 10f; // Minimum force to maintain bounce
    [SerializeField] private Vector2 initialForce = new Vector2(2f, 5f);
    [SerializeField] private GameObject kickParticlePrefab; // Assign your particle prefab in the Inspector
    private GameObject activeParticleEffect;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Apply an initial force to the ball
        rb.AddForce(initialForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the ball hits the ground, ensure it keeps bouncing
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (Mathf.Abs(rb.velocity.y) < minBounceForce)
            {
                // Apply minimum upward force if the ball slows down
                rb.velocity = new Vector2(rb.velocity.x, minBounceForce);
            }
        } else if (collision.gameObject.CompareTag("Penguin") || collision.gameObject.CompareTag("Bro"))
        {
            // Calculate force direction based on the collision
            Vector2 hitDirection = (transform.position - collision.transform.position).normalized;
            float playerHitForce = 15f; // Adjust the force as needed
            SoundManager.Instance.Play(Sounds.Ball);
            // Instantiate and attach particle effect
            if (activeParticleEffect == null) // Prevent multiple instances
            {
                activeParticleEffect = Instantiate(kickParticlePrefab, transform.position, Quaternion.identity);
                activeParticleEffect.transform.SetParent(transform); // Attach to the ball
            }

            // Optional: Stop the particle after some time
            
            // Apply force to simulate the "head ball" effect
            rb.AddForce(hitDirection * playerHitForce, ForceMode2D.Impulse);
            StartCoroutine(StopParticleAfterTime(1f));
        }




    }

    private IEnumerator StopParticleAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (activeParticleEffect != null)
        {
            Destroy(activeParticleEffect);
            activeParticleEffect = null;
        }
    }

    private void FixedUpdate()
    {
        // Prevent the ball from slowing down (optional)
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -minBounceForce));
    }
}
