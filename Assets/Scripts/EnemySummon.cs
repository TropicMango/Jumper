using UnityEngine;
using System.Collections;

public class EnemySummon : MonoBehaviour {
	public GameObject Spear;
	public GameObject CannonBall;
	public GameObject BouncyCube;
	public GameObject Rocket;
	public double delay = 1;
	public double FireTime=1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			transform.RotateAround (transform.position, Vector3.left, -90 * Time.deltaTime);
		}else if (Input.GetKey (KeyCode.D)) {
			transform.RotateAround (Vector3.zero, Vector3.up, 90 * Time.deltaTime);
		}
		//transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
		/*if (Time.time > FireTime) {
			//Instantiate (Spear, transform.position, transform.rotation);
			//Instantiate (CannonBall, transform.position, transform.rotation);
			Instantiate (BouncyCube, transform.position, transform.rotation);
			//Instantiate (Rocket, transform.position, transform.rotation);
			FireTime = Time.time + delay;
		}*/
	}

}
