using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float jumpForce = 10f; // Jump force
    public float groundCheckRadius = 0.1f; // Radius of ground check circle
    public Transform groundCheck; // Object to check for ground
    public LayerMask whatIsGround; // LayerMask for ground

    private Rigidbody2D rb; // Rigidbody2D component
    private bool isGrounded; // Whether the player is grounded or not

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal"); // Get horizontal input
        rb.velocity = new Vector2(move * speed, rb.velocity.y); // Move the player horizontally

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); // Check if the player is grounded
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Check if the player presses the jump key and is grounded
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Apply jump force
            isGrounded = false; // Player is no longer grounded
        }
    }

    public void Activate()
    {

    }
}