using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody rb;
    private Vector2 moveInput;
    private Transform cameraTransform;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckGround();
        UpdateAnimation();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        ApplyCustomGravity();
        MovePlayer();
    }

    void MovePlayer()
    {
        //计算相机水平朝向
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * moveInput.y + right * moveInput.x;
        if (moveDir.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);

            //角色转向移动方向
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (anim)
            {
                anim.SetTrigger("Jump");
            }
        }
    }

    //使角色下落地更快
    void ApplyCustomGravity()
    {
        if (rb.linearVelocity.y < 0 && !isGrounded)
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    void UpdateAnimation()
    {
        if (!anim) return;

        Vector3 horizontalVelocity = rb.linearVelocity;
        horizontalVelocity.y = 0f;
        float currentSpeed = horizontalVelocity.magnitude;

        if (moveInput.magnitude < 0.1f)
        {
            currentSpeed = 0f;
        }

        anim.SetFloat("Speed", currentSpeed);
    }
}
