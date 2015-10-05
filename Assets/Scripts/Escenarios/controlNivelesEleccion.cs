using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlNivelesEleccion : MonoBehaviour {
	
	private List<Transform> hijos;
	public bool estaCargando = false;
	public int indexSeleccionado = 0;
	
	void Awake () {
		hijos = buscarHijos();
	}
	
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "nivelSeleccionado");
		desactivarTodos ();
		indexSeleccionado = 0;
		activarIcono ();
	}
	
	private List<Transform> buscarHijos () {
		List<Transform> hijos = new List<Transform>();
		foreach(Transform hijo in transform){
			hijos.Add(hijo);
		}
		return hijos;
	}
	
	private void desactivarTodos () {
		for(int i = 0; i < hijos.Count; i++){
			tintar (hijos[i], new Color(1, 1, 1, 0.9f));
			hijos[i].GetComponent<Animator>().SetBool("Seleccionado", false);
		}
		estaCargando = false;
		indexSeleccionado = -1;
	}
	
	private void tintar(Transform target, Color color){
		foreach(Transform pieza in target)
			pieza.renderer.material.color = color;
	}
	
	private void activarIcono(){
		tintar (hijos[indexSeleccionado], Color.white);
		hijos[indexSeleccionado].GetComponent<Animator>().SetBool("Seleccionado", true);
		
	}
	
	private void activarNivel(){
		hijos[indexSeleccionado].GetComponent<controlNivel>().activar();
		estaCargando = true;
	}

	private void nivelSeleccionado( Notification notification){
		desactivarTodos ();
		indexSeleccionado = (int) notification.data;
		activarIcono ();
		Invoke("activarNivel", 1.5f);
		if (indexSeleccionado != 0 && indexSeleccionado != 1 && indexSeleccionado != 8 && indexSeleccionado != 9)
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", hijos[indexSeleccionado].position.x, "axis", "x", "time", 1));
	}
}
