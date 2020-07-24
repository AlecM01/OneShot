using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class pOneCharecterController : MonoBehaviour
{
    #region vSettings
    public float playerSpeed = 2.0f;
    public float jumpHeight = 300.0f;
    public float diveSpeed = 20.0f;
    public float groundDistance = 0.5f;
    public float wallDistance = 0.4f;
    public float wallJumpForce = 10f;
    public float wallJumpHeight = 150f;
    public float wallClingForce = 5;
    public float wallClingDragForce = 1;
    public float stopMoveForce = 10f;
    #endregion

    #region Keys
    public KeyCode jump = KeyCode.W;
    public KeyCode left = KeyCode.A;
    public KeyCode down = KeyCode.S;
    public KeyCode right = KeyCode.D;
    #endregion

    #region tools
    public Rigidbody2D rb;
    #endregion

    #region keyPressed
    bool leftPressed;
    bool downPressed;
    bool rightPressed;
    bool clingingR;
    bool clingingL;
    #endregion

    #region Check Surroundings
    float grounded = 0;
    float walledR = 0;
    float walledL = 0;
    #endregion

    #region Health
    public float health = 1;
    #endregion

    #region stats
    float currentSpeed = 0f;
    float currentFallSpeed = 0f;
    bool rMove;
    bool lMove;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        rb.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        #region Jump
        ///Ground
        if (Input.GetKeyDown(jump) & grounded >= 2)
        {
            rb.AddForce(transform.up * jumpHeight);
            grounded -= 1;
        }

        #region Wall cling
        if (Input.GetKeyDown(jump))
        {
            clingingR = true;
            clingingL = true;
        }
        if (Input.GetKeyUp(jump))
        {
            clingingR = false;
            clingingL = false;
        }
        if (clingingR == true & walledR >= 1)
        {
            rb.AddForce(transform.up * wallClingDragForce);
            rb.AddForce(transform.right * wallClingForce);
        }
        if (clingingL == true & walledL >= 1)
        {
            rb.AddForce(transform.up * wallClingDragForce);
            rb.AddForce(transform.right * wallClingForce * -1);
        }
        #endregion
        #region Wall Jump
        ///Left
        if (Input.GetKeyUp(jump) & walledR >= 1 & rMove == false)
        {
            rb.velocity = new Vector2(-wallJumpForce, 0);
            rb.AddForce(transform.up * wallJumpHeight);
            walledR -= 1;
        }
        ///Right
        if (Input.GetKeyUp(jump) & walledL >= 1 & lMove == false)
        {
            rb.velocity = new Vector2(wallJumpForce, 0);
            rb.AddForce(transform.up * wallJumpHeight);
            walledL -= 1;
        }

        #endregion
        #endregion
        #region left
        if (Input.GetKeyDown(left))
        {
            leftPressed = true;
        }if (Input.GetKeyUp(left))
        {
            leftPressed = false;
            rb.AddForce(transform.right * (-stopMoveForce * currentSpeed));
        }
        if (leftPressed == true)
        {
            rb.AddForce(transform.right * -playerSpeed);
        }
        #endregion
        #region down
        if (Input.GetKeyDown(down))
        {
            downPressed = true;
        }if (Input.GetKeyUp(down))
        {
            downPressed = false;
        }
        if (downPressed == true)
        {
            rb.AddForce(transform.up * -diveSpeed);
        }
        #endregion
        #region right
        if (Input.GetKeyDown(right))
        {
            rightPressed = true;
        }if (Input.GetKeyUp(right))
        {
            rightPressed = false;
            rb.AddForce(transform.right * (-stopMoveForce * currentSpeed));
        }
        if (rightPressed == true)
        {
            rb.AddForce(transform.right * playerSpeed);
        }
        #endregion


    }

    #region PlayerPos
    float playerPosX = 0;
    float playerPosY = 0;
    #endregion

    private void playerPosition()
    {
        playerPosX = rb.position.x;
        playerPosY = rb.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Projectile Collision Check
        if (collision.gameObject.tag == "ProjectileP2")
        {
            print("collide");
            health -= 1;
        }
        #endregion
    }

    #region Dead
    void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    private void FixedUpdate()
    {

        #region controll Pressed
        ///Right
        if (Input.GetKeyDown(right))
        {
            rMove = true;
        }if (Input.GetKeyUp(right))
        {
            rMove = false;
        }
        ///Left
        if (Input.GetKeyDown(left))
        {
            lMove = true;
        }if (Input.GetKeyUp(left))
        {
            lMove = false;
        }
        #endregion

        #region health Check
        if (health == 0)
        {
            Die();
        }
        #endregion

        #region Ground Check
        int layerMask = 1 << 8;
        int layerMaskPellets = 1 << 9;
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, layerMask);
        RaycastHit2D pelletHit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, layerMaskPellets);
        if (groundHit.collider != null || pelletHit.collider != null)
        {
            grounded = 2;
        }
        #endregion
        #region Wall Jump
        int layerMaskTwo = 1 << 8;
        RaycastHit2D wallHitR = Physics2D.Raycast(transform.position, Vector2.right, wallDistance, layerMaskTwo);
        if (wallHitR.collider != null)
        {
            walledR = 1;
        }
        else
        {
            walledR = 0;
        }
        RaycastHit2D wallHitL = Physics2D.Raycast(transform.position, Vector2.right * -1, wallDistance, layerMaskTwo);
        if (wallHitL.collider != null)
        {
            walledL = 1;
        }
        else
        {
            walledL = 0;
        }
        #endregion

        #region speed track
        currentSpeed = rb.velocity.x;
        currentFallSpeed = rb.velocity.y;
        #endregion
        #region Other Track
        #endregion
    }
}
