using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2.5f;

    public float autoTurnAngle = 45f;
    public float attackRotateSpeed = 540f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody rb;
    private Vector2 moveInput;
    private Transform cameraTransform;

    private Animator anim;

    private Character character;

    private LockOnSystem lockOnSystem;

    private void Start()
    {
        lockOnSystem = GetComponent<LockOnSystem>();
        character = GetComponent<Character>();
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
        moveSpeed = character.Stats.GetStat(StatType.MoveSpeed).Value;
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
        if (character.IsAttacking) return;

        if (lockOnSystem != null && lockOnSystem.IsLocked)
        {
            MoveLocked();
        }
        else
        {
            MoveFree();
        }
    }

    void MoveFree()
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

    void MoveLocked()
    {
        Transform target = lockOnSystem.CurrentTarget;

        if (target == null) return;

        //锁定时角色始终朝向目标
        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 moveDir = forward * moveInput.y + right * moveInput.x;
        if (moveDir.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }

    public void OnJump()
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

        if (lockOnSystem != null && lockOnSystem.IsLocked)
        {
            anim.SetFloat("MoveX", moveInput.x);
            anim.SetFloat("MoveY", moveInput.y);
        }
        else
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", moveInput.magnitude);
        }
    }

    //攻击时自动转向
    public void RotateToAttackTarget(List<Collider> targets)
    {
        if (!character.IsAttacking) return;

        Transform target = null;

        target = GetNearestTarget(targets);

        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        float angleToTarget = Vector3.Angle(transform.forward, dir);
        if (angleToTarget > autoTurnAngle) return;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, attackRotateSpeed);
    }

    Transform GetNearestTarget(List<Collider> targets)
    {
        Transform nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < nearestDistance)
            {
                nearest = target.transform;
                nearestDistance = distance;
            }
        }

        return nearest;
    }

    public bool CanAttack()
    {
        return isGrounded && !character.IsAttacking;
    }
}
