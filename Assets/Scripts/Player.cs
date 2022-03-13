using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int velocity;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        velocity = 2;
    }

    private void Update()
    {
        //rb.velocity = new Vector2(velocity, 0);


        //foreach (Touch touch in Input.touches)
        //{
        //    if ()
        //}
    }

    private void DetectSwipe()
    {
        //if(CheckDistance())

    }
}
