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
    private Animator animator;

    //Particles
    public ParticleSystem rightDustCloud;
    public ParticleSystem leftDustCloud;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rightDustCloud.Stop();
        leftDustCloud.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //Forward and backward movement
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * moveSpeed);
        //walk animation
        animator.SetFloat("verticalInput", Mathf.Abs(verticalInput));
        //Activate or deactivate dust cloud
        if (verticalInput > 0 && (!rightDustCloud.isPlaying || !leftDustCloud.isPlaying))
        {
            rightDustCloud.Play();
            leftDustCloud.Play();
        }
        else if (verticalInput <= 0)
        {
            rightDustCloud.Stop();
            leftDustCloud.Stop();
        }


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
