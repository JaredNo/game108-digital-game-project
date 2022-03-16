using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float knockbackPower = 100;
    public float knockbackDuration = 1;

    private void Awake() => instance = PlayerMovement;

    public CharacterController2D controller;
    Rigidbody2D rb;
    public Animator Animator;

    float horizontalMove = 0f;
    public float runSpeed = 0;

    bool jump = false;
    public float fallVelocity;

    bool crouch = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player 2")
        {
            StartCoroutine(Player2Movement.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
        }
    }

    public void OnLanding()
    {
        Animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool IsCrouching)
    {
        Animator.SetBool("IsCrouching", IsCrouching);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallVelocity * Time.deltaTime;
        }
    }
    //Knockback code
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            rb.AddForce(-direction * knockbackPower);

        }

        yield return 0;
    }
}