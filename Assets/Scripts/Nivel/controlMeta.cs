using UnityEngine;
using System.Collections;

public class controlMeta : MonoBehaviour {

	void Start () {
	
	}
	
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("prota")){
			GameObject manager = GameObject.Find ("Manager");
			if (manager.GetComponent<Manager>().vidaActual > 0){
				NotificationCenter.DefaultCenter().PostNotification(this, "paraAlpersonaje");
				NotificationCenter.DefaultCenter().PostNotification(this, "nivelFinalizadoConExito");
			}
		}
	}
}
