using UnityEngine;
using System.Collections;

public class controlZombie1 : MonoBehaviour {

	private controlGeneralEnemigo cntGeneral;

	void Awake () {
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		
		
	}
	
	void Start(){
		cambiarColor ();
		if (audio != null){
			if (GameObject.Find("libreriaSonidos")!= null)
				audio.clip = GameObject.Find("libreriaSonidos").GetComponent<libreriaSonidos>().dameSonidoMorirAleatorio();
		}
	}
	
	private void cambiarColor(){
		GameObject tronco = transform.FindChild("Tronco").gameObject;
		Vector4 color = new Vector4((float)Random.Range(0, 100)/100, (float)Random.Range(0, 100)/100, (float)Random.Range(0, 100)/100, 1f);
		SpriteRenderer renderer = tronco.GetComponent<SpriteRenderer>();
		renderer.color = color;
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (other.CompareTag("bala")) {
			Destroy (other);
			if (cntGeneral.estado == "alerta" || cntGeneral.estado == "pasivo"){
				cntGeneral.estado = "golpeado";
				rigidbody2D.AddForce(new Vector2(50000 * cntGeneral.aqueLadoDeMi(cntGeneral.objetivo), 100000));
				cntGeneral.restarVida(false);
				cntGeneral.dispararEfecto("sangre");
				cntGeneral.dispararEfecto("escoria");
			}
		}
	}
}
