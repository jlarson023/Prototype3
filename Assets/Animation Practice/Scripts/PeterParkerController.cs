using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterParkerController : MonoBehaviour
{
    //Variables
    //movement
    public float moveSpeed;
    public float verticalInput;

    //turning
    public float horizontalInput;
    public float turnSpeed;

    //jumping
    public float jumpForce;
    public KeyCode jumpKey;
    private Rigidbody rb;
    private bool isOnGround = true;

    //attack
    public KeyCode attackKey;
    public KeyCode attackKey2;

    //Animation
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Forward and backward movement
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * moveSpeed);
        animator.SetFloat("verticalInput", Mathf.Abs(verticalInput));
        //turning
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);
        
        //jumping
        if (Input.GetKeyDown(jumpKey) && isOnGround )
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetBool("isOnGround", isOnGround);
        }

        //attack
        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("attack");
        }
        if(Input.GetKeyDown(attackKey2))
        {
            animator.SetTrigger("attack2");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //reset jump
        if(collision.gameObject.CompareTag("Ground")) 
        {
            isOnGround = true;
            animator.SetBool("isOnGround", isOnGround);
        }
    }
}
