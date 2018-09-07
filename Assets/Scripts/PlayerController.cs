using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool doublejump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public float doubleJumpFactor = 1;
	public LayerMask layerMask;


	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	bool IsGrounded() {
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float distance = 1.0f;

		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, layerMask);
		if (hit.collider != null)
		{
			doublejump = false;
			return true;
		}

		return false;
	}
    
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Jump") && IsGrounded())
		{
			jump = true;
		}
		if (Input.GetButtonDown("Jump") && !IsGrounded() && !doublejump)
		{
			doublejump = true;
			rb2d.AddForce(new Vector2(0f, jumpForce * doubleJumpFactor));
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		//anim.SetFloat("Speed", Mathf.Abs(h));

		if (h * rb2d.velocity.x < maxSpeed)
		{
			rb2d.AddForce(Vector2.right * h * moveForce);			
		}

		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
		{
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);			
		}

		//animation
		if (h > 0 && !facingRight)
		{
			Flip ();			
		} else if (h < 0 && facingRight)
		{
			Flip ();			
		}


		if (jump)
		{
			//anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
