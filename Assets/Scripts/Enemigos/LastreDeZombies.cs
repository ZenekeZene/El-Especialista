using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LastreDeZombies : MonoBehaviour {

	[System.NonSerialized]
	public Transform objetivo;

	public float vida;
	public float delay;
	public float intervalo;
	public float rotacion;
	
	public bool conContenedor;
	private List<Transform> hijos;

	void Awake(){
		objetivo = GameObject.FindWithTag("prota").transform;
		if (conContenedor == true)
			hijos = buscarHijos();
	}

	void Start () {
		int direction = aqueLadoDeMi(objetivo);
		rotacion = rotacion * direction;
		transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
		Invoke("empezarFade", delay);
	}
	
	void FixedUpdate () {
		rigidbody2D.AddTorque(rotacion);
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (other.CompareTag("bala")) {
			Destroy (other);
			rigidbody2D.AddForce(new Vector2(5000 * aqueLadoDeMi(objetivo), 10000));
				//cntGeneral.dispararEfectoEscoriaBala(); // Estaria bien este efecto en los lastres, clase Utils??
		}
	}
	
	void empezarFade(){
		if (conContenedor == true)
			StartCoroutine("FadeAHijos");
		else
			StartCoroutine("Fade");
	}
	
	IEnumerator FadeAHijos() {
		foreach(Transform hijo in hijos){
			for (float f = 1f; f > 0; f -= intervalo) {
				Color c = hijo.gameObject.renderer.material.color;
				c.a = f;
				hijo.gameObject.renderer.material.color = c;
				yield return new WaitForSeconds(intervalo);
			}
			destruir();
		}
	}
	
	IEnumerator Fade() {
		for (float f = 1f; f > 0; f -= intervalo) {
			Color c = renderer.material.color;
			c.a = f;
			renderer.material.color = c;
			yield return new WaitForSeconds(intervalo);
		}
		destruir();
	}
	
	void destruir(){
		Destroy(gameObject);
	}
	
	//Refactoring???
	public int aqueLadoDeMi(Transform target){
		return ((transform.position.x > target.position.x)? 1 : -1);
	}
	
	private List<Transform> buscarHijos(){
		List<Transform> hijos = new List<Transform>();
		int i = 0;
		foreach(Transform t in transform){
			if (t.gameObject.activeInHierarchy)
				hijos.Add(t);
		}
		return hijos;
	}
	
}
