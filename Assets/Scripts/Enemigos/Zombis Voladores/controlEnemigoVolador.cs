using UnityEngine;
using System.Collections;

public class controlEnemigoVolador : MonoBehaviour {

	private controlGeneralEnemigo cntGeneral;

	Transform[] path;
	Transform contenedorNodos;
		
	void OnDrawGizmos(){
		path = buscarNodosVoladores();
		iTween.DrawPath(path);	
	}
	
	void Awake(){
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		path = buscarNodosVoladores();
		NotificationCenter.DefaultCenter().AddObserver(this, "empezarAlerta");
	}
	
	void Start () {
		
	}
	
	Transform[] buscarNodosVoladores(){
		contenedorNodos = transform.parent.FindChild("_nodosVoladores");
		Transform[] nodos = new Transform[contenedorNodos.childCount];
		int i = 0;
		foreach(Transform t in contenedorNodos){
			nodos[i++] = t;
		}
		return nodos;
	
	}
	
	void pathInicialTerminado(){
		cntGeneral.estado = "llegando";
	}
	
	void Update(){
		if (cntGeneral.estado == "llegando"){
			transform.position = Vector3.MoveTowards(transform.position, cntGeneral.objetivo.position, cntGeneral.vel);
		}
	}
	
	void rotarConenedorNodos(){
		// LookAt 2D
		// get the angle
		Vector3 norTar = (cntGeneral.objetivo.position-transform.position).normalized;
		float angle = Mathf.Atan2(norTar.y,norTar.x)*Mathf.Rad2Deg;
		// rotate to angle
		Quaternion rotation = new Quaternion ();
		rotation.eulerAngles = new Vector3(0,0,angle-180); // venia con 90 no con 180
		contenedorNodos.transform.rotation = rotation;
	}
	
	public void empezarAlerta(Notification notificacion){
		Transform quienLoEnvia = notificacion.sender.transform;
		if (cntGeneral.soySuHijo(quienLoEnvia) || cntGeneral.soySuNieto(quienLoEnvia)){
			cntGeneral.girarse ();
			Invoke("empezarAmoverse", 4f);
		}
	}
	
	private void empezarAmoverse(){
		cntGeneral.estado = "alerta";
		rotarConenedorNodos(); // solo rotar cuando el enemigo este activo, no como ahora
		//vel = Random.Range(velMin, velMax);
		iTween.MoveTo(gameObject, iTween.Hash("path",path,"time",cntGeneral.vel,"easetype",iTween.EaseType.linear,"looptype",iTween.LoopType.none,"movetopath",false, "onComplete", "pathInicialTerminado"));
	}
	
	void OnCollisionEnter2D(Collision2D colision){
		GameObject other = colision.gameObject;
		if (cntGeneral.estado == "alerta" || cntGeneral.estado == "pasivo" || cntGeneral.estado == "llegando"){
			if (other.CompareTag("bala")) {
				Destroy (other);
				cntGeneral.restarVida(false);
				cntGeneral.dispararEfecto("sangre");
				cntGeneral.dispararEfecto("escoria");
			}
		}
	}

}
