using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool FacingRight = true;
    [HideInInspector] public bool Jump;
    [HideInInspector] public bool DoubleJump;

    [Header("Basic Settings")] public float DoubleJumpFactor = 1;
    public LayerMask LayerMask;
    public bool AddForceMovement;

    [Header("Settings For 'AddForce' Movement")]
    public float MoveForce = 365f;

    public float MaxSpeed = 5f;
    public float JumpForce = 1000f;

    [Header("Settings For 'Velocity' Movement")]
    public float Speed = 5;


    private Rigidbody2D _rb2D;


    // Use this for initialization
    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, LayerMask);
        if (hit.collider != null)
        {
            DoubleJump = false;
            return true;
        }

        return false;
    }

    float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    // Update is called once per frame

    void Update()
    {
        //Debug.Log(CalculateJumpForce(Physics2D.gravity.magnitude,5f));
        //Debug.Log(JumpForce);

        //first jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //=================add force movement================================            
            if (AddForceMovement)
            {
                Jump = true;
            }
            else
                //=================velocity movement================================
            {
                //todo
            }
        }

        //second jump
        if (Input.GetButtonDown("Jump") && !IsGrounded() && !DoubleJump)
        {
            //=================add force movement================================            
            if (AddForceMovement)
            {
                DoubleJump = true;
                _rb2D.AddForce(new Vector2(0f, JumpForce * DoubleJumpFactor));
            }
            else
                //=================velocity movement================================            
            {
                //todo
            }
        }
    }

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");

        //=================add force movement================================

        if (AddForceMovement)
        {
            Debug.Log("addForce movement");
            if (horizontalAxis * _rb2D.velocity.x < MaxSpeed)
            {
                _rb2D.AddForce(Vector2.right * horizontalAxis * MoveForce);
            }

            if (Mathf.Abs(_rb2D.velocity.x) > MaxSpeed)
            {
                _rb2D.velocity = new Vector2(Mathf.Sign(_rb2D.velocity.x) * MaxSpeed, _rb2D.velocity.y);
            }


            if (Jump)
            {
                _rb2D.AddForce(new Vector2(0f, JumpForce));
                Jump = false;
            }
        }
        else
            //=================velocity movement================================
        {
            Debug.Log("velocity movement");
            _rb2D.velocity =
                Vector2.right * horizontalAxis * Speed * Time.fixedDeltaTime;
        }
    }
}