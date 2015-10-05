using UnityEngine;
using System.Collections;

public class controlCadenaTorre : MonoBehaviour {

	void Start () {
	
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (other.CompareTag("bala")) {
			Destroy (colision.gameObject);
			Destroy (this.gameObject);
		}
	}
}
