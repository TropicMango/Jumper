using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    private Vector3 pivot;
	private bool OnFloor=true;
    public float speed = 250;
    public float degrees;
	public float Acc = 10F;


    // TODO: Make this cleaner
    private GestureRecognizer.SwipeDirection direction = GestureRecognizer.SwipeDirection.Up;
    //private GestureRecognizer.SwipeDirection queueDirection = GestureRecognizer.SwipeDirection.Up;

    void Start () {
        pivot = new Vector3 (transform.position.x + 2.5F, transform.position.y, transform.position.z);
		direction = GestureRecognizer.SwipeDirection.Up;
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
			if (OnFloor) {
				degrees = 0;
				speed += Acc;
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				if (direction == GestureRecognizer.SwipeDirection.Up) {
					transform.position = new Vector3 (pivot.x + 2.5F, pivot.y, pivot.z);
				} else {
					transform.position = new Vector3 (pivot.x, pivot.y, pivot.z + 2.5F);
				}




				switch (direction) {
				case GestureRecognizer.SwipeDirection.Up:
					pivot = new Vector3 (transform.position.x + 2.5F, 8, transform.position.z);
					break;
				/*case GestureRecognizer.SwipeDirection.Down:
                pivot = new Vector3 (transform.position.x - 2.5F, 8, transform.position.z);
                break;*/
				case GestureRecognizer.SwipeDirection.Left:
					pivot = new Vector3 (transform.position.x, 8, transform.position.z + 2.5F);
					break;
				/*case GestureRecognizer.SwipeDirection.Right:
                pivot = new Vector3 (transform.position.x, 8, transform.position.z - 2.5F);
                break;*/

				default:
					Debug.Log ("??");
					break;
				}
				
//__________________________________Testing Only does not effect phones (I think)_________________________________

				if (Input.anyKey) { // replace with finger down
					direction = GestureRecognizer.SwipeDirection.Left;
					pivot = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 2.5F);
					transform.Rotate (new Vector3 (0, -90, 0));
				} else {
					direction = GestureRecognizer.SwipeDirection.Up;
					pivot = new Vector3 (transform.position.x + 2.5F, transform.position.y, transform.position.z);
				}
			} else {
				direction = GestureRecognizer.SwipeDirection.Down;
			}

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
			this.direction = direction;
        }
    }

    void LateUpdate() { //the update after everything have updated

//        switch (direction) {
//        case GestureRecognizer.SwipeDirection.Up:
//            transform.RotateAround (pivot, Vector3.forward, -speed * Time.deltaTime);
//            break;
//        /*case GestureRecognizer.SwipeDirection.Down:
//            transform.RotateAround (pivot, Vector3.back, -speed * Time.deltaTime); // we do not turn this way
//            break;*/
//        case GestureRecognizer.SwipeDirection.Left:
//            transform.RotateAround (pivot, Vector3.left, -speed * Time.deltaTime);
//            break;
//        /*case GestureRecognizer.SwipeDirection.Right:
//            transform.RotateAround (pivot, Vector3.right, -speed * Time.deltaTime); // we do not turn this way
//            break;*/
//
//        default:
//            Debug.Log ("??");
//            break;
//        }
		//Debug.Log(direction);

		if (direction == GestureRecognizer.SwipeDirection.Down) {
			transform.rotation = Quaternion.identity;
			if (direction == GestureRecognizer.SwipeDirection.Up) {
				transform.Translate (new Vector3 (0, -5 * Time.deltaTime, speed * Time.deltaTime * 0.01F));
			} else {
				transform.Translate (new Vector3 (speed * Time.deltaTime * 0.01F, -5 * Time.deltaTime, 0));
			}
			transform.Rotate (new Vector3 (0, degrees, 0));

		} else { 
			if (direction == GestureRecognizer.SwipeDirection.Up) {
				transform.RotateAround (pivot, Vector3.forward, -speed * Time.deltaTime);
				transform.Rotate (new Vector3 (0, 0, speed * Time.deltaTime));
			} else if (direction == GestureRecognizer.SwipeDirection.Left) {
				transform.RotateAround (pivot, Vector3.left, -speed * Time.deltaTime);
				transform.Rotate (new Vector3 (0, 0, speed * Time.deltaTime));
			}
				
		}
		degrees += speed * Time.deltaTime;
    }


	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Tile")) {
			OnFloor = true;
			//Debug.Log ("ENTER");
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Tile")) {
			OnFloor = false;
		}
	}
}
