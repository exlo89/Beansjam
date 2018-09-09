using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Settings")] public LayerMask LayerMask;
    public Text DeathText;
    public float DeathZone;

    [Header("Settings For 'Movement' Movement")]
    public float MoveForce = 365f;

    public float MaxSpeed = 5f;
    public float Speed = 5;

    [Header("Settings For 'Jumping' Movement")]
    public float JumpForce = 300f;

    public float DoubleJumpFactor = 1;
    public float SphereForce = 2;

    [HideInInspector] public bool WinGame =true;

    private bool _jump;
    private bool _firstJump;
    private bool _secondJump;
    private bool _sphere;
    private bool _canMove = true;

    private int _jumpCounter;
    private Rigidbody2D _rb2D;


    // Use this for initialization
    void Awake()
    {
        DeathText.text = "";
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

        if (Input.GetButtonDown("Jump") && _canMove)
        {
            if (_jumpCounter < 2)
            {
                _jump = true;
                _jumpCounter++;
            }
        }

        if (transform.position.y < -DeathZone)
        {
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Top")
        {
            WinGame = true;
            StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        DeathText.text = "YOU WIN";
        yield return new WaitForSeconds(1);
        _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("02_EndWin");
    }

    IEnumerator Death()
    {
        _canMove = false;
        DeathText.text = "YOU DIED";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("03_EndLose");
    }

    
    void FixedUpdate()
    {
        //===============movement left right====================================
        if (_canMove)
        {
            float horizontalAxis = Input.GetAxis("Horizontal");

            if(horizontalAxis > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if(horizontalAxis < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (horizontalAxis * _rb2D.velocity.x < MaxSpeed)
            {
                //_rb2D.velocity = Vector2.right * horizontalAxis * MoveForce;
                _rb2D.AddForce(Vector2.right * horizontalAxis * 20000 * MoveForce);
            }
        }


        if (Mathf.Abs(_rb2D.velocity.x) > MaxSpeed)
        {
            _rb2D.velocity = new Vector2(Mathf.Sign(_rb2D.velocity.x) * MaxSpeed, _rb2D.velocity.y);
        }


        //=====================movement jump====================================
        if (_jump)
        {
            float jumpFactor = _jumpCounter == 2 ? DoubleJumpFactor : 1;
            _rb2D.velocity = Vector2.up * JumpForce * jumpFactor * Speed * Time.fixedDeltaTime;
            _jump = false;
        }
    }

    // is player inside of dash-sphere?
    public void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb2D.velocity = Vector2.up * JumpForce * SphereForce * Speed * Time.fixedDeltaTime;
            _jumpCounter = 0;
        }

    }

}