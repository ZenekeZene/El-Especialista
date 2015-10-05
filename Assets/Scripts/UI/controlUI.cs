using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class controlUI : MonoBehaviour {

	private static controlUI _instance;
	public bool esPresentacion;
	
	void Start () {
		Manager manager = GameObject.Find ("Manager").GetComponent<Manager>();
		if (esPresentacion == false){
			NotificationCenter.DefaultCenter().AddObserver(this, "pausar");
			NotificationCenter.DefaultCenter().AddObserver(this, "despausar");
			transform.FindChild("btnPausa").GetComponent<Button>().onClick.AddListener(() => {
				manager.controlarPausa();
			});
			// Menu Pausa:
			Transform canvasPausa = GameObject.Find("pausa").transform.FindChild("CamaraPausa").FindChild ("CanvasPausa");
			canvasPausa.FindChild("btnContinuar").GetComponent<Button>().onClick.AddListener(() => {
				manager.controlarPausa();
			});
			canvasPausa.FindChild("btnReiniciar").GetComponent<Button>().onClick.AddListener(() => {
				manager.reiniciarDesdePrincipio();
			});
			canvasPausa.FindChild("btnSalir").GetComponent<Button>().onClick.AddListener(() => {
				manager.salirAeleccion();
			});
		}
		// Menu Exito:
		Transform canvasExito = GameObject.Find("exito").transform.FindChild("CamaraExito").FindChild ("CanvasExito");
		canvasExito.FindChild("btnAtras").GetComponent<Button>().onClick.AddListener(() => {
			manager.salirAeleccion();
		});
		canvasExito.FindChild("btnRepetir").GetComponent<Button>().onClick.AddListener(() => {
			manager.reiniciarDesdePrincipio();
		});
		
		// Menu Fracaso:
		Transform canvasFracaso = GameObject.Find("fracaso").transform.FindChild("CamaraFracaso").FindChild ("CanvasFracaso");
		canvasFracaso.FindChild("btnAtras").GetComponent<Button>().onClick.AddListener(() => {
			manager.salirAeleccion();
		});
		canvasFracaso.FindChild("btnRepetir").GetComponent<Button>().onClick.AddListener(() => {
			manager.reiniciarDesdePrincipio();
		});
	}
	
	void Update () {
	
	}
	
	private void pausar(Notification notification){
		//panelPausa.GetComponent<Animator>().SetBool("pausado", true);
		//animation["panelPausa"].wrapMode = WrapMode.Once;
		//panelPausa.GetComponent<Animation>().Play("panelPausa");
	}
	
	private void despausar(Notification notification){
		//panelPausa.GetComponent<Animator>().SetBool("pausado", false);
	}
}
