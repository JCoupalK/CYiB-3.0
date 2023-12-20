using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float forwardForce = 735f;
    private float sidewayForce = 60f;
    private float jumpForce = 280f;
    private float maxSpeed = 735f;
    public AudioSource squish;
    public SpawnManager spawnManager;

    private Vector3 jump;
    private bool isGrounded = true;  // Initialize to true
    private bool hasJumped = false; // Track if the player has jumped
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0f, 0.02f, 0f);

        // Apply an initial forward force to "wake up" the Rigidbody
        rb.AddForce(0, 0, 250);
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            hasJumped = true; // Player has initiated a jump
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            float sideForce = Input.GetAxis("Horizontal") * sidewayForce * Time.deltaTime;
            rb.AddForce(sideForce, 0, 0, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        // Limit forward speed
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
        }

        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isGrounded && rb.velocity.y <= 0 && hasJumped)
        {
            isGrounded = true;
            squish.Play();  // Play the squish sound when landing after a jump
            hasJumped = false; // Reset jump flag
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }
}
