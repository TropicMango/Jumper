using UnityEngine;
using System.Collections;

public class GestureRecognizer : MonoBehaviour {

    public float swipeThreshold = 1.0f;
    private Vector2 swipeStart = Vector2.zero;
    private Vector2 swipeEnd = Vector2.zero;
    private bool swipeWasActive = false;

    void Start () {

    }

    void Update () {
        if (Input.touchCount == 1) {
            ProcessSwipe ();
        }
    }

    void ProcessSwipe() {
        if (Input.touchCount != 1) {
            return;
        }

        Touch touch = Input.touches [0];
        if (touch.deltaPosition == Vector2.zero) {
            return;
        }

        Vector2 speedVector = touch.deltaPosition * touch.deltaTime;
        float speed = speedVector.magnitude;

        bool swipeIsActive = (speed > swipeThreshold);

        if (swipeIsActive) {
            if (!swipeWasActive) {
                swipeStart = touch.position;
            }
        } else {
            if (swipeWasActive) {
                swipeEnd = touch.position;
                Debug.Log("Swipe Complete");
            }
        }

        swipeWasActive = swipeIsActive;
    }
}
