using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // <-- Needed for resetting the level

public class PlayerScript : MonoBehaviour
{
    //A bunch of variables
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
    [Tooltip("Machine... I will cut you down, break you apart, splay the gore of your profane form across the STARS! I will grind you down until the very SPARKS CRY FOR MERCY! My hands shall RELISH ENDING YOU... HERE! AND! NOW!")]
    [SerializeField] private float CoyoteTime = 0.2f;

    [Header("Keybinds")]
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    //This is the animator for the player.
    [Tooltip("This is the animator for the player")]
    [SerializeField] Animator PlayerAnimation;

    private Rigidbody2D rb;
    private float ogGrav;
    private float CoyoteTimeLeft = 0f;
    bool isJumping = false;

    // awake
    private void Awake()
    {
        //sets variables
        //done to increase performance
        rb = GetComponent<Rigidbody2D>();
        ogGrav = rb.gravityScale;
    }

    private void Update()
    {
        //simple null check to prevent errors
        if (rb == null)
        {
            Debug.LogError("Player Rigidbody2D is not assigned.");
            return;
        }

        //movement
        HandleMovement();
        //jumping (grav is through unity's rigidbody)
        HandleJumping();

        // Reset level when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = 0f;

        //left movement
        if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            horizontalInput = -moveSpeed;
            PlayerAnimation.SetBool("IsWalking", true);
            PlayerAnimation.SetBool("IsWaiting", false);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //right movement
        else if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
        {
            horizontalInput = moveSpeed;
            PlayerAnimation.SetBool("IsWalking", true);
            PlayerAnimation.SetBool("IsWaiting", false);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            PlayerAnimation.SetBool("IsWalking", false);
            PlayerAnimation.SetBool("IsWaiting", true);
        }

        //actually change the x velocity on the player
        rb.velocity = new Vector2(horizontalInput, rb.velocity.y);
    }

    private void HandleJumping()
    {
        //check if player is grounded
        bool grounded = IsGrounded();

        //jump if the player is grounded and is trying to jump
        if (grounded && Input.GetKeyDown(jumpKey))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            PlayerAnimation.SetBool("IsJumpingAnimation", true);
            PlayerAnimation.SetBool("IsWalkingRight", false);
            PlayerAnimation.SetBool("IsWaiting", false);
        }

        //stops the player 'jumping' after they stop going up
        if (rb.velocity.y < 0f)
        {
            isJumping = false;
        }

        // Apply gravity scaling
        if (!grounded)
        {
            //makes the gravity scale the original gravity if not holding jump
            //otherwise makes them fall faster
            rb.gravityScale = Input.GetKey(jumpKey) ? ogGrav : gravityAmplifier;
            PlayerAnimation.SetBool("IsJumpingAnimation", true);
            PlayerAnimation.SetBool("IsWalkingRight", false);
            PlayerAnimation.SetBool("IsWaiting", false);
        }
        else
        {
            //edge case: player falls at normal speed (usually if falling off a block)
            rb.gravityScale = ogGrav;
            PlayerAnimation.SetBool("IsJumpingAnimation", false);
            PlayerAnimation.SetBool("IsWaiting", true);
        }
    }

    private bool IsGrounded()
    {
        //sets the origin of the raycast to the bottom middle of the player
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - playerHeight / 2f);

        //sets a raycast on both the right/left bottom edge of the player
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(origin.x - leftRaycastOffset, origin.y), Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(origin.x + rightRaycastOffset, origin.y), Vector2.down, groundCheckDistance, groundLayer);

        //debug draw rays: green if hit, red if not
        Debug.DrawRay(new Vector2(origin.x - leftRaycastOffset, origin.y), Vector2.down * groundCheckDistance, hit.collider != null ? Color.green : Color.red);
        Debug.DrawRay(new Vector2(origin.x + rightRaycastOffset, origin.y), Vector2.down * groundCheckDistance, hit2.collider != null ? Color.green : Color.red);

        //returns true if either raycast hit an object.
        if (hit.collider != null || hit2.collider != null)
        {
            CoyoteTimeLeft = CoyoteTime;
            return true;
        }
        //reduces coyote time if not touching collision
        else if (CoyoteTimeLeft > 0)
        {
            CoyoteTimeLeft -= Time.deltaTime;
            //returns true if not jumped yet
            if (!isJumping)
            {
                return true;
            }
        }
        return false;
    }
}
