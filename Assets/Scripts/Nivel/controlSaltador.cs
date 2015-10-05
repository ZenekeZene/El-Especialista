using UnityEngine;
using System.Collections;

public class controlSaltador : MonoBehaviour {

	public float fuerzaSaltoX, fuerzaSaltoY;
	
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "prota"){
			other.gameObject.rigidbody2D.velocity = new Vector2(0, 0);
			other.gameObject.rigidbody2D.AddForce (new Vector2(fuerzaSaltoX, fuerzaSaltoY));
		}
	}
}
