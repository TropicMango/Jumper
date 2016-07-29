using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	public int AliveTime = 2;
	private float DeathTime = 0;

	// Use this for initialization
	void Start () {
		DeathTime = Time.time + AliveTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (DeathTime < Time.time) {
			transform.Translate (new Vector3 (0, -5 * Time.deltaTime, 0));
		}
		if (transform.position.y < -5) {
			Destroy (this.gameObject);
		}
	}
}
