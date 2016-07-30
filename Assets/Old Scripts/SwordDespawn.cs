using UnityEngine;
using System.Collections;

public class SwordDespawn : MonoBehaviour {

	public double DespawnTime;
	public double TimeAlive = 0.01;

	// Use this for initialization
	void Start () {
		DespawnTime = Time.time + TimeAlive;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > DespawnTime) {
			Destroy(gameObject);
		}
	}
}
