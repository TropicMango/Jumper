using UnityEngine;
using System.Collections;

public class LockScreen : MonoBehaviour {

	public GameObject Player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(Player.transform.position.x,transform.position.y-offset.y,Player.transform.position.z) + offset;
	}
}
