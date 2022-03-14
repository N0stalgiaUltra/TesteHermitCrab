using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int velocity;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;// StartRun, Jump, Slide, Dead
    [SerializeField] private PlayerState currentState;

    [SerializeField] private bool isGrounded;

    private void Start()
    {
        velocity = 2;
        currentState = PlayerState.IDLE;
        isGrounded = true;

        Time.fixedDeltaTime = Time.deltaTime * 5f;

    }

    private void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
#endif

    }
    private void FixedUpdate()
    {
        //if(currentState == PlayerState.RUN)
        if (isGrounded)
        {
            animator.SetTrigger("StartRun");
            currentState = PlayerState.RUN;
        }
        //rb.velocity = new Vector2(velocity * Time.time * .15f, rb.velocity.y); //movimento do player
    }

    public void Jump()
    {
        if (isGrounded)
        {
            //animator.SetBool("Jump", true);
            animator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            isGrounded = false;
            currentState = PlayerState.JUMP;
            
        }
        else
        {
            //print("não grounded");
        }

    }
    private void Slide()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chao")
            isGrounded = true;
    }

    public enum PlayerState
    {
        IDLE,
        RUN,
        JUMP,
        SLIDE,
        DEAD
    }

    public PlayerState CurrentState { get => this.currentState; set => value = this.currentState;}
}
