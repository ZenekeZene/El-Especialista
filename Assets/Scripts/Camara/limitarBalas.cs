using UnityEngine;
using System.Collections;

public class limitarBalas : MonoBehaviour {

	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "bala" || other.tag == "balaEnemigo") {
			Destroy(other.gameObject);
		}
	}
}
