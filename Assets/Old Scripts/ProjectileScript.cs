using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	public float Speed;
	public GameObject SwordOne;
	public GameObject SwordTwo;
	public bool bounce=false;
	public bool homming=false;
	private bool bounced = false;
	public float turnC = 0.05F;

	// Use this for initialization
	void Start () {
		transform.Rotate (new Vector3 (0, Random.Range(0,360), 0));
		transform.Translate (new Vector3 (0, 0, -40));
		if (homming)
			transform.Rotate (new Vector3 (0, 90, 0));
		transform.Rotate (new Vector3 (90, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (homming) {
			transform.Rotate (new Vector3 (-90, 0, 0));
			/*Vector3 targetDir = new Vector3 (0,1,0) -transform.position;
			float step = Speed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			//Debug.DrawRay (transform.position, newDir, Color.red);
			transform.rotation = Quaternion.LookRotation (newDir);*/

			Vector3 targetDir = new Vector3(0,1,0) - transform.position;
			float turnRate = turnC / Mathf.Pow(Vector3.Distance(new Vector3 (0,1,0),transform.position),0.4F);
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, 0.05F, 0.0F);
			Debug.Log(turnRate);
			transform.rotation = Quaternion.LookRotation (newDir);

			Debug.DrawLine (transform.position, new Vector3 ((transform.position.x * 2), 1, (transform.position.z * 2)));


			transform.Rotate (new Vector3 (90, 0, 0));
		}
		transform.Translate (new Vector3 (0, Speed, 0) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (bounce) {
			if (bounced) {
				if (other.CompareTag ("Wall")) {
					transform.Rotate (new Vector3 (0, 0, 180));
				} else {
					Destroy (gameObject);
				}
			} else {
				transform.Rotate (new Vector3 (0, 0, 180));
				bounced = true;
			}
		} else {
			Destroy (gameObject);
		}

		if (other.CompareTag ("home")) {
			Instantiate (SwordOne);
			Instantiate (SwordTwo);
			Destroy(gameObject);
		}
			
	}
}
