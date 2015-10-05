using UnityEngine;
using System.Collections;

public class controlTorre : MonoBehaviour {

	private controlGeneral gen;
	private utils u;

	void Awake(){
		gen = GetComponent<controlGeneral>();
		u = GetComponent<utils>();
	}

	void Start(){
		NotificationCenter.DefaultCenter().AddObserver(this, "empezarTorre");
		NotificationCenter.DefaultCenter().AddObserver(this, "acabarTorre");
	}

	public void empezarTorre(Notification notificacion){
		rigidbody2D.isKinematic = true;
		u.moverApunto(transform, "nodoTorreBase", 1, new Vector2(0, 0), "transicionAtorreBaseTerminada");
	}

	public void transicionAtorreBaseTerminada() {
		u.moverApunto(transform, "nodoTorre", 1, new Vector2(0, 0), "transicionAtorreArribaTerminada");
		
	}
	
	public void transicionAtorreArribaTerminada() {
		gen.estado = 3;
		gen.animator.SetInteger("Estado", gen.estado);
		rigidbody2D.isKinematic = false;
		rigidbody2D.velocity = new Vector2(0, 0);
	}

	public void acabarTorre(Notification notification){
		u.moverApunto(transform, "nodoTorreBase", 1, new Vector2(0, 0), "torreTerminada");
	}

	private void torreTerminada(){
		gen.estado = 1;
		gen.animator.SetInteger("Estado", gen.estado);
		rigidbody2D.isKinematic = false;
		gen.girarseDerecha();
	}
}
