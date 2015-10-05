using UnityEngine;
using System.Collections;

public class controlZombieSaltarin : MonoBehaviour {

	public float fuerzaSalto;
	private GameObject cerebro;
	public GameObject cerebroPrefab;
	private bool conCerebro = true;
	private controlEnemigoTerrestre cntTerrestre;
	private controlGeneralEnemigo cntGeneral;

	void Awake() {
		cntTerrestre = GetComponent<controlEnemigoTerrestre>();
		cntGeneral = GetComponent<controlGeneralEnemigo>();
	}

	void Start () {
	
	}
	
	void FixedUpdate(){	
		if (cntTerrestre.enSuelo == true){
			rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
		}
		
		if (rigidbody2D.velocity.y > 2000){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 2000);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (cntTerrestre.collider.isTrigger == true){
			GameObject other = collider.collider2D.gameObject;
			if (cntGeneral.estado == "alerta" || cntGeneral.estado == "pasivo"){
				if (other.CompareTag("bala")) {
					Destroy (other);
					//cntGeneral.estado = "golpeado";
					//rigidbody2D.AddForce(new Vector2(2000 * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 2000));
					cntGeneral.dispararEfecto("sangre");
					cntGeneral.dispararEfecto("escoria");
					if (cntGeneral.vida <= 1){
						if (conCerebro == true){
							soltarCerebro();
							conCerebro = false;
						}
					}
					cntGeneral.restarVida(false);
				}
			}
		}
	}

	void soltarCerebro(){
		if (cerebroPrefab != null){
			Vector3 pos = new Vector3(transform.position.x, transform.position.y + 400, transform.position.z);
			cerebro = Instantiate(cerebroPrefab, pos, Quaternion.identity) as GameObject;
			cerebro.rigidbody2D.AddRelativeForce(new Vector2(500 * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 500));
			cerebro.rigidbody2D.AddTorque(10000f);
		}
	}
}
