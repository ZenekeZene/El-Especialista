using UnityEngine;
using System.Collections;

public class controlZombieGordo : MonoBehaviour {
	
	public int numHamburguesasMin, numHamburguesasMax;
	private int numHamburguesas;
	public GameObject hamburguesaPrefab;
	private bool conHamburguesas = false;
	
	private controlGeneralEnemigo cntGeneral;
	private Transform coberturaPadre;
	private Animator animator;
	
	void Awake () {
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		coberturaPadre = transform.parent;
		animator = GetComponent<Animator>();
	}
	
	void Start(){
		numHamburguesas = Random.Range (numHamburguesasMin, numHamburguesasMax);
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (cntGeneral.estado == "alerta" || cntGeneral.estado == "pasivo" || cntGeneral.estado == "explotando"){
			if (other.CompareTag("bala")) {
				Destroy (other);
				//cntGeneral.estado = "golpeado";
				//rigidbody2D.AddForce(new Vector2(cntGeneral.vel * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 0));
				cntGeneral.dispararEfecto("sangre");
				cntGeneral.dispararEfecto("escoria");
				//Efecto agrandarse la barriga [pendiente]
				if (cntGeneral.vida <= 1){
					if (conHamburguesas == false){
						conHamburguesas = true;
						animator.SetBool("explotar", true);
						cntGeneral.estado = "explotando";
					}
				}
				cntGeneral.restarVida(true);
			}
		}
	}
	
	void explosionTerminada(){
		soltarHamburguesas();
		cntGeneral.muerto ();
	}
	
	void soltarHamburguesas(){
		float desfase = -100f;
		for(int i = 0; i < numHamburguesas; i++){
			GameObject hamburguesa = Instantiate(hamburguesaPrefab, transform.position, Quaternion.identity) as GameObject;
			hamburguesa.transform.parent = coberturaPadre;
			hamburguesa.transform.position = new Vector3(hamburguesa.transform.position.x + desfase, hamburguesa.transform.position.y, hamburguesa.transform.position.z);
			hamburguesa.GetComponent<controlGeneralEnemigo>().estado = "alerta";
			desfase = desfase + (i * 100);
		}
		NotificationCenter.DefaultCenter().PostNotification(this, "refrescarBusquedaEnemigos");
	}
}
