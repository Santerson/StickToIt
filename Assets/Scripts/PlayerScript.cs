using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool hasKey = false;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("How long should the ground raycast be? [top point]")]
    [SerializeField] private float groundCheckDistance = 0.2f;
    [Tooltip("How far should the raycast be for checking the ground? [bottom point]")]
    [SerializeField] private float playerHeight = 1.1f;
    [Tooltip("Left Raycast offset (positive)")]
    [SerializeField] private float leftRaycastOffset = 0.2f;
    [Tooltip("Right Raycast offset (positive)")]
    [SerializeField] private float rightRaycastOffset = 0.2f;

    [Header("Movement")]
    [Tooltip("Take a wild guess...")]
    [SerializeField] private float moveSpeed = 5f;
    [Tooltip("Jump height")]
    [SerializeField] private float jumpForce = 5f;

    [Header("Jumping")]
    [Tooltip("How much should gravity be set to after the player releases the jump button mid-air?")]
    [SerializeField] private float gravityAmplifier = 1.5f;
    [Tooltip("BAD SPELLING LIVES ON EHEHEHEHEHHEHEHEH (seconds)")]
    [SerializeField] private float KyoteeTime = 0.2f;

    [Header("Keybinds")]
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private Rigidbody2D rb;
    private float ogGrav;
    private float KyoteeTimeLeft = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ogGrav = rb.gravityScale;
    }

    private void Update()
    {
        if (rb == null)
        {
            Debug.LogError("Player Rigidbody2D is not assigned.");
            return;
        }

        HandleMovement();
        HandleJumping();
    }

    private void HandleMovement()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            horizontalInput = -moveSpeed;
        }
        else if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
        {
            horizontalInput = moveSpeed;
        }

        rb.velocity = new Vector2(horizontalInput, rb.velocity.y);
    }

    private void HandleJumping()
    {
        bool grounded = IsGrounded();

        if (grounded && Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Apply gravity scaling
        if (!grounded)
        {
            rb.gravityScale = Input.GetKey(jumpKey) ? ogGrav : gravityAmplifier;
        }
        else
        {
            rb.gravityScale = ogGrav;
        }
    }

    private bool IsGrounded()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - playerHeight / 2f);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(origin.x - leftRaycastOffset, origin.y), Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(origin.x + rightRaycastOffset, origin.y), Vector2.down, groundCheckDistance, groundLayer);

        Debug.DrawRay(new Vector2(origin.x - leftRaycastOffset, origin.y), Vector2.down * groundCheckDistance, hit.collider != null ? Color.green : Color.red);
        Debug.DrawRay(new Vector2(origin.x + rightRaycastOffset, origin.y), Vector2.down * groundCheckDistance, hit2.collider != null ? Color.green : Color.red);
        if (hit.collider != null || hit2.collider != null)
        {
            KyoteeTimeLeft = KyoteeTime;
            return hit.collider != null || hit2.collider != null;
        }
        else if (KyoteeTimeLeft > 0)
        {
            KyoteeTimeLeft -= Time.deltaTime;
            return true;
        }
        return false;
       
    }
}
