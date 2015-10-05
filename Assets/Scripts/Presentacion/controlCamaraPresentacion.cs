using UnityEngine;
using System.Collections;

public class controlCamaraPresentacion : MonoBehaviour {

	private bool arrancado = false;
	private GameObject prota;
	private ControlCamara cntCamara;
	private controlGeneral cntGeneral;
	private utils u;
	void Start () {
		u = GetComponent<utils>();
		cntCamara = GetComponent<ControlCamara>();
		cntCamara.modo = "inicioPresentacion";
		prota = GameObject.FindWithTag("prota");
		cntGeneral = prota.GetComponent<controlGeneral>();
		transform.position = new Vector3(prota.transform.position.x, prota.transform.position.y, transform.position.z);
	}
	
	void Update () {
		if (arrancado == false){
			if (utils.estaTocando()){
				arrancado = true;
				realizarZoom();
			}
		}
	}
	
	private void realizarZoom(){
		u.moverApunto (Camera.main.transform, prota.transform.position, 5, new Vector2(cntCamara.separacionRunner.x, 100), "", gameObject);
		u.cambiarZoom(gameObject.camera, 1028, 3, "actualizarTamCamara", "zoomTerminado", gameObject);
	}
	
	private void actualizarTamCamara(float nuevoTam){
		camera.orthographicSize = nuevoTam;
	}
	
	private void zoomTerminado(){
		cntCamara.modo = "seguir";
		cntGeneral.estado = 0;
	}
}
