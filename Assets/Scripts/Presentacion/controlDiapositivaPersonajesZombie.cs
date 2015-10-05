using UnityEngine;
using System.Collections;

public class controlDiapositivaPersonajesZombie : MonoBehaviour {

	private Rigidbody2D cuerpoFisico;
	private controlGeneralEnemigo cntGeneral;
	private controlDiapositivaPersonajes cntDiapositivaPersonajes;
	
	void Start () {
		cuerpoFisico = GetComponent<Rigidbody2D>();
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		cntDiapositivaPersonajes = transform.parent.transform.parent.FindChild("areaActivadora").GetComponent<controlDiapositivaPersonajes>();
	}
	
	void OnMouseUpAsButton(){
		cuerpoFisico.isKinematic = false;
		cntGeneral.estado = "alerta";
	}
	
	void OnDestroy(){
		Debug.Log ("Zombie presentacion destruido");
		cntDiapositivaPersonajes.puedeTocarParaOtroZombie();
	}

}
