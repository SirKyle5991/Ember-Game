using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int MAX_JUMPS = 2;
    
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundContactLayers;
    [SerializeField] float jumpingPower = 4f;
    [SerializeField] private float playerAcceleration = 10;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D playerCollisionBounds;
     
    public bool grounded;

    private bool canDash = true;
    private bool isDashing;
    private bool isJumping;
    private float dashingPower = 13f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 3f;
    private float horizontalInput;

    [SerializeField] private int remainingJumps = 2;

    private void Awake()
    {
        // grab references for rigidBody and animator for the object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCollisionBounds = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        var playerHorizontalMovement = Mathf.MoveTowards(body.velocity.x, horizontalInput * speed,
            playerAcceleration * Time.fixedDeltaTime);
        
        body.velocity = new Vector2(playerHorizontalMovement, body.velocity.y);
        
        var groundHit = Physics2D.BoxCast((Vector2)transform.position + playerCollisionBounds.offset, playerCollisionBounds.size * 0.95f, 0, Vector2.down, 0.05f, layerMask:groundContactLayers);
        var isGroundStable = groundHit && !groundHit.collider.isTrigger && groundHit.distance < playerCollisionBounds.size.y / 2f + float.Epsilon;
        
        var leftWallHit = Physics2D.BoxCast((Vector2)transform.position + playerCollisionBounds.offset, playerCollisionBounds.size * 0.95f, 0, Vector2.left, 0.05f, layerMask:groundContactLayers);
        var isLeftWallStable = leftWallHit && !leftWallHit.collider.isTrigger && leftWallHit.distance < playerCollisionBounds.size.x / 2f + float.Epsilon;
        
        var rightWallHit = Physics2D.BoxCast((Vector2)transform.position + playerCollisionBounds.offset, playerCollisionBounds.size * 0.95f, 0, Vector2.right, 0.05f, layerMask:groundContactLayers);
        var isRightWallStable = rightWallHit && !rightWallHit.collider.isTrigger && rightWallHit.distance < playerCollisionBounds.size.x / 2f + float.Epsilon;

        
        if (isGroundStable)
        {
            if (!grounded)
            {
                anim.ResetTrigger("jump");
            }

            grounded = true;
            remainingJumps = MAX_JUMPS;
        }
        else
        {
            grounded = false;
        }

        
        
        if (isJumping)
        {
            if(grounded || (remainingJumps > 0 && !isRightWallStable && !isLeftWallStable))
            {
                anim.SetTrigger("jump");
                body.velocity = new Vector2(body.velocity.x, jumpingPower);
                remainingJumps--;
                grounded = false;
            }
            else
            {
                if (isLeftWallStable)
                {
                    body.velocity = new Vector2(1, 1).normalized * jumpingPower;
                }
                else if (isRightWallStable)
                {
                    body.velocity = new Vector2(-1, 1).normalized * jumpingPower;
                }
            }
        }

        isJumping = false;
        anim.SetBool("grounded", grounded);
    }

    public void OnJump()
    {
        isJumping = true;
    }


    
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        
        horizontalInput = Input.GetAxis("Horizontal");
        

        //flips the player when moving left and right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;

        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        //set animator parameters
        anim.SetBool("Run", horizontalInput != 0);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, 5);
        anim.SetTrigger("jump");
        grounded = false;
    }
    

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
