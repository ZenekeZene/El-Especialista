using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlCieloGeneral : MonoBehaviour {
	
	private List<Transform> cielosHijos = new List<Transform>();
	
	void Start () {
		cielosHijos = buscarCielosHijos();
		NotificationCenter.DefaultCenter().AddObserver(this, "ajustarColorCielos");
	}
	
	private List<Transform> buscarCielosHijos(){
		List<Transform> cielosHijos = new List<Transform>();
		foreach(Transform cielo in transform)
			cielosHijos.Add(cielo);
		return cielosHijos;
	}
	
	private void ajustarColorCielos(Notification notification){
		coloresYvelocidadCambioColor diccioColor = (coloresYvelocidadCambioColor) notification.data;
		Color[] colores = (Color[]) diccioColor.colores.Clone();
		float vel = (float) diccioColor.velocidadAcambiar;
		int i = 0;
		foreach(Transform cielo in transform){
			cielo.GetComponent<controlCielo>().ajustarColor(colores[i], vel);
			i++;
		}
	}
}
