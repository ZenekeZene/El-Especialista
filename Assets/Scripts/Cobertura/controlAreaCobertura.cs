using UnityEngine;
using System.Collections;

public class controlAreaCobertura : MonoBehaviour {

	private bool estaActivo = false;
	private controlGeneral gen;
	private GameObject controlEnemigos;
	
	void Awake(){
		controlEnemigos = transform.parent.Find("_enemigos").gameObject;
		controlEnemigos.SetActive(false);
	}
	
	void Start () {
		
	}
	
	void Update(){
	
	}
	
	void OnTriggerEnter2D(Collider2D other){		
		if (other.gameObject.CompareTag("prota")){
			if (estaActivo == false){
				controlEnemigos.GetComponent<controlEnemigosCobertura>().activarEnemigos();
				NotificationCenter.DefaultCenter().PostNotification(this, "empezarShooter");
			}
		}
	}

}
