using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlEnemigosTorre : MonoBehaviour {

	public bool estaActivo = false;
	
	private List<Transform> hordas;
	private List<Transform>[] enemigos;
	private int hordasRestantes;
	
	void Awake(){
		hordas = buscarHordas();
		hordasRestantes = hordas.Count;
		enemigos = buscarEnemigosDeHordas();
	}
	
	void Start () {
		desactivarHordas();

		NotificationCenter.DefaultCenter().AddObserver(this, "restarEnemigo");
	}
	
	List<Transform> buscarHordas() {
		List<Transform> hordasActivas = new List<Transform>();
		int i = 0;
		foreach(Transform t in transform){
			if (t.gameObject.activeInHierarchy)
				hordasActivas.Insert(i, t);
			i++;
		}
		return hordasActivas;
	}

	public void desactivarHordas(){
		foreach(Transform horda in hordas)
			horda.gameObject.SetActive(false);
	}
	
	List<Transform>[] buscarEnemigosDeHordas(){
		List<Transform>[] enemigosActivos = new List<Transform>[hordas.Count];
		int i = 0;
		foreach(Transform horda in hordas){
			enemigosActivos[i] = buscarEnemigosDeHorda (i);
			i++;
			
		}
		return enemigosActivos;
	}
	
	List<Transform> buscarEnemigosDeHorda(int indexHorda){
		Transform horda = hordas[indexHorda];
		List<Transform> enemigos = new List<Transform>();
		foreach(Transform enemigo in horda.transform){
			if (enemigo.gameObject.activeInHierarchy)
				enemigos.Add (enemigo);
		}
		return enemigos;
	}
	
	public void activarHordas() {
		estaActivo = true;
		int i = 0;
		foreach(Transform horda in hordas){
			activarHorda(i);
			i++;
		}
	}
	
	public void activarHorda(int indexHorda) {
		hordas[indexHorda].gameObject.SetActive(true);
		NotificationCenter.DefaultCenter().PostNotification(this, "empezarAlerta");
	}
	
	private void restarEnemigo(Notification notification) {
		Transform enemigo = notification.sender.transform;
		if (estaActivo == true){
			int indexHorda = aQueHordaPertenece(enemigo);
			if (enemigos[indexHorda].Count > 1)
				if (enemigo.parent.tag == "enemigoVolador") // Deuda tecnica con voladores
					enemigos[indexHorda].Remove(enemigo.parent);
				else
					enemigos[indexHorda].Remove(enemigo);
			else
				restarHorda (indexHorda);
		}
	}
	
	private int aQueHordaPertenece(Transform enemigo){
		int index = -1;
		for(int i = 0; i < enemigos.Length; i++){
			if (enemigos[i].Contains(enemigo) || enemigos[i].Contains(enemigo.parent))
				index = i;
		}
		return index;
	}

	private void restarHorda(int indexHorda) {
		if (estaActivo == true){
			if (hordasRestantes > 1){
				hordasRestantes -=1;
			} else {
				estaActivo = false;
				NotificationCenter.DefaultCenter().PostNotification(this, "acabarTorre");
			}
		}
	}
}
