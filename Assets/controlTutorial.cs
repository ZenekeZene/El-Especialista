using UnityEngine;
using System.Collections;

public class controlTutorial : MonoBehaviour {

	private bool estaActivo = false;
	private GameObject area;
	private GameObject panel;
	private SpriteRenderer sprRendererPanel;
	public float vel;
	private string modo = "estatico";
	private Vector3 posCamara;
	private float tamPanel;
	private GameObject tocaParaContinuar;
	private bool puedeTocarContinuar = false;
	public bool conTiempo;
	public bool esDependiente;
	public bool conTocarParaContinuar;
	public bool conMovimiento;
	public bool conParpadeo;
	public bool conItem;
	private GameObject item;
	
	void Start () {
		area = transform.FindChild("area").gameObject;
		panel = transform.FindChild("panel").gameObject;
		sprRendererPanel = panel.GetComponent<SpriteRenderer>();
		tamPanel = sprRendererPanel.bounds.size.x /2;
		panel.SetActive(false);
		posCamara = Camera.main.transform.position;
		tocaParaContinuar = transform.FindChild("TocaParaContinuar").gameObject;
		if (conItem){
			item = transform.FindChild("item").gameObject;
			item.SetActive(false);
		}
	}
	
	void Update () {
		if (conMovimiento){
			if (modo == "seguirCamara"){
				panel.transform.position = new Vector3(Camera.main.transform.position.x - tamPanel, Camera.main.transform.position.y, transform.position.z);
			}
		} else {
			if (conTocarParaContinuar){
				if (puedeTocarContinuar){
					if (utils.estaTocando()){
						puedeTocarContinuar = false;
						quitarTutorial();
					}
				}
			}
		}
	}
	
	public void activar(){
		if (estaActivo == false){
			estaActivo = true;
			area.SetActive(false);
			if (conItem)
				item.SetActive(true);
			if (conMovimiento)
				modo = "seguirCamara";
			else
				NotificationCenter.DefaultCenter().PostNotification(this, "paraAlpersonaje");
			panel.SetActive(true);
			iTween.MoveTo(panel, iTween.Hash("position", new Vector3(panel.transform.position.x, panel.transform.position.y - 500, panel.transform.position.z), "time", vel, "oncomplete", "tutorialMostrado", "oncompletetarget", gameObject));
		}
	}
	
	private void tutorialMostrado(){
		if (conParpadeo)
			Invoke ("efectoPanel", 2f);
		if (conTocarParaContinuar == true)
			Invoke ("sacaContinuar", 2f);
		else if (conTocarParaContinuar == false){
			if (conTiempo)
				Invoke ("quitarTutorial", 10f);
		}
	}
	
	private void efectoPanel(){
		iTween.ColorTo(panel, iTween.Hash ("a", 0, "time", 1, "looptype", iTween.LoopType.pingPong, "includechildren", false));
	}
	
	public void quitarTutorial(){
		Debug.Log ("Quitamos tutorial");
		gameObject.SetActive(false);
		if (esDependiente == false)
			NotificationCenter.DefaultCenter().PostNotification(this, "reanudaAlpersonaje");
	}
	
	private void sacaContinuar(){
		tocaParaContinuar.SetActive(true);
		Invoke ("puedeTocarParaContinuar", 2f);
	}
	
	private void puedeTocarParaContinuar(){
		puedeTocarContinuar = true;
	}
}
