using UnityEngine;
using System.Collections;

public class destructorProta : MonoBehaviour {

	private bool soloUnaVez = true;
	
	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("prota")){
			if (soloUnaVez){
				soloUnaVez = false;
				controlGeneral cntGeneral = other.gameObject.GetComponent<controlGeneral>();
				if (cntGeneral.estado == 1){
					cntGeneral.quitarVida(false);
					NotificationCenter.DefaultCenter().PostNotification(this, "quitarVidaProta");
					NotificationCenter.DefaultCenter().PostNotification(this, "acabarParallax");
				}
			}
			
		}
	}
}
