using UnityEngine;
using System.Collections;

public class controlDiapositivaTecnologia : MonoBehaviour {

	public float velSoltarCajas, velJugarConCamara, velZoomCamara;
	private utils u;
	private bool puedeTocarParaCamara = false;
	private ControlCamara cntCamara;
	private Transform atlas;
	private int contAtlas = 0;
	private bool puedeTocarParaAtlas = false;
	
	void Start () {
		cntCamara = Camera.main.GetComponent<ControlCamara>();
		u = GetComponent<utils>();
		atlas = transform.parent.FindChild("atlas");
	}
	
	void OnEnable(){
		Invoke ("soltarCajas", velSoltarCajas);
		Invoke ("permitirJugarConCamara", velJugarConCamara);
	}
	
	private void soltarCajas(){
		transform.parent.FindChild("_cajas").gameObject.SetActive(true);
	}
	
	private void permitirJugarConCamara(){
		puedeTocarParaCamara = true;
		Debug.Log ("Ahora si toco, la camara se mueve");
	}
	
	public void Update(){
		if (puedeTocarParaCamara){
			if (utils.estaTocando()){
				puedeTocarParaCamara = false;
				realizarZoom();
			}
		}
		
		if (puedeTocarParaAtlas){
			if (utils.estaTocando()){
				cambiarAtlas();
			}
		}
	}
	
	private void realizarZoom(){
		Debug.Log (utils.estaTocando());
		if (utils.estaTocando ()){
			Debug.Log("Estoy tocando");
			cntCamara.modo = "zoom";
			u.moverApunto(Camera.main.transform, "prota", velZoomCamara, new Vector2(0, 0), "");
			u.cambiarZoom(Camera.main, 512, velZoomCamara, "actualizarTamCamara", "zoomTerminado", gameObject);
		}
	}
	
	private void actualizarTamCamara (float size) {
		Camera.main.orthographicSize = size;
	}
	
	private void zoomTerminado(){
		Debug.Log ("Zoom terminado");
		
		u.moverApunto(Camera.main.transform, Camera.main.transform.position, 1, new Vector2(500, 0), "posicionEnAtlasTerminado", gameObject);
	}
	
	private void posicionEnAtlasTerminado(){
		Debug.Log ("atlas terminado");
		puedeTocarParaAtlas = true;
	}
	
	//private void aparecerAtlas(){
		//atlas.gameObject.SetActive(true);
	//}
	
	private void cambiarAtlas(){
		if (atlas.gameObject.activeSelf == false){
			atlas.gameObject.SetActive(true);
			atlas.GetChild(contAtlas).gameObject.SetActive(true);
			contAtlas++;
		} else {
			foreach(Transform at in atlas){
				at.gameObject.SetActive(false);
			}
			if (atlas.childCount != contAtlas){
				atlas.GetChild(contAtlas).gameObject.SetActive(true);
				contAtlas++;
			} else {
				puedeTocarParaAtlas = false;
				quitarZoom();
			}
		}
	}
	
	private void quitarZoom(){
		
		u.cambiarZoom(Camera.main, 1028, 1, "actualizarTamCamara", "zoomQuitado", gameObject);
	}
	
	private void zoomQuitado(){
		cntCamara.modo = "seguir"; //new Vector2(1300, -80)
		u.moverApunto (Camera.main.transform, GameObject.FindWithTag("prota").transform.position, 1, new Vector2(cntCamara.separacionRunner.x, 100), "terminado", gameObject);
		//u.moverApunto(Camera.main.transform, "prota", 1, new Vector2(cntCamara.separacionRunner.x, 100), "terminado");
	}
	
	private void terminado(){
		Destroy (this);
	}
}
