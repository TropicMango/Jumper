using UnityEngine;
using System.Collections;

public class Overseer : MonoBehaviour {

	public GameObject FloorBlock;
	private Vector3 CurrentLoc;
	private float cooldown = 0.61F;
	public float NextGen = 0;

    // TODO: Global / static variables for tile height and widgth
	void Start () {
		CurrentLoc = new Vector3 (0, 5, 0);
		Instantiate (FloorBlock, CurrentLoc, Quaternion.identity);
		CurrentLoc = new Vector3 (5, 5, 0);
		Instantiate (FloorBlock, CurrentLoc, Quaternion.identity);
		CurrentLoc = new Vector3 (10, 5, 0);
		for (int i = 0; i < 4; i++) { // increase this for more inital blocks
			GenerateBlock ();
		}
	}
	
	void Update () {
		if (Time.time > NextGen) {
			GenerateBlock ();
		}
	}

	void GenerateBlock(){
		Instantiate (FloorBlock, CurrentLoc, Quaternion.identity);
		if (Random.Range (0, 2) == 0) {
			CurrentLoc.x += 5;
		} else {
			CurrentLoc.z += 5;
		}
		NextGen = Time.time + cooldown;
	}

}
