using UnityEngine;
using System.Collections;

public class controlLadrillo : MonoBehaviour {

	public int vida = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (other.CompareTag("bala")) {
			Destroy (other);
			rigidbody2D.AddForce(new Vector2(2, 2));
			if (vida > 0){
				vida--;
				return;
			} else if (vida <= 0){
				Destroy (this.gameObject);
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (other.CompareTag("bala")) {
			Destroy (colision.gameObject);
		}
	}
}
