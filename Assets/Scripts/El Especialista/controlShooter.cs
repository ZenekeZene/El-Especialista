using UnityEngine;
using System.Collections;

public class controlShooter : MonoBehaviour {

	private controlGeneral gen;
	private controlRunner run;
	private utils u;

	public bool estaCubriendose = false;

	private GameObject arma, brazo2Armado;
	private Transform puntoDisparo, efectoDisparo;
    private GameObject prefabBala;
    private int numBalas;
    private float delayDisparo;
    private float timerDisparo;
    public float velShooter;
    private AudioClip sonido;
    
    private bool aqueLadoMira = true; // true derecha, false izquierda

    void Awake() {
		gen = GetComponent<controlGeneral>();
		run = GetComponent<controlRunner>();
		u = GetComponent<utils>();
    }

	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "empezarShooter");
		NotificationCenter.DefaultCenter().AddObserver(this, "acabarShooter");
		buscarArma();
	}
	
	void Update () {
		
        if (utils.estaTocando()){
			if (gen.estado == 1) {
	            if (utils.mitadPantalla() == false)
	                disparar();
	        } else if (gen.estado == 2){
				if (utils.mitadPantalla () == false && estaCubriendose == false)
					disparar ();
				else
					cubrirse();
	        } else if (gen.estado == 3)
				disparar();
			else if (gen.estado == 6){ // presentacion
				if (utils.mitadPantalla () == false)
					disparar ();
			}	
		}
		
		if (Input.GetMouseButtonUp(0)){
			if (gen.estado == 2){
				descubrirse();
			}
		}
		
		// Delay disparo:
		if (timerDisparo > 0)
			timerDisparo -= Time.deltaTime;
		
		if (arma != null && brazo2Armado != null){
			if (gen.estado == 1 || gen.estado == 2 || gen.estado == 6){
				if (utils.mitadPantalla() == false)
					rotarArma("normal");
			} else if (gen.estado == 3){
				if (utils.mitadPantalla()){
					if (aqueLadoMira == true){
						aqueLadoMira = false;
						gen.girarseIzquierda();
					}
				} else {
					if (aqueLadoMira == false){
						aqueLadoMira = true;
						gen.girarseDerecha();
					}
				}
				rotarArma("torre");
			}
		}
	}

    void disparar() {
		if (timerDisparo <= 0) {
			for (int i = 0; i < numBalas; i++) {
				GameObject bala = Instantiate (prefabBala, new Vector3 (puntoDisparo.position.x, puntoDisparo.position.y, puntoDisparo.position.z), Quaternion.identity) as GameObject;
				
				Vector3 initialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				double angle2 = Mathf.Atan2(initialPosition.y - bala.transform.position.y, initialPosition.x - bala.transform.position.x);
				float angleInDegrees = (float)(angle2);
				angleInDegrees = Mathf.Rad2Deg * angleInDegrees + (i * 0.5f) + Random.Range (-3, 3);
				bala.transform.Rotate (new Vector3 (0, 0, angleInDegrees));
				//bala.transform.localScale = new Vector3(8, 3.5f, 1);
			}
			arma.GetComponent<Animator>().Play ("efectoRetroceso");
			efectoDisparo.GetComponent<Animator>().Play("efectoDisparar");
			if (audio != null)
				audio.Play();
			timerDisparo = delayDisparo;
		}
	}

	public void empezarShooter(Notification notification){
		rigidbody2D.isKinematic = true;
		gen.estado = 2;
		u.moverApunto(transform, "nodoCobertura", velShooter, new Vector2(0, 0), "transicionAshooterTerminada");
	}

	public void transicionAshooterTerminada(){
		gen.animator.SetInteger("Estado", gen.estado);
	}
	
	public void acabarShooter(Notification notification){
		rigidbody2D.isKinematic = false;
		gen.estado = 1;
		gen.animator.SetInteger("Estado", gen.estado);
		run.saltar(1500, run.fuerzaSalto * 7);
	}
	
	private void buscarArma(){
		arma = GameObject.FindGameObjectWithTag("arma");
		if (arma != null){
			buscarHijosDeArma();
			paramsArma armaParams = arma.GetComponent<paramsArma>();
			if (armaParams != null){
				delayDisparo = armaParams.delayDisparo;
				numBalas = armaParams.numBalas;
				prefabBala = armaParams.prefabBala;
				timerDisparo = delayDisparo;
				if (audio != null)
					audio.clip = armaParams.sonido;
			}
		}
	}
	
	private void buscarHijosDeArma(){
		brazo2Armado = GameObject.FindGameObjectWithTag("brazo2Prota");
		puntoDisparo = arma.transform.FindChild("puntoDisparo");
		efectoDisparo = arma.transform.FindChild("efectoDisparo");
	}
	
	private void rotarArma(string tipo){
		// Arma:
		Vector3 pos = Camera.main.WorldToScreenPoint(arma.transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		
		if (angle > 90 || angle < -90)
			angle = 180 - angle;
		arma.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		// Brazo 2:
		Vector3 posBrazo2 = Camera.main.WorldToScreenPoint(brazo2Armado.transform.position);
		Vector3 dirBrazo2 = Input.mousePosition - posBrazo2;
		float angleBrazo2 = Mathf.Atan2(dirBrazo2.y - 50, dirBrazo2.x) * Mathf.Rad2Deg;
		if (tipo == "normal"){
			if (angleBrazo2 < 50 && angleBrazo2 >-30){
				brazo2Armado.SetActive(true);
				brazo2Armado.transform.rotation = Quaternion.AngleAxis(angleBrazo2 + 10, Vector3.forward);
			} else
				brazo2Armado.SetActive(false);
		} else if (tipo == "torre")
			brazo2Armado.SetActive(false);
	}
	
	public void cambiarArma(GameObject armaNueva){
		paramsArma armaParams = armaNueva.GetComponent<paramsArma>();
		
		arma.SetActive(false);
		arma = transform.FindChild(armaParams.name).gameObject;
		
		arma.SetActive(true);
		NotificationCenter.DefaultCenter().PostNotification(new Notification(this, "registrarArma", armaParams.name));
		buscarHijosDeArma();
		delayDisparo = armaParams.delayDisparo;
		numBalas = armaParams.numBalas;
		prefabBala = armaParams.prefabBala;
		if (audio != null)
			audio.clip = armaParams.sonido;
	}
	
	private void cubrirse(){
		if (estaCubriendose == false){
			estaCubriendose = true;
			gen.animator.SetBool("EstaCubriendose", estaCubriendose);
		}
	}
	
	private void descubrirse(){
		estaCubriendose = false;
		gen.animator.SetBool("EstaCubriendose", false);
	}
}
