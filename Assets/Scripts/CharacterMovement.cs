using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public enum Direction {
    Up,
    Left
}

public class CharacterMovement : MonoBehaviour {
    private Vector3 pivot;
    public float speed = 250;
    public float degrees;
    public float acceleration = 10F;

    private bool isGameOver  = false;
    private bool OnTile      = true;

    public delegate void GameOver ();
    public static event GameOver OnGameOver;

    private Direction direction = Direction.Up;

    void Start () {

        UpdatePivot ();
    }

    void UpdateTransform() {
        if (direction == Direction.Up) {
			transform.position = new Vector3 (pivot.x + Constants.TileStep, Constants.PlayerAltitude, pivot.z);
        } else if (direction == Direction.Left) {
			transform.position = new Vector3 (pivot.x, Constants.PlayerAltitude, pivot.z + Constants.TileStep);
        }
    }

    void UpdatePivot() {
        if (direction == Direction.Up) {
            pivot = new Vector3 (transform.position.x + Constants.TileStep, 
								Constants.PlayerAltitude, transform.position.z);
        } else if (direction == Direction.Left) {
			pivot = new Vector3 (transform.position.x, Constants.PlayerAltitude, 
                                 transform.position.z + Constants.TileStep);
        }
    }

    void Update () {
        if (isGameOver) {
            if(Input.touchCount == 1 || Input.anyKey)
                SceneManager.LoadScene("Game_Main");
            return;
        }
           
        if (degrees >= 180) {
            if (OnTile) {
                degrees = 0;

                transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
                speed += acceleration;

                UpdateTransform ();
//                UpdatePivot ();

                if (Input.touchCount > 0) {
                    Touch touch = Input.GetTouch (0);
                    if (touch.phase == TouchPhase.Began
                        || touch.phase == TouchPhase.Stationary
                        || touch.phase == TouchPhase.Moved) {
                        direction = Direction.Left;
                        //transform.Rotate (new Vector3 (0, -90, 0));
                    } else {
                        direction = Direction.Up;
                    }
                }

                // Testing
               
                if (Input.anyKey) { // replace with finger down
                    direction = Direction.Left;
                    transform.Rotate (new Vector3 (0, -90, 0));
                } else {
                    direction = Direction.Up;
                }

                UpdatePivot ();

            } else {
                Death ();
            }

        }

    }

    void Death() {
        isGameOver = true;
        if (OnGameOver != null) {
            OnGameOver ();
        }

    }

    void LateUpdate() {

        if (isGameOver) {
            transform.rotation = Quaternion.identity;
            if (direction == Direction.Up) {
                transform.Translate (new Vector3 (0, -5 * Time.deltaTime, -speed * Time.deltaTime * 0.01F));
            } else {
                transform.Translate (new Vector3 (-speed * Time.deltaTime * 0.01F, -5 * Time.deltaTime, 0));
            }
            transform.Rotate (new Vector3 (0, degrees, 0));

        } else { 
            if (direction == Direction.Up) {
                transform.RotateAround (pivot, Vector3.forward, -speed * Time.deltaTime);
                transform.Rotate (new Vector3 (0, 0, speed * Time.deltaTime));
            } else if (direction == Direction.Left) {
                transform.RotateAround (pivot, Vector3.left, -speed * Time.deltaTime);
                transform.Rotate (new Vector3 (0, 0, speed * Time.deltaTime));
            }

        }
        degrees += speed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other) {
        if (other.CompareTag (Constants.TileTag)) {
            OnTile = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag (Constants.TileTag)) {
            OnTile = false;
        }
    }

//    void OnEnable() {
//        Debug.Log ("Enabled");
//        GestureRecognizer.OnSwipe += ChangeDirection;
//    }
//
//    void OnDisable() {
//        Debug.Log ("Disabled");
//        GestureRecognizer.OnSwipe -= ChangeDirection;
//    }
//
//    void ChangeDirection(GestureRecognizer.SwipeDirection direction) {
//        if (degrees >= 0 && degrees < 180) {
//            this.direction = direction;
//        }
//    }
}
