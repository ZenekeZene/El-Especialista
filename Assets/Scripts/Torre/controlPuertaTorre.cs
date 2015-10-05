using UnityEngine;
using System.Collections;

public class controlPuertaTorre : MonoBehaviour {

	public float delay;
	private bool estaSiendoAtacada = false;
	
	void Start () {
		
	}
	
	void OnCollisionStay2D(Collision2D colision){
		if (estaSiendoAtacada == false){
			GameObject other = colision.gameObject;
			if (other.CompareTag("enemigo")){ // y volador? no ataca puerta pero...cuidado
				estaSiendoAtacada = true;
				Invoke ("sigueTocando", delay);
			}
		}
	}
	
	private void sigueTocando(){
		if (estaSiendoAtacada == true){
			NotificationCenter.DefaultCenter().PostNotification(this, "restarVidaTorre");
			Invoke ("sigueTocando", delay);
		}
	}
	
	void OnCollisionExit2D(Collision2D colision){
		estaSiendoAtacada = false;
	}

}
