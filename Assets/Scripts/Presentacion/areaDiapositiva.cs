using UnityEngine;
using System.Collections;

public class areaDiapositiva : MonoBehaviour {

	private bool estaActivo = false;
	private GameObject panel;
	
	void Start () {
		panel = transform.parent.FindChild("panel").gameObject;
		panel.SetActive(false);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("prota")){
			if (estaActivo == false){
				estaActivo = true;
				activar ();
				NotificationCenter.DefaultCenter().PostNotification(this, "paraAlpersonaje");
				NotificationCenter.DefaultCenter().PostNotification(new Notification(this, "registraDiapositivaActiva", gameObject));
			}
		}
	}
	
	private void activar(){
		panel.SetActive(true);
		Vector3 pos = new Vector3(panel.transform.position.x, panel.transform.position.y - panel.renderer.bounds.size.y/2 - 100, panel.transform.position.z);
		iTween.MoveTo (panel, iTween.Hash("position", pos, "time", 1f));
		// Diapositiva Tecnologia
		if (gameObject.GetComponent<controlDiapositivaTecnologia>() != null){
			gameObject.GetComponent<controlDiapositivaTecnologia>().enabled = true;
		} else if (gameObject.GetComponent<controlDiapositivaPersonajes>() != null){
			gameObject.GetComponent<controlDiapositivaPersonajes>().enabled = true;
		}
	}
	
	public void desactivarDiapositiva(){
		Vector3 pos = new Vector3(panel.transform.position.x, panel.transform.position.y + panel.renderer.bounds.size.y/2 + 100, panel.transform.position.z);
		iTween.MoveTo (panel, iTween.Hash("position", pos, "time", 1f, "oncomplete", "diapositivaDesactivada", "oncompletetarget", gameObject));
	}
	
	private void diapositivaDesactivada(){
		NotificationCenter.DefaultCenter().PostNotification(this, "reanudaAlpersonaje");
	}
	
}
