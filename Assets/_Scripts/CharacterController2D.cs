using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [SerializeField] private float fallMultiplier;								// Gravity multiplier to fall faster
    [SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
    [SerializeField] private float _coyoteTime = 0.15f;                         // Time where the player is able to jump when on air
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck1, m_GroundCheck2;			// A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private bool isFalling = false;
    private float timeOnAir;
    public bool hasJumped;

    
    //TODO:Change Unity Events for normal events / boolean events
    public UnityEvent OnJumpEvent;		//Calls when jumping
    public UnityEvent OnLandEvent;		// Calls when landing on floor
    public UnityEvent OnRunEvent;		//Calls when running
    public UnityEvent OnStopEvent;		// Calls when Stopping running
    
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (OnRunEvent == null)
            OnRunEvent = new UnityEvent();
        if (OnStopEvent == null)
            OnStopEvent = new UnityEvent();
        if (OnJumpEvent == null)
            OnJumpEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck positions hits anything designated as ground

        List<Collider2D> colliders = new List<Collider2D>();
        
        colliders.AddRange(Physics2D.OverlapCircleAll(m_GroundCheck1.position, k_GroundedRadius, m_WhatIsGround)
            .ToList());
        colliders.AddRange(Physics2D.OverlapCircleAll(m_GroundCheck2.position, k_GroundedRadius, m_WhatIsGround)
            .ToList());
        
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                timeOnAir = 0; // We reset the time on air counter
                if (!wasGrounded)
                {
                    hasJumped = false;
                    OnLandEvent.Invoke();
                }
				
            }
        }
        
        if (!m_Grounded) timeOnAir += Time.deltaTime; // We count the time we are on air (coyote time purposes)
        
        Debug.Log(timeOnAir);
        
    }

    public void Move(float moveDir, bool jump)
    {
        // We can only control the player if it is on ground or air control is activated
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(moveDir * 10f, m_Rigidbody2D.velocity.y);

            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity,
                m_MovementSmoothing);


            // If the input is moving the player right and the player is facing left...
            if (moveDir > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (moveDir < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If player is not grounded
        if (!m_Grounded)
        {
            // We check if the player is falling by checking its vertical velocity. If it is less than -0.5, the player is falling.
            isFalling = m_Rigidbody2D.velocity.y < -0.5; 
        
            // If player released the jump button and we are not falling yet, we want to start falling
            if (m_Rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump")) 
            {
                m_Rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier * Time.deltaTime));
            }
        }
  

        
        // If the player should jump...
        if (jump)
        {
            // If we are grounded, our time on air is less than _coyoteTime and we haven't jumped yet since the last time
            // we were on the ground
            if ((m_Grounded || timeOnAir < _coyoteTime) && !hasJumped)
            {
                float compensatingForce = 0;
             
                OnJumpEvent.Invoke(); // We call Jumping event

                hasJumped = true;
                
                if (isFalling)
                {
                    // If we're currentrly falling, we want to compensate the gravity force by adding the current
                    // vertical velocity to our jump force
                    compensatingForce = -m_Rigidbody2D.velocity.y; 
                }
                
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce + compensatingForce), ForceMode2D.Impulse);
            }
        }
        
        // If we are falling, we want to add a force to make us fall faster.
        if (isFalling)
        {
            m_Rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier * Time.deltaTime));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
