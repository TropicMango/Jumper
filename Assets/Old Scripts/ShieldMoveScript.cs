using UnityEngine;
using System.Collections;

public class ShieldMoveScript : MonoBehaviour {

	public float RotationSpeed = 5000;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update(){
		if (Input.GetKey (KeyCode.A)) {
			transform.RotateAround (Vector3.zero, Vector3.up, -RotationSpeed * Time.deltaTime);
		}else if (Input.GetKey (KeyCode.D)) {
			transform.RotateAround (Vector3.zero, Vector3.up, RotationSpeed * Time.deltaTime);
		}
			
		//transform.RotateAround (Vector3.zero, Vector3.up, -20 * Time.deltaTime);
	}
}
