using UnityEngine;
using System.Collections;

public class controlEnemigoDisparador : MonoBehaviour {

	public GameObject prefabBala;
	public float delayInicial, delayDisparoMin, delayDisparoMax;
	private controlGeneralEnemigo cntGeneral;
	private controlEnemigoTerrestre cntTerrestre;
	private Transform pistola;

	void Awake () {
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		cntTerrestre = GetComponent<controlEnemigoTerrestre>();
		pistola = transform.FindChild("Tronco").transform.FindChild("Antebrazo1").transform.FindChild("Brazo1").transform.FindChild("Pistola");
	}
	
	void Start() {
		Invoke("realizarDisparo", delayInicial);
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (cntGeneral.estado == "alerta" || cntGeneral.estado == "pasivo" || cntGeneral.estado == "disparando"){
			if (other.CompareTag("bala")) {
				Destroy (other);
				cntGeneral.estado = "golpeado";
				rigidbody2D.AddForce(new Vector2(30000 * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 70000));
				cntGeneral.restarVida(false);
				cntGeneral.dispararEfecto("sangre");
				cntGeneral.dispararEfecto("escoria");
			}
		}
	}
	
	void realizarDisparo(){
		cntGeneral.estado = "disparando";
		cntTerrestre.pararse();
		//disparar ();
		cntGeneral.animator.SetBool("disparo", true);
	}
	
	void disparar(){
		GameObject bala = Instantiate(prefabBala, pistola.transform.position, Quaternion.identity) as GameObject;
		//Vector3 initialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		/*double angle2 = Mathf.Atan2(initialPosition.y - bala.transform.position.y, initialPosition.x - bala.transform.position.x);
		float angleInDegrees = (float)(angle2);
		angleInDegrees = Mathf.Rad2Deg * angleInDegrees + Random.Range (-3, 3);
		bala.transform.Rotate (new Vector3 (0, 0, angleInDegrees));*/
		bala.transform.localScale = new Vector3(8, 3.5f, 1);
	}
	
	void disparoTerminado(){
		cntGeneral.estado = "alerta";
		cntTerrestre.seguir();
		cntGeneral.animator.SetBool("disparo", false);
		Invoke("realizarDisparo", Random.Range(delayDisparoMin, delayDisparoMax));
	}
}
