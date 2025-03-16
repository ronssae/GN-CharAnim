using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private float moveSpeed = 3f;
    private float jumpForce = 4f;
    private float rotationSpeed = 15f;

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded = true;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponent<Animator>();
    } 
    void Update()
    {
        if (!IsOwner) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("isCrouching", true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.SetBool("isCrouching", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
}
