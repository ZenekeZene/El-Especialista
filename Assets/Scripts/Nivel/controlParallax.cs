using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/*public enum tipoParallax { Secuencial, Aleatorio }
[ContextMenu("Explode now!")]

[System.Serializable]
public class aleatorioParallax{
	public float desfX;
	public float desfY = 1;
}*/

public class controlParallax : MonoBehaviour {

	private List<Transform> elementos;
	private Camera camara;
	public bool conMovimiento = false;
	public bool esImparable = false; // Para capas que no paran incluso en Shooter o Torre (e.j: vehiculos)
	public float velocidad;
	private float velocidadOriginal;
	public Vector2 desfMin, desfMax;
	private float desfEnX, desfEnY;
	private float tamMax;
	public bool activo = false;
	public bool conHijosCompuestos;
	public Sprite[] spritesDisponibles;
	[Range(0.0f, 100.0f)]
	public int porcInvisible;
	[HideInInspector]
	public int porcInvisibleOriginal;
	
	//public tipoParallax tipo;
	//public aleatorioParallax aleatorio;
	
	void Awake () {
		NotificationCenter.DefaultCenter().AddObserver(this, "reposicionarParallax");
		NotificationCenter.DefaultCenter().AddObserver(this, "empezarParallax");
		NotificationCenter.DefaultCenter().AddObserver(this, "acabarParallax");
	}
	
	void Start () {
		porcInvisibleOriginal = porcInvisible;
		velocidadOriginal = velocidad;
		//empezar ();
	}
	
	public void reposicionarParallax(){
		reposicionar();
	}
	
	private void buscarCamara(){
		if (camara == null){
			camara = Camera.main;
			if (camara == null){
				Debug.LogError("[Parallax] No se ha encontrado ninguna camara.");
				return;
			}
		}
	}
	
	private List<Transform> buscarElementos() {
		List<Transform> elementos = new List<Transform>();
		foreach(Transform hijo in transform)
			elementos.Add(hijo);
		return elementos;
	}
	
	public void reposicionar(){
		buscarCamara();
		elementos = buscarElementos();
		//Primer elemento:
		if (conHijosCompuestos == false)
			tamMax = quienSpriteMasGrande();
		else
			tamMax = quienSpriteHijosMasGrande();
		if (conHijosCompuestos == false)
			decidirTextura(elementos[0]);
		//float desfXIni = elementos[0].renderer.bounds.size.x/2;
		float iniX = camara.ViewportToWorldPoint(Vector3.zero).x;
		elementos[0].position = new Vector3(iniX - tamMax, elementos[0].position.y, elementos[0].position.z);
		//Resto de elementos:
		Vector2 elemAnterior = elementos[0].position;
		for(int i = 1; i <= elementos.Count -1; i++){
			Transform elemento = elementos[i];
			if (conHijosCompuestos == false)
				decidirTextura(elemento);
			//float tam = elemento.renderer.bounds.size.x;
			desfEnX = Random.Range(desfMin.x, desfMax.x);
			desfEnY = Random.Range(desfMin.y, desfMax.y);
			elemento.position = new Vector3(elemAnterior.x + tamMax + desfEnX, elemento.position.y + desfEnY, elemento.position.z);
			elemAnterior = elemento.position;
		}
	}
	
	void FixedUpdate(){
		if (activo){
			if (conMovimiento == true)
				aplicarMovimiento();
			aplicarTraslado();
		}
	}
	
	void aplicarMovimiento(){
		if (elementos != null){
			if (elementos.Count > 0){
				foreach(Transform elemento in elementos)
					elemento.position = new Vector3(elemento.position.x - velocidad , elemento.position.y, elemento.position.z);
			}
		}
		
	}
	
	void aplicarTraslado(){
		if (elementos != null){
			if (elementos.Count > 0){
				Vector2 elemMasAlaDerecha = elementos[elementos.Count - 1].position;
				foreach(Transform elemento in elementos){
					
					//float desfX = elemento.renderer.bounds.size.x/2;
					Vector3 pos = camara.WorldToViewportPoint(new Vector3(elemento.position.x + tamMax, elemento.position.y, elemento.position.z));
					if (elemento.position.x > elemMasAlaDerecha.x)
						elemMasAlaDerecha = elemento.position;
					if (pos.x <= 0){
						//float tamMitad = elemento.renderer.bounds.size.x;
						desfEnX = Random.Range(desfMin.x, desfMax.x);
						desfEnY = Random.Range(desfMin.y, desfMax.y);
						elemento.position = new Vector3(elemMasAlaDerecha.x + tamMax + desfEnX, elemento.position.y + desfEnY, elemento.position.z);
						if (conHijosCompuestos == false)
							decidirTextura(elemento);
					}
				}
			}
		}
	}
	
	void decidirTextura(Transform elemento){		
		SpriteRenderer sprRenderer = elemento.GetComponent<SpriteRenderer>();
		int index = Random.Range(0, spritesDisponibles.Length);
		sprRenderer.sprite = spritesDisponibles[index];
		int esInvisible = Random.Range(0, 100);
		sprRenderer.enabled = esInvisible>=porcInvisible;
	}
	
	float quienSpriteMasGrande(){
		float anchuraMax = elementos[0].renderer.bounds.size.x;
		foreach(Transform elemento in elementos){
			float anchura = elemento.renderer.bounds.size.x;
			if (anchura > anchuraMax)
				anchuraMax = anchura;
		}
		return anchuraMax;
	}
	
	float quienSpriteMasGrande(Transform padre){
		float anchuraMax = 0;
		foreach(Transform hijo in padre){
			float anchura = hijo.renderer.bounds.size.x;
			if (anchura > anchuraMax)
				anchuraMax = anchura;
		}
		return anchuraMax;
	}
	
	float quienSpriteHijosMasGrande(){
		float anchuraMax = 0;
		foreach(Transform hijo in elementos){
			float anchuraMaxDeHijo = quienSpriteMasGrande(hijo);
			if (anchuraMaxDeHijo > anchuraMax)
				anchuraMax = anchuraMaxDeHijo;
		}
		return anchuraMax;
	}
	
	public void empezarParallax(Notification notification){
		activo = true;
		if (conMovimiento)
			velocidad = velocidadOriginal;
	}
	
	public void acabarParallax(Notification notification){
		if (esImparable == false)
			activo = false;
		else
			velocidad = velocidad * (-1);
	}
	
	public void desactivarParallax(){
		porcInvisible = 100;
		velocidad = Mathf.Abs(velocidad * 2) ;
		foreach(Transform elemento in elementos){
			elemento.GetComponent<SpriteRenderer>().enabled = false;
			//if (estaFueraDeCamara(elemento))
				//elemento.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	
	private bool estaFueraDeCamara(Transform elemento){
		float desfX = elemento.renderer.bounds.size.x/2;
		Vector3 pos = camara.WorldToViewportPoint(new Vector3(elemento.position.x + desfX, elemento.position.y, elemento.position.z));
		return (pos.x > 1.4 || pos.x <= 0); // ¿Por que 1.2?
	}
	
	public void activarParallax(){
		porcInvisible = porcInvisibleOriginal;
		velocidad = velocidadOriginal;
		foreach(Transform elemento in elementos){
			if (estaFueraDeCamara(elemento)){
				elemento.GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}
}


