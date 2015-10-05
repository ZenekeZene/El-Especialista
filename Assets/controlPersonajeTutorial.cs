using UnityEngine;
using System.Collections;

public class controlPersonajeTutorial : MonoBehaviour {

	private controlGeneral gen;
	private controlRunner run; 

	void Start () {
		gen = GetComponent<controlGeneral>();
		run = GetComponent<controlRunner>();
	}
	
	void Update () {
		if (gen.estado == 1){
			if (utils.estaTocando ()){
				if (utils.mitadPantalla()){
					if (run.estaEnSuelo == true)
						NotificationCenter.DefaultCenter().PostNotification(this, "registraSalto");
				}
			}
		}
	}
}
