using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public float pixelDistToDetect = 20f;
    private Vector2 fingerUpPos;
    private Vector2 fingerDownPos;

    public static event Action<SwipeData> onSwipe = delegate { };

    private bool releaseSwipe = false;

    [SerializeField] private Player playerRef;
    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
                GameManager.instance.GameStart();
            }
            if (!releaseSwipe && touch.phase == TouchPhase.Moved)
            {
                fingerDownPos = touch.position;
                DetectSwipe();
            }
            if(touch.phase == TouchPhase.Ended)
            {
                fingerDownPos = touch.position;
                DetectSwipe();
            }
        }   
    }

    private void DetectSwipe()
    {
        if(CheckDistance())
        {
            SwipeDirection direction = fingerDownPos.y - fingerUpPos.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            SendSwipe(direction);
        }
    }
    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData data = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPos,
            EndPosition = fingerDownPos
        };

        onSwipe(data); 
    }

    private bool CheckDistance()
    {
        return VerticalMoveDistance() > pixelDistToDetect;
    }

    private float VerticalMoveDistance()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    public enum SwipeDirection
    {
        Up,Down
    }
    public struct SwipeData
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public SwipeDirection Direction;
    }
}
