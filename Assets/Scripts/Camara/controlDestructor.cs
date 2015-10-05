using UnityEngine;
using System.Collections;

public class controlDestructor : MonoBehaviour {

	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		GameObject otro = other.gameObject;
		if (otro.CompareTag("enemigo")){
			controlGeneralEnemigo cntGeneral = otro.transform.GetComponent<controlGeneralEnemigo>();
			if (cntGeneral.estado == "pasivo" || cntGeneral.estado == "golpeado")
				cntGeneral.eliminar();
		}
	}
}
