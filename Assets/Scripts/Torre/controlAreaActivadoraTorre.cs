using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlAreaActivadoraTorre : MonoBehaviour {

	private bool activa = false;
	
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (activa == false){
			if (other.tag == "prota"){
				NotificationCenter.DefaultCenter().PostNotification(this, "empezarTorre");
				activa = true;
			}
		}
	}
}
