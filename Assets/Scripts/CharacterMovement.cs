using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	private Vector3 pivot;
	public int speed = 250;
	public double degrees;

    // TODO: Make this cleaner
    private GestureRecognizer.SwipeDirection direction = GestureRecognizer.SwipeDirection.Up;
    private GestureRecognizer.SwipeDirection queueDirection = GestureRecognizer.SwipeDirection.Up;


   	void Start () {
		pivot = new Vector3 (transform.position.x + 2.5F, transform.position.y, transform.position.z);
	}


    // TODO: Clean up
 	void Update () {
//        if (Input.touchCount == 1) {
//            if (Input.touches [0].phase == TouchPhase.Began || 
//                Input.touches [0].phase == TouchPhase.Stationary) {
//                transform.RotateAround (pivot, Vector3.forward, -speed * Time.deltaTime);
//                if (degrees>180) {
//                  degrees = 0;
//                  transform.position = new Vector3 (pivot.x + 2.5F, pivot.y, pivot.z);
//                  transform.rotation = Quaternion.Euler(new Vector3(0,0,0)); 
//                  pivot = new Vector3 (transform.position.x + 2.5F, transform.position.y, transform.position.z);
//                }
//                degrees += speed * Time.deltaTime;
//                Debug.Log (degrees);
//            }
//        }

        if (degrees >= 180) {
			degrees = 0;
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0)); 

            direction = queueDirection;
        
            switch (direction) {
            case GestureRecognizer.SwipeDirection.Up:
                pivot = new Vector3 (transform.position.x + 2.5F, 8, transform.position.z);
                break;

            case GestureRecognizer.SwipeDirection.Down:
                pivot = new Vector3 (transform.position.x - 2.5F, 8, transform.position.z);
                break;

            case GestureRecognizer.SwipeDirection.Left:
                pivot = new Vector3 (transform.position.x, 8, transform.position.z + 2.5F);
                break;

            case GestureRecognizer.SwipeDirection.Right:
                pivot = new Vector3 (transform.position.x, 8, transform.position.z - 2.5F);
                break;

            default:
                Debug.Log ("??");
                break;

            }

//			if (direction == 0) {
//				transform.position = new Vector3 (pivot.x + 2.5F, pivot.y, pivot.z);
//			} else {
//				transform.position = new Vector3 (pivot.x, pivot.y, pivot.z + 2.5F);
//			}

//			if (Input.anyKey) { // replace with finger down
//				direction = 1;
//                pivot = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 2.5F);
//			} else {
//				direction = 0;
//                pivot = new Vector3 (transform.position.x + 2.5F, transform.position.y, transform.position.z);
//			}
		}

	}

    void OnEnable() {
        Debug.Log ("Enabled");
        GestureRecognizer.OnSwipe += ChangeDirection; 
    }

    void OnDisable() {
        Debug.Log ("Disabled");
        GestureRecognizer.OnSwipe -= ChangeDirection; 

    }

    // TODO: Remove constants (2.5F, 8)
    void ChangeDirection(GestureRecognizer.SwipeDirection direction) {
        if (degrees >= 0 && degrees < 180) {
            queueDirection = direction;
        }
    }

	void LateUpdate(){ //the update after everything have updated
		
        switch (direction) {
        case GestureRecognizer.SwipeDirection.Up:
            transform.RotateAround (pivot, Vector3.forward, -speed * Time.deltaTime);

            break;

        case GestureRecognizer.SwipeDirection.Down:
            transform.RotateAround (pivot, Vector3.back, -speed * Time.deltaTime);

            break;

        case GestureRecognizer.SwipeDirection.Left:
            transform.RotateAround (pivot, Vector3.left, -speed * Time.deltaTime);

            break;

        case GestureRecognizer.SwipeDirection.Right:
            transform.RotateAround (pivot, Vector3.right, -speed * Time.deltaTime);

            break;

        default:
            Debug.Log ("??");
            break;

        }


//        if (direction == 0) {
//			transform.RotateAround (pivot, Vector3.forward, -speed * Time.deltaTime);
//			//transform.Rotate (new Vector3 (0, 0, -speed * Time.deltaTime));
//		}else {
//			transform.RotateAround (pivot, Vector3.left, -speed * Time.deltaTime);
//			//transform.Rotate (new Vector3 (0, 0, -speed * Time.deltaTime));
//		}
//

		degrees += speed * Time.deltaTime;
	}
}
