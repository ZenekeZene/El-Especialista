using UnityEngine;
using System.Collections;

public class controlSeguirPresentacion : MonoBehaviour {

	private GameObject diapositivaActual = null;
	
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "registraDiapositivaActiva");
	}
	
	void OnMouseUpAsButton(){
		if (diapositivaActual != null){
			diapositivaActual.GetComponent<areaDiapositiva>().desactivarDiapositiva();
			diapositivaActual = null;
		}
	}
	
	private void registraDiapositivaActiva(Notification notification){
		diapositivaActual = (GameObject) notification.data;
		
	}
}
