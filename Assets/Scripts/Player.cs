using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //  MOVEMENT & JUMPS
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private int maxJumps = 2;
    private int jumpsRemaining;
    
    private float hInput;
    private Rigidbody2D rb;
    
    
    
   
    
    //  ANIMATION & PARTICLES
    public ParticleSystem SmokeFX;
    private Animator anim;

    //  GROUND CHECK
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;
    private bool isGrounded;
    
    //  WALL CHECK
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask wallLayer;
    private bool isWalled;
    
    //  WALL MOVEMENT
    private float wallSlideSpped = 2;
    private bool isWallSliding;
    

    private void Awake() //Awake used to prepare the "things" for the object that owns the script
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        //Inputs are read in the update method
        hInput = Input.GetAxisRaw("Horizontal"); //-1, 0, 1
       

        anim.SetFloat("xSpeed", Mathf.Abs(hInput));
        anim.SetFloat("ySpeed", rb.linearVelocityY);
        FaceMovement();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsRemaining > 0)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpsRemaining--;
                SmokeFX.Play();
            }

            
        }

        GroundCheck();
    }
    

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    /*private void ProcessWallSlide()
    {
        if (!isGrounded && WallCheck() && )
        {
            
        }
    }
    */

    private void FaceMovement()
    {
        if (hInput < 0 && transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            SmokeFX.Play();
        }
        
        else if (hInput > 0)
        {
            transform.eulerAngles = Vector3.zero;
            SmokeFX.Play();
        }

       
    }
    void FixedUpdate()
    {
        //Constant forces should be applied in the fixedUpdate Method
        rb.linearVelocity = new Vector2(hInput * movementSpeed, rb.linearVelocityY);
    }

    private void GroundCheck()
    {
        bool groundedNow = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
        if (groundedNow && !isGrounded)
        {
            jumpsRemaining = maxJumps;
        }

        isGrounded = groundedNow;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}
