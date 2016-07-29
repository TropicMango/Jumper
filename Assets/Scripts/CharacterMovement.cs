using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	private Vector3 pivot;
	public int speed = 250;
	public double degrees;
	private int direction = 0; // 0 = straight, 1 = side;


	// Use this for initialization
	void Start () {
		pivot = new Vector3 (transform.position.x + 2.5F, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
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
			if (direction == 0) {
				transform.position = new Vector3 (pivot.x + 2.5F, pivot.y, pivot.z);
			} else {
				transform.position = new Vector3 (pivot.x, pivot.y, pivot.z + 2.5F);
			}

			if (Input.anyKey) { // replace with finger down
				direction = 1;
				pivot = new Vector3 (transform.position.x, 8, transform.position.z + 2.5F);
			} else {
				direction = 0;
				pivot = new Vector3 (transform.position.x + 2.5F, 8, transform.position.z);
			}
		}
	}

	void LateUpdate(){ //the update after everything have updated
		if (direction == 0) {
			transform.RotateAround (pivot, Vector3.forward, -speed * Time.deltaTime);
			//transform.Rotate (new Vector3 (0, 0, -speed * Time.deltaTime));
		}else {
			transform.RotateAround (pivot, Vector3.left, -speed * Time.deltaTime);
			//transform.Rotate (new Vector3 (0, 0, -speed * Time.deltaTime));
		}
		degrees += speed * Time.deltaTime;
		//Debug.Log (degrees);
	}
}
