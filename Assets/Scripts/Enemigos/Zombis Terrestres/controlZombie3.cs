using UnityEngine;
using System.Collections;

public class controlZombie3 : MonoBehaviour {

	private controlGeneralEnemigo cntGeneral;
	private GameObject casco, cascoSuelto;
	public GameObject cascoSueltoPrefab;
	private int vidaInicial;
	private bool conCasco = true;
	public Color colorCascoGolpeado, colorCascoNormal;

	void Awake () {
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		casco = transform.FindChild("CascoPuesto").gameObject;
		vidaInicial = cntGeneral.vida;
	}
	
	void Start(){
	
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
				efectoCascoGolpeada ();
				if (cntGeneral.vida < (vidaInicial/3)){
					if (conCasco == true){
						soltarCasco();
						conCasco = false;
					}
				}
			}
		}
	}
	
	void soltarCasco(){
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + 400, transform.position.z);
		cascoSuelto = Instantiate(cascoSueltoPrefab, pos, Quaternion.identity) as GameObject;
		casco.SetActive(false);
		cascoSuelto.rigidbody2D.AddRelativeForce(new Vector2(500 * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 500), ForceMode2D.Force);
		cascoSuelto.rigidbody2D.AddTorque(10000f);
	}
	
	void efectoCascoGolpeada() {
		iTween.ColorTo(casco, iTween.Hash("color", colorCascoGolpeado,"time", 0.4,"loopType", iTween.LoopType.none, "oncompletetarget", gameObject, "onComplete", "acabarEfectoCascoGolpeada"));
	}
	
	void acabarEfectoCascoGolpeada() {
		iTween.ColorTo(casco, iTween.Hash("color", colorCascoNormal,"time", 0.4,"loopType", iTween.LoopType.none));
	}
}
