using UnityEngine;
using System.Collections;

public class GestureRecognizer : MonoBehaviour {

    public enum SwipeDirection {
        Up,
        Down,
        Left,
        Right
    }

    // Delegate
    public delegate void SwipeGesture(SwipeDirection direction);
    public static event SwipeGesture OnSwipe;

    public float swipeThreshold = 1.0f;
    private Vector2 swipeStart = Vector2.zero;
    private Vector2 swipeEnd = Vector2.zero;
    private bool swiping = false;

    void Start () {

    }

    void Update () {
        ProcessSwipe_MouseButton ();

//        if (Input.touchCount == 1) {
//            ProcessSwipe_Touch ();
//        } else {
//            return;
//        }
    }
        
    void ProcessSwipe_MouseButton() {
        if (OnSwipe != null) {
            if (Input.GetMouseButtonDown (0)) {
                swipeStart = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
            } else if (Input.GetMouseButtonUp (0)) {
                swipeEnd = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
                Vector2 direction = swipeEnd - swipeStart;

                direction.Normalize ();

                if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
                    if (direction.x > 0)
                        OnSwipe (SwipeDirection.Right);
                    else
                        OnSwipe (SwipeDirection.Left);
                    
                } else {
                    if (direction.y > 0)
                        OnSwipe (SwipeDirection.Up);
                    else
                        OnSwipe (SwipeDirection.Down);
                    
                    }
            }
        }
    }

    void ProcessSwipe_Touch() {
        if (OnSwipe == null)
            return;
                
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.touches [0];
        if (touch.deltaPosition == Vector2.zero) {
            return;
        }

        Vector2 speedVector = touch.deltaPosition * touch.deltaTime;
        float speed = speedVector.magnitude;

        bool swipeIsActive = (speed > swipeThreshold);

        if (swipeIsActive && !swiping) {
            swipeStart = touch.position;
        } else if (swiping) {
            swipeEnd = touch.position;

            Vector2 direction = swipeEnd - swipeStart;

            // Check if direction is (more) horizontal
            if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
                if (direction.x > 0)
                    OnSwipe (SwipeDirection.Right);
                else
                    OnSwipe (SwipeDirection.Left);

            } else {
                if (direction.y > 0)
                    OnSwipe (SwipeDirection.Up);
                else
                    OnSwipe (SwipeDirection.Down);
            }
            
        }

        swiping = swipeIsActive;
    }
}
