using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerJulian : MonoBehaviour
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
    public float Speed = 10;


    private Rigidbody2D _rb2D;
	public static int numOfJumps;

    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
		numOfJumps = 0;
    }

    float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }


    void Update()
    {
		//Debug.Log("Jumps left: " + numOfJumps);
        //first jump
        if (Input.GetButtonUp("Jump") && numOfJumps>0)   //Dash.dashing)
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
        if (Input.GetButtonUp("Jump") && numOfJumps>0 && !DoubleJump)// && !Dash.dashing)
        {
            //=================add force movement================================            
            if (AddForceMovement)
            {
				DoubleJump = true;
				_rb2D.AddForce(new Vector2(0f, JumpForce * DoubleJumpFactor));
				numOfJumps--;

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
           // Debug.Log("addForce movement");
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
				numOfJumps--;
				Jump = false;
            }
        }
        else
            //=================velocity movement================================
        {
            //Debug.Log("velocity movement");
            _rb2D.velocity =
                Vector2.right * horizontalAxis * Speed * Time.fixedDeltaTime;
        }
    }
}