using UnityEngine;
using System.Collections;

public class controlZombie2 : MonoBehaviour {
	
	private controlGeneralEnemigo cntGeneral;
	private GameObject cazo, cazoSuelto;
	public GameObject cazoSueltoPrefab;
	private int vidaInicial;
	private bool conCazo = true;
	public Color colorCazoGolpeado, colorCazoNormal;
	
	void Awake () {
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		cazo = transform.FindChild("CazoPuesto").gameObject;
		vidaInicial = cntGeneral.vida;
	}
	
	void Start() {
	
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (other.CompareTag("bala")) {
			Destroy (other);
			if (cntGeneral.estado == "alerta" || cntGeneral.estado == "pasivo"){
				cntGeneral.estado = "golpeado";
				rigidbody2D.AddForce(new Vector2(5000 * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 10000));
				cntGeneral.restarVida(false);
				cntGeneral.dispararEfecto("sangre");
				cntGeneral.dispararEfecto("escoria");
				efectoCazoGolpeada ();
				if (cntGeneral.vida < (vidaInicial/3)){
					if (conCazo == true){
						soltarCazo();
						conCazo = false;
					}
				}
			}
		}
	}
	
	void soltarCazo(){
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + 400, transform.position.z);
		cazoSuelto = Instantiate(cazoSueltoPrefab, pos, Quaternion.identity) as GameObject;
		cazo.SetActive(false);
		cazoSuelto.rigidbody2D.AddRelativeForce(new Vector2(500 * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 500), ForceMode2D.Force);
		cazoSuelto.rigidbody2D.AddTorque(10000f);
	}
		
	void efectoCazoGolpeada() {
		iTween.ColorTo(cazo, iTween.Hash("color", colorCazoGolpeado,"time", 0.4,"loopType", iTween.LoopType.none, "oncompletetarget", gameObject, "onComplete", "acabarEfectoCazoGolpeada"));
	}
	
	void acabarEfectoCazoGolpeada() {
		iTween.ColorTo(cazo, iTween.Hash("color", colorCazoNormal,"time", 0.4,"loopType", iTween.LoopType.none));
	}

}