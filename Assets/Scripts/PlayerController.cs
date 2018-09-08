using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Settings")] public float DoubleJumpFactor = 1;
    public LayerMask LayerMask;
    public bool AddForceMovement;
    public Text DeathText;
    public float DeathZone;

    [Header("Settings For 'AddForce' Movement")]
    public float MoveForce = 365f;

    public float MaxSpeed = 5f;
    public float JumpForce = 1000f;

    [Header("Settings For 'Velocity' Movement")]
    public float Speed = 5;

    private bool _jump;
    private bool _firstJump;
    private bool _secondJump;
    private bool _sphere;

    private int _jumpCounter;
    private Rigidbody2D _rb2D;

    //=======================================================
    //=======================================================
    //=======================================================

    public Material[] Material;
    public float StartDashTime;
    public float DashSpeed;
    public static bool Dashing;

    private Rigidbody2D _player;
    private float _dashTime;
    private bool _canDash;
    private float _xTrans;

    // Use this for initialization
    void Awake()
    {
        DeathText.text = "";
        _player = GetComponent<Rigidbody2D>();
        _dashTime = StartDashTime;
        _canDash = false;
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
            _jumpCounter = 0;
            return true;
        }

        return false;
    }

    // Update is called once per frame

    void Update()
    {
        IsGrounded();
        Debug.Log(_jumpCounter);
        

        if (_jumpCounter < 2)
        {
            //first jump
            if (Input.GetButtonDown("Jump") && _jumpCounter == 0)
            {
                _jumpCounter++;
                _jump = true;
            }
    
            //second jump
            if (Input.GetButtonDown("Jump") && _jumpCounter == 1 && !_jump)
            {
                _secondJump = true;
                _jumpCounter++;
            }
        }


        if (transform.position.y < -DeathZone)
        {
            StartCoroutine(Death());
        }

        //==============================================================
        //==============================================================

        /*
        // actual dash move
        if (Dashing == false && _canDash == true)
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.RightControl))
            {
                Dashing = true;
            }
        }
        else
        {
            if (_dashTime <= 0)
            {
                Dashing = false;
                _dashTime = StartDashTime;
            }
            else
            {
                _dashTime -= Time.deltaTime;
                if (Dashing == true)
                {
                    _player.velocity = Vector2.up * DashSpeed;
                    //PlayerController.numOfJumps = 2;
                }
            }
        }
*/
    }
    
    IEnumerator Death()
    {
        DeathText.text = "YOU DIED";
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene ("03_EndLose");
    }

    void FixedUpdate()
    {
        //===============movement left right====================================
        float horizontalAxis = Input.GetAxis("Horizontal");


        if (horizontalAxis * _rb2D.velocity.x < MaxSpeed)
        {
            _rb2D.AddForce(Vector2.right * horizontalAxis * MoveForce);
        }


        if (Mathf.Abs(_rb2D.velocity.x) > MaxSpeed)
        {
            _rb2D.velocity = new Vector2(Mathf.Sign(_rb2D.velocity.x) * MaxSpeed, _rb2D.velocity.y);
        }


        //=====================movement jump====================================
        if (_jump)
        {
            _rb2D.AddForce(new Vector2(0f, JumpForce));
            _jump = false;
        }

        if (_secondJump)
        {
            _rb2D.AddForce(new Vector2(0f, JumpForce * DoubleJumpFactor));
            _secondJump = false;
        }

//_rb2D.velocity = Vector2.right * horizontalAxis * Speed * Time.fixedDeltaTime;
    }

    //========================================================================================
    //========================================================================================
    //========================================================================================


/*


    // is player inside of dash-sphere?
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("dashSphere"))
        {
            _canDash = true;
            other.gameObject.GetComponent<Renderer>().material = Material[1];
            Debug.Log("Can dash: " + _canDash);
        }

        if (other.gameObject.CompareTag("smallSphere"))
        {
            _canDash = true;
            //dashSpeed = dashSpeed + (dashSpeed * 0.5f);
            other.gameObject.GetComponent<Renderer>().material = Material[3];
            Debug.Log("Can dash: " + _canDash);
        }
    }
*/

    // is player inside of dash-sphere?
    public void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb2D.AddForce(new Vector2(0f, JumpForce));
            _jumpCounter = 0;
        }

        /*

        if (other.gameObject.CompareTag("dashSphere"))
        {
            _canDash = true;
            other.gameObject.GetComponent<Renderer>().material = Material[1];
            Debug.Log("Can dash: " + _canDash);
        }

        if (other.gameObject.CompareTag("smallSphere"))
        {
            _canDash = true;
            //dashSpeed = dashSpeed + (dashSpeed * 0.5f);
            other.gameObject.GetComponent<Renderer>().material = Material[3];
            Debug.Log("Can dash: " + _canDash);
        }*/
    }


/*    // has player left dash-sphere?
    public void OnTriggerExit2D(Collider2D other)
    {
        _sphere = true;
        if (other.gameObject.CompareTag("dashSphere"))
        {
            _canDash = false;
            other.gameObject.GetComponent<Renderer>().material = Material[0];
            Debug.Log("Can dash: " + _canDash);
        }

        if (other.gameObject.CompareTag("smallSphere"))
        {
            other.gameObject.GetComponent<Renderer>().material = Material[2];
            //dashSpeed = dashSpeed - (dashSpeed*0.5f);
        }
    }*/
}