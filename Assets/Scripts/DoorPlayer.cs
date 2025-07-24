using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPlayer : MonoBehaviour
{
    public bool hasKey = false;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private float playerHeight = 1.1f;
    [SerializeField] private float leftRaycastOffset = 0.2f;
    [SerializeField] private float rightRaycastOffset = 0.2f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Jumping")]
    [SerializeField] private float gravityAmplifier = 1.5f;
    [SerializeField] private float KyoteeTime = 0.2f;

    [Header("Keybinds")]
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Polish")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip runSound;
    [SerializeField] private ParticleSystem RunParticles;
    [SerializeField] private ParticleSystem JumpParticles;

    [Tooltip("This is the animator for the player")]
    [SerializeField] private Animator PlayerAnimation;

    private Rigidbody2D rb;
    private float ogGrav;
    private float KyoteeTimeLeft = 0f;
    private bool isJumping = false;
    private bool wasGroundedLastFrame = false;
    private AudioSource runAudioSource;

    public bool haltPlayer = false;
    enum State { idle, walk, jumping, falling }
    State state = State.idle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ogGrav = rb.gravityScale;

        runAudioSource = gameObject.AddComponent<AudioSource>();
        runAudioSource.clip = runSound;
        runAudioSource.loop = true;
        runAudioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (haltPlayer) 
            return;
        if (rb == null)
        {
            Debug.LogError("Player Rigidbody2D is not assigned.");
            return;
        }

        HandleMovement();
        HandleJumping();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Handle landing sound
        bool grounded = IsGrounded();
        if (!wasGroundedLastFrame && grounded)
        {
            if (landSound != null)
            {
                AudioSource.PlayClipAtPoint(landSound, transform.position);
            }
        }
        wasGroundedLastFrame = grounded;
    }



    ParticleSystem psToBeMade = null;
    [SerializeField] int frameSkip = 3;
    int framesLeftBeforePS = 3;

    private void FixedUpdate()
    {
        if (haltPlayer)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            return;

        }
        if (psToBeMade != null && framesLeftBeforePS <= 2 && IsGrounded())
        {
            Instantiate(psToBeMade, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
            framesLeftBeforePS = frameSkip;
        }
        else
        {
            framesLeftBeforePS--;
        }
    }

    private void HandleMovement()
    {
        State temp = state;
        float horizontalInput = 0f;

        if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            horizontalInput = -moveSpeed;

        }
        else if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
        {
            horizontalInput = moveSpeed;

        }
        else
        {
            psToBeMade = null;
        }

        bool isGroundedNow = IsGrounded();
        bool isMovingHorizontally = Mathf.Abs(horizontalInput) > 0.1f;

        if (isGroundedNow && isMovingHorizontally)
        {
            if (!runAudioSource.isPlaying)
            {
                runAudioSource.Play();
            }
        }
        else
        {
            if (runAudioSource.isPlaying)
            {
                runAudioSource.Stop();
            }
        }


        rb.velocity = new Vector2(horizontalInput, rb.velocity.y);
    }

    private void HandleJumping()
    {
        bool grounded = IsGrounded();

        if (grounded && Input.GetKeyDown(jumpKey))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (jumpSound != null)
            {
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
        }

        if (rb.velocity.y < 0f)
        {
            isJumping = false;
        }

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
            return true;
        }
        else if (KyoteeTimeLeft > 0)
        {
            KyoteeTimeLeft -= Time.deltaTime;
            if (!isJumping)
            {
                return true;
            }
        }
        return false;
    }
}
