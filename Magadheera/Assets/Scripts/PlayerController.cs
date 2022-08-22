using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float movementSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private int extraJumpsValue;
    private int extraJumps;

    private bool isGrounded;
    private Rigidbody2D rb2d;
    private Animator anim;

    private void Awake() {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    private void Start() {
        extraJumps = extraJumpsValue;
    }

    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        PlayerMovementAnimation(horizontal, vertical);
        MoveCharacter(horizontal, vertical);
    }

    #region BasicAnimation
    private void PlayerMovementAnimation(float horizontal, float vertical) {
        anim.SetFloat("speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;

        if (horizontal < 0) {
            scale.x = -1f * Mathf.Abs(scale.x);
        } else if (horizontal > 0) {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        if (vertical > 0) {
            anim.SetBool("jump", true);
        } else {
            anim.SetBool("jump", false);
        }

    }
    #endregion

    private void MoveCharacter(float horizontal, float vertical) {
        Vector3 position = transform.position;
        position.x += horizontal * movementSpeed * Time.deltaTime;
        transform.position = position;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded) {
            extraJumps = extraJumpsValue;
        }

        if (vertical > 0 && Input.GetKeyDown(KeyCode.Space) && extraJumps > 0) {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            extraJumps--;

        } else if (vertical > 0 && Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded) {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);

        }

    }















}//Class
