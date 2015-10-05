using UnityEngine;
using System.Collections;

public class ControlCamara : MonoBehaviour {

	public string modo;
	private Transform objetivo;
	private GameObject destructorProta;
	
	public Vector2 separacionRunner, separacionShooter, separacionTorre;
	public float velShooter, velTorre;
	public float zoomNormal, zoomTorre;
	public float velZoomNormal, velZoomTorre;
	
	private Animator animator;
	private utils u;
	
	void Awake(){
		u = GetComponent<utils>();
		animator = GetComponent<Animator>();
		NotificationCenter.DefaultCenter().AddObserver(this, "reposicionarCamara");
		NotificationCenter.DefaultCenter().AddObserver(this, "empezarShooter");
		NotificationCenter.DefaultCenter().AddObserver(this, "acabarShooter");
		NotificationCenter.DefaultCenter().AddObserver(this, "empezarTorre");
		NotificationCenter.DefaultCenter().AddObserver(this, "acabarTorre");
		NotificationCenter.DefaultCenter().AddObserver(this, "quitarVidaProta");
		NotificationCenter.DefaultCenter().AddObserver(this, "nivelFinalizadoConExito");
	}
	
	void Start () {
		destructorProta = transform.Find("destructorProta").gameObject;
	}
	
	private void reposicionarCamara(Notification notification){
		Debug.Log ("Reposicionar camara");
		posicionarCamara();
	}
	
	public void posicionarCamara(){
		objetivo = GameObject.FindGameObjectWithTag("prota").transform;
		Debug.Log (objetivo);
		transform.position = new Vector3 (objetivo.position.x , objetivo.position.y, transform.position.z);
	}

	void empezarInicio(){
		cambiarZoom(zoomNormal, velZoomNormal, "");
	}
	
	/*void empezarVideojuego(Notification notification) {
		if (objetivo != null){
			modo = "seguir";
			transform.position = new Vector3 (objetivo.position.x + separacionRunner.x, objetivo.position.y + separacionRunner.y, transform.position.z);
			NotificationCenter.DefaultCenter().PostNotification(this, "empezarParallax");
		}
	}*/
	
	void FixedUpdate(){
		if (modo == "seguir"){
			if (objetivo != null)
				transform.position = new Vector3 (objetivo.position.x + separacionRunner.x, transform.position.y, transform.position.z);
		}
	}
	
	// Shooter:
	private void empezarShooter(Notification notification){
		modo = "shooter";
		NotificationCenter.DefaultCenter().PostNotification(this, "acabarParallax");
		cambiarZoom(zoomNormal, velZoomNormal, "");
		u.moverApunto (transform, "nodoCobertura", velShooter, separacionShooter, "");
		
	}
	
	private void acabarShooter(Notification notification){
		modo = "seguir";
		NotificationCenter.DefaultCenter().PostNotification(this, "empezarParallax");
	}
	
	// Torre:
	private void empezarTorre(Notification notification){
		modo = "torre";
		u.moverApunto(transform, "nodoTorreBase", velTorre, new Vector2(0, 1500), "transicionAtorreBaseTerminada");
		cambiarZoom(zoomTorre, velZoomTorre, "");
		cambiarEscalaLimitadores("_limitadoresBalas", new Vector3(2f, 2f, 1f));
		destructorProta.gameObject.SetActive(false);
		NotificationCenter.DefaultCenter().PostNotification(this, "acabarParallax");
	}
	
	private void transicionAtorreBaseTerminada(){
		u.moverApunto(transform, "nodoTorre", velTorre, separacionTorre, "transicionAtorreArribaTerminada");
	}
	
	private void transicionAtorreArribaTerminada(){
		
	}
	
	private void acabarTorre(Notification notification){
		u.moverApunto(transform, "nodoTorreBase", velTorre, new Vector2(separacionRunner.x, separacionRunner.y - 500), "salimosDeTorre");
		cambiarZoom(zoomNormal, velZoomNormal, "");
		cambiarEscalaLimitadores("_limitadoresBalas", new Vector3(1f, 1f, 1f));
		NotificationCenter.DefaultCenter().PostNotification(this, "empezarParallax");
	}
	
	private void salimosDeTorre(){
		modo = "seguir";
		Invoke ("activarDestructorTorre", 2f);
	}
	
	private void activarDestructorTorre(){
		destructorProta.SetActive(true);
	}
	
	// Otros:
	public void cambiarZoom(float hastaAquiDeTam, float vel, string funcionAlacabar){
		iTween.ValueTo (gameObject, iTween.Hash ("from", Camera.main.orthographicSize,
															 "to", ((float)hastaAquiDeTam),
		                                                     "time", vel,
		                                                     "easetype", iTween.EaseType.easeInExpo,
		                                                     "onUpdate", "actualizarTamCamara",
															 "onUpdateTarget", Camera.main.gameObject,
															 "onComplete", "zoomTerminado"));
		
	}
		
	void actualizarTamCamara (float size) {
		Camera.main.orthographicSize = size;
		if (modo != "torre")
			NotificationCenter.DefaultCenter().PostNotification(this, "ajustarTamanoCielo");
		else
			NotificationCenter.DefaultCenter().PostNotification(this, "ajustarTamanoCieloTorre");	
	}
	
	void zoomTerminado(){
		
	}

	private void cambiarEscalaLimitadores(string tagLimitadores, Vector3 vScale){
		GameObject limitadores = transform.FindChild(tagLimitadores).gameObject;
		if (limitadores != null)
			limitadores.transform.localScale = new Vector3(vScale.x, vScale.y, vScale.z);
	}
	
	private void quitarVidaProta(Notification notification){
		modo = "golpeado";
		pendulear ();
	}
	
	//Efectos:
	private void pendulear(){
		u.moverApunto(transform, "prota", velTorre, new Vector2(0, 350), "");
		cambiarZoom(zoomNormal/1.5f, velZoomTorre, "");
		animator.SetInteger("Estado", 1);
	}
	
	private void efectoQuitarVidaTerminado(){
		animator.SetInteger("Estado", 2);
		NotificationCenter.DefaultCenter().PostNotification(this, "quitaleVidaProtaManager");
	}
	
	//Exito:
	private void nivelFinalizadoConExito(Notification notification){
		u.moverApunto(transform, "prota", velTorre, new Vector2(500, 500), "sacarMenuExito");
	}
	
	private void sacarMenuExito(){
		modo = "exito";
		NotificationCenter.DefaultCenter().PostNotification(this, "sacarMenuExito");
	}
	
}
