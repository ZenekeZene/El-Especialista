using UnityEngine;
using System.Collections;

public class controlOnda : MonoBehaviour {

	public float segVida;
	void Start () {
		Invoke("morir", segVida);
	}
	
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "enemigo"){
			
			float dirHorizontal = 10 * ((other.gameObject.transform.position.x>transform.position.x)? 1: -1);
			//controlEnemigo scr= other.gameObject.GetComponent<controlEnemigo>();
			//scr.estado = "muertoPorExplosion"; // Cambiar esta chapuza con Notification Center, el resto del proto tb
			other.rigidbody.fixedAngle = false;
			other.rigidbody.AddForce(new Vector2(130, 130));
			
			
			/*other.rigidbody.*/
			//Destroy (other.gameObject);
			//Destroy (gameObject);
		}
	}
	
	void morir(){
		Destroy(gameObject);
	}
}
