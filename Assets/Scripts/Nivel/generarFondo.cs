using UnityEngine;
using System.Collections;

public class generarFondo : MonoBehaviour {
	public GameObject suelo;
	public int tiempo = 0, tiempoMin = 2, tiempoMax = 10;
	// Use this for initialization
	void Start () {
		generar ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void generar(){
		tiempo = Random.Range (tiempoMin, tiempoMax);
		Instantiate (suelo, transform.position, Quaternion.identity);
		Invoke("generar", tiempo);
	}
}
