using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	public int AliveTime = 2;
	private bool fall = false;

	// Use this for initialization
	void Start () {
		fall = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fall) {
			transform.Translate (new Vector3 (0, -3 * Time.deltaTime, 0));
		}

//		if (transform.position.y < -5) {
//			Destroy (this.gameObject);
//		}
	}

	void OnTriggerEnter(Collider other) {
        if (other.CompareTag (Constants.PlayerTag)) {
//			fall = true;
		}
	}
       
}
