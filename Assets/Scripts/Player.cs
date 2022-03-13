using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int velocity;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerState currentState;

    private bool isGrounded;

    private void Start()
    {
        velocity = 2;
        currentState = PlayerState.IDLE;
        isGrounded = true;
    }

    private void Update()
    {
        //switch (this.currentState)
        //{
        //    case PlayerState.RUN:
        //        break;
        //    case PlayerState.JUMP:
        //        Jump();
        //        break;
        //    case PlayerState.SLIDE:
        //        break;
        //    case PlayerState.DEAD:
        //        break;
        //}
    }

    private void Run()
    {

    }
    public void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocity * Time.deltaTime);
            isGrounded = false;
            currentState = PlayerState.JUMP;
        }
    }
    private void Slide()
    {

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
