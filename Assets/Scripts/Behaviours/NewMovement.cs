using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    [Space]
    public float checkDistance;
    public Transform groundCheck;
    public LayerMask groundMask;
    Rigidbody rb;

    [Space]
    public Transform playerMesh;

    [Space]
    public bool canJump;
    public bool isJumping, isJumpingAlt, isGrounded = false;
    public bool canMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        isJumping = Input.GetButton("Jump");

        Cursor.lockState = CursorLockMode.Locked;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);

        if (moveDirection != new Vector3(0, 0, 0))
        {
            playerMesh.rotation = Quaternion.LookRotation(moveDirection);
        }

    }

    private void Update()
    {
        //canJump = Physics.CheckSphere(groundCheck.position, checkDistance, groundMask);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector3.up * jumpForce;
        }
    }

    void OnGroundCheck()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, checkDistance);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
