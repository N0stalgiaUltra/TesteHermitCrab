using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int velocity;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Animator animator;// StartRun, Jump, Slide, Dead
    [SerializeField] private PlayerState currentState;

    [SerializeField] private bool isGrounded;
    private bool isSliding;
    public float contador = 0;

    private void Start()
    {
        velocity = 2;
        currentState = PlayerState.IDLE;
        isGrounded = true;

        Time.fixedDeltaTime = Time.deltaTime * 5f;
        TouchManager.onSwipe += TouchManager_onSwipe;

        StartGame();
    }
    #region Subscribe no evento de swipe

    private void TouchManager_onSwipe(TouchManager.SwipeData obj)
    {
        switch (obj.Direction)
        {
            case TouchManager.SwipeDirection.Up:
                Jump();
                break;
            case TouchManager.SwipeDirection.Down:
                Slide();
                break;
        }
    }

    private void OnDestroy()
    {
        TouchManager.onSwipe -= TouchManager_onSwipe;
    }
    #endregion

    private void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
#endif

    }
    private void FixedUpdate()
    {

        //movimentar aqui

    }

    public void StartGame()
    {
        animator.SetTrigger("StartRun");
        currentState = PlayerState.RUN;
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
            return;

    }
    private void Slide()
    {
        if (isGrounded)
            StartCoroutine(SlideCoroutine());
        else
            return;

    }
    private IEnumerator SlideCoroutine()
    {
        contador += Time.deltaTime;
        animator.SetTrigger("Slide");
        currentState = PlayerState.SLIDE;
        boxCollider2D.size = new Vector2(boxCollider2D.size.x, 2.5f);
        boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, -1f);
        
        yield return new WaitForSeconds(1f);

        currentState = PlayerState.RUN;
        boxCollider2D.size = new Vector2(boxCollider2D.size.x, 4.5f);
        boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, -0.05f);
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
