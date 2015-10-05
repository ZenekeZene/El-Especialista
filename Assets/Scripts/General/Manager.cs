using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	private static Manager _instance;
	
	public string estado = "jugando";
	private GameObject protagonista;
	private controlGeneral cntGeneral;
	private ControlCamara cntCamara;
	public int vidaInicio, vidaActual;
	private Transform fondos;
	private GameObject _constantes;
	public Vector3 ultimoCheckPointSuperado;
	private string armaActual, armaAntigua;
	private GameObject bateria;
	
	public GameObject camaraPausa, camaraExito, camaraFracaso;
	
	private static bool yaEstaCargado = false;
	
	public static Manager instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<Manager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}
	
	void Awake(){
		if(_instance == null){
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else{
			if(this != _instance)
				Destroy(this.gameObject);
		}
		if (Application.loadedLevelName == "eleccion"){
			destruirConstantes();
			Destroy (this.gameObject);
		}
			
	}
	
	void OnLevelWasLoaded(int level){
		
		if (Application.loadedLevelName == "eleccion")
			Destroy (this.gameObject);
		else {
			if (yaEstaCargado == false){
				yaEstaCargado = true;
				init ();
			}
		}
	}

	void Start () {
		if (protagonista == null) // quitar en Produccion
			init (); // quitar en Produccion
		
	}
	
	private void init(){
		if (estado == "jugando"){
			if (ultimoCheckPointSuperado.Equals(Vector3.zero))
				ultimoCheckPointSuperado = GameObject.FindWithTag("posicionInicial").transform.position;
			Debug.Log ("INIT");
			recogerReferencias();
			recogerConstantes();
			mostrarVida();
			reposicionarProtagonista();
			Invoke ("reposicionarCamara", 0.1f);
			//reposicionarCamara();
			Invoke ("reposicionarFondos", 0.2f);
			Invoke ("recogerReferencias", 0.3f);
			Invoke("recogerConstantes", 0.4f);
			Invoke("mostrarVida", 0.5f);
			NotificationCenter.DefaultCenter().AddObserver(this, "registrarCheckPoint");
			NotificationCenter.DefaultCenter().AddObserver(this, "quitaleVidaProtaManager");
			NotificationCenter.DefaultCenter().AddObserver(this, "aumentarVidaProta");
			NotificationCenter.DefaultCenter().AddObserver(this, "registrarArma");
			NotificationCenter.DefaultCenter().AddObserver(this, "sacarMenuExito");
			NotificationCenter.DefaultCenter().AddObserver(this, "paraAlpersonaje");
			NotificationCenter.DefaultCenter().AddObserver(this, "reanudaAlpersonaje");
		}
	}
	
	private void recogerReferencias(){
		bateria = GameObject.FindWithTag("bateriaUI");
		protagonista = GameObject.FindWithTag("prota");
		cntGeneral = protagonista.GetComponent<controlGeneral>();
		cntCamara = Camera.main.GetComponent<ControlCamara>();
		camaraPausa = GameObject.Find ("pausa").transform.FindChild("CamaraPausa").gameObject;
		camaraExito = GameObject.Find ("exito").transform.FindChild("CamaraExito").gameObject;
		camaraFracaso = GameObject.Find ("fracaso").transform.FindChild("CamaraFracaso").gameObject;
		fondos = GameObject.Find ("_fondos").transform;
	}
	
	private void recogerConstantes(){
		_constantes = GameObject.Find ("_constantes");
		_constantes.transform.SetParent(transform);
	}
	
	private void destruirConstantes(){
		Destroy (_constantes);
	}
	
	public void controlarPausa(){
		if (estado != "pausado"){
			estado = "pausado";
			camaraPausa.SetActive(true);
			camaraPausa.GetComponent<Camera>().orthographicSize = Camera.main.orthographicSize;
			//iTween.Stop();
			Time.timeScale = 0f;
			NotificationCenter.DefaultCenter().PostNotification(this, "pausar");
		} else if (estado == "pausado"){
			camaraPausa.SetActive(false);
			camaraPausa.GetComponent<Camera>().orthographicSize = Camera.main.orthographicSize;
			estado = "jugando";
			//iTween.Resume ();
			Time.timeScale = 1;
			NotificationCenter.DefaultCenter().PostNotification(this, "despausar");
		}
	}
	
	private void reposicionarFondos(){
		NotificationCenter.DefaultCenter().PostNotification(this, "reposicionarParallax");
	}
	
	private void reposicionarProtagonista(){
		float desfX = 0;
		if (!GameObject.FindWithTag("posicionInicial").transform.position.Equals(ultimoCheckPointSuperado))
			desfX = 400;
		protagonista.transform.position = new Vector3(ultimoCheckPointSuperado.x - desfX, ultimoCheckPointSuperado.y, ultimoCheckPointSuperado.z);
		if (!Application.loadedLevelName.Equals("presentacion"))
			cntGeneral.estado = 0;
		else
			cntGeneral.estado = -1;
		if (armaActual != null){
			desactivarArmasDeProta();
			Debug.Log ("Metemos arma " + armaAntigua);
			protagonista.transform.FindChild(armaAntigua).gameObject.SetActive(true);
		}
	}
	
	private void desactivarArmasDeProta(){
		foreach(Transform hijo in protagonista.transform){
			if (hijo.CompareTag("arma"))
				hijo.gameObject.SetActive(false);
		}
	}
	
	private void reposicionarCamara(){
		NotificationCenter.DefaultCenter().PostNotification(this, "reposicionarCamara");
		
	}
	
	private void registrarCheckPoint(Notification notification){
		Vector3 checkPoint = (Vector3) notification.data;
		ultimoCheckPointSuperado = checkPoint;
	}
	
	private void registrarArma(Notification notification){
		string armaNueva = (string) notification.data;
		if (armaActual != null)
			armaAntigua = armaActual;
		else
			armaAntigua = "Escopeta";
		armaActual = armaNueva;
	}
	
	private void mostrarVida(){
		bateria = GameObject.FindWithTag("bateriaUI");
		if (bateria != null)
			bateria.GetComponent<Image>().sprite = bateria.GetComponent<refBateriaImagenes>().imagenes[vidaActual];
	}
	
	private void aumentarVidaProta(Notification notification){
		if (vidaActual < 3){
			vidaActual++;
			mostrarVida();
		}
	}
	
	private void quitaleVidaProtaManager(Notification notification){
		if (vidaActual > 1){
			vidaActual--;
			reiniciar ();
		} else {
			camaraFracaso.SetActive(true);
			camaraFracaso.GetComponent<Camera>().orthographicSize = Camera.main.orthographicSize;
		}
		GameObject.Find ("Canvas").SetActive(false);
	}
	
	public void salirAeleccion(){
		estado = "eleccion";
		Time.timeScale = 1;
		Application.LoadLevelAsync("eleccion");
	}
	
	public void reiniciar(){
		estado = "jugando";
		yaEstaCargado = false;
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevelName);
	}
	
	public void reiniciarDesdePrincipio(){
		ultimoCheckPointSuperado = Vector3.zero;
		reiniciar();
	}
	
	private void sacarMenuExito(Notification notification){
		camaraExito.SetActive(true);
		camaraExito.GetComponent<Camera>().orthographicSize = Camera.main.orthographicSize;
		if (GameObject.Find ("Canvas") != null)
			GameObject.Find ("Canvas").SetActive(false);
	}
	///
	private void paraAlpersonaje(Notification notification){
		protagonista.GetComponent<Rigidbody2D>().isKinematic = true;
		NotificationCenter.DefaultCenter().PostNotification(this, "acabarParallax");		
		cntGeneral.estado = 6;
		cntGeneral.animator.SetInteger("Estado", cntGeneral.estado);
	}
	
	private void reanudaAlpersonaje(Notification notification){
		protagonista.GetComponent<Rigidbody2D>().isKinematic = false;
		NotificationCenter.DefaultCenter().PostNotification(this, "empezarParallax");		
		cntGeneral.estado = 1;
		cntGeneral.animator.SetInteger("Estado", cntGeneral.estado);
	}
}
