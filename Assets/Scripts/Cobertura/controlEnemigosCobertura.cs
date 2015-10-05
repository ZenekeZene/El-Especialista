using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlEnemigosCobertura : MonoBehaviour {

	public bool estaActivo = false;
	
	private List<Transform> enemigos;
	public int enemigosRestantes;
	
	void Awake(){
		enemigos = buscarEnemigos();
	}
	
	void Start () {
		//desactivarEnemigos();
		NotificationCenter.DefaultCenter().AddObserver(this, "restarEnemigo");
		NotificationCenter.DefaultCenter().AddObserver(this, "refrescarBusquedaEnemigos");
	}
	
	void Update () {
	
	}
	
	void refrescarBusquedaEnemigos(){
		if (estaActivo == true)
			enemigos = buscarEnemigos();
	}
	
	List<Transform> buscarEnemigos() {
		List<Transform> enemigos = new List<Transform>();
		int i = 0;
		foreach(Transform t in transform){
			if (t.gameObject.activeInHierarchy)
				enemigos.Add(t);
		}
		enemigosRestantes = enemigos.Count;
		return enemigos;
	}
	
	private void desactivarEnemigos(){
		foreach(Transform enemigo in enemigos)
			enemigo.gameObject.SetActive(false);
	}
	
	public void activarEnemigos() {
		estaActivo = true;
		gameObject.SetActive(true);
		enemigos = buscarEnemigos();
		foreach(Transform enemigo in enemigos){
			enemigo.gameObject.SetActive(true);
		}
		NotificationCenter.DefaultCenter().PostNotification(this, "empezarAlerta");
	}
	
	private void restarEnemigo(Notification notification) {
		if (estaActivo == true){
			if (enemigosRestantes > 1){
				enemigosRestantes -=1;
			} else {
				estaActivo = false;
				gameObject.SetActive(false);
				NotificationCenter.DefaultCenter().PostNotification(this, "acabarShooter");
			}
		}
	}
}
