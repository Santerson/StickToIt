using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float gravityAmplifier = 1.5f;
    [SerializeField] private float playerHeight = 1.1f;

    private Rigidbody2D rb;
    private float ogGrav;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ogGrav = rb.gravityScale;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null)
        {
            Debug.LogError("Player Rigidbody2D is not assigned.");
            return;
        }
        //checks movement for going left and right. does not move if both are pressed
        if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement if both keys are pressed
        }
        else if (Input.GetKey(leftKey))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(rightKey))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //checks if the jump key is pressed and if the player is grounded
        if (IsGrounded() && Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (!IsGrounded() && Input.GetKey(jumpKey))
        {
            rb.gravityScale = ogGrav;
        }
        else if (!IsGrounded())
        {
            rb.gravityScale = gravityAmplifier;
        }
        else
        {
            rb.gravityScale = ogGrav;
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded using a raycast
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - playerHeight), Vector2.down * 0.1f, Color.red); // Visualize the raycast in the editor
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - playerHeight), Vector2.down, 0.1f);
        return hit.collider != null;
    }
}
