using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class desactivadorFondos : MonoBehaviour {

	public List<GameObject> parallaxAdesactivar;
	private controlParallax cntParallax;
	private bool activo = false;
	
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.CompareTag("detector")){
			if (activo == false){
				for(int i = 0; i < parallaxAdesactivar.Count; i++){
					cntParallax = parallaxAdesactivar[i].GetComponent<controlParallax>();
					//cntParallax.porcInvisible = 100;
					cntParallax.desactivarParallax();
				}
				activo = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D collider){
		if (collider.CompareTag("detector")){
			if (activo){
				for(int i = 0; i < parallaxAdesactivar.Count; i++){
					cntParallax = parallaxAdesactivar[i].GetComponent<controlParallax>();
					cntParallax.activarParallax();
					//cntParallax.porcInvisible = cntParallax.porcInvisibleOriginal;
				}
				activo = false;
			}
		}
	}
}
