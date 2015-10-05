using UnityEngine;
using System.Collections;

public class areaActivadoraTutorial : MonoBehaviour {

	private bool estaActivo = false;
	private controlTutorial cntTutorial;

	void Start () {
		cntTutorial = transform.parent.GetComponent<controlTutorial>();
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("prota")){
			if (estaActivo == false){
				estaActivo = true;
				cntTutorial.activar();
			}
		}
	}
}
