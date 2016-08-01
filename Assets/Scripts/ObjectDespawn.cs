using UnityEngine;
using System.Collections;

public class ObjectDespawn : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        Destroy (other.gameObject, 2);
    }
}
