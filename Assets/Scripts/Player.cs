using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Animator animator;// StartRun, Jump, Slide, Dead
    
    private PlayerState currentState;
    private bool isGrounded;
    private bool isGameStarted;

    public enum PlayerState
    {
        IDLE,
        RUN,
        JUMP,
        SLIDE,
        DEAD
    }

    private void Start()
    {
        currentState = PlayerState.IDLE;
        isGrounded = true;
        isGameStarted = false;

        TouchManager.onSwipe += TouchManager_onSwipe;
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
        if(isGameStarted)
        {
            GameManager.instance.score += 1;
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        
    }

    public void StartGame()
    {
        isGameStarted = true;
        animator.SetTrigger("StartRun");
        currentState = PlayerState.RUN;
    }

    #region Ações do Jogador
    public void Jump()
    {
        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        animator.SetTrigger("Slide");
        currentState = PlayerState.SLIDE;
        boxCollider2D.size = new Vector2(boxCollider2D.size.x, 2.5f);
        boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, -1f);
        
        yield return new WaitForSeconds(1f);

        currentState = PlayerState.RUN;
        boxCollider2D.size = new Vector2(boxCollider2D.size.x, 4.5f);
        boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, -0.05f);
    }

    private void Dead()
    {
        isGameStarted = false;
        StartCoroutine(GameOver());
    }
    
    private IEnumerator GameOver()
    {
        animator.SetTrigger("Dead");
        currentState = PlayerState.DEAD;
        yield return new WaitForSeconds(1f);
        GameManager.instance.GameOver();
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chao")
            isGrounded = true;

        if (collision.gameObject.tag == "Trap")
            Dead();

        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameManager.instance.score += 100;
        }
    }

    

}
