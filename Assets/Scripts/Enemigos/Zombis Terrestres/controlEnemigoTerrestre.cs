using UnityEngine;
using System.Collections;

public class controlEnemigoTerrestre : MonoBehaviour {

	private controlGeneralEnemigo cntGeneral;

	public LayerMask mascaraSuelo, mascaraEnemigos;
	[System.NonSerialized]
	public BoxCollider2D collider;
	private Vector3 s, c;
	public bool enSuelo;
	private Ray ray;
	private RaycastHit hit;

	public bool esPasivo = true;
	
	void Awake(){
		collider = GetComponent<BoxCollider2D>();	
		cntGeneral = GetComponent<controlGeneralEnemigo>();
		NotificationCenter.DefaultCenter().AddObserver(this, "empezarAlerta");
	}

	void Start(){
		s = collider.size;
		c = collider.center;
		cntGeneral.girarse ();
		if (cntGeneral.estado != "pasivo")
			esPasivo = false;
	}

	void FixedUpdate(){
		if (cntGeneral.estado == "alerta")
			seguir ();
		
		enSuelo = comprobarSiEstaEnSuelo();
		if (enSuelo == true){
			if (cntGeneral.estado == "golpeado"){
				if (esPasivo == false)
					cntGeneral.estado = "alerta";
				else
					cntGeneral.estado = "pasivo";
				//cntGeneral.girarse ();
			}
		}
	}

	bool comprobarSiEstaEnSuelo(){
	
		Vector2 pos = transform.position;
		for(int i = 1; i <= 1; i++) {
			float x = (pos.x + c.x - s.x/2) + s.x/2 * i; // Left, centre and then rightmost point of collider
			float y = pos.y + c.y + s.y/2 * - 1;
			ray = new Ray(new Vector2(x,y), new Vector2(1, -10));
			Debug.DrawRay(ray.origin,ray.direction, Color.red, 0.01f);
			if (Physics2D.Raycast(ray.origin, ray.direction, 10, mascaraSuelo) || Physics2D.Raycast(ray.origin, -Vector2.up, 10, mascaraEnemigos))
				return true;
		}
		return false;
	}

	public void seguir(){
		int direction = cntGeneral.aqueLadoDeMi(cntGeneral.objetivo);
		float velActual = Mathf.Abs(rigidbody2D.velocity.x);
		if (velActual < cntGeneral.vel)
			rigidbody2D.AddForce(new Vector2(1000 * (direction * -1), 0), ForceMode2D.Force);
	}
	
	public void pararse(){
		rigidbody2D.velocity = new Vector2(0, 0);
	}

	public void empezarAlerta(Notification notificacion){
		if (cntGeneral.estaSuelto == false){
			if (gameObject.activeInHierarchy){
				Transform quienLoEnvia = notificacion.sender.transform;
				if (quienLoEnvia.gameObject.activeSelf){
					if (cntGeneral.soySuHijo(quienLoEnvia) || cntGeneral.soySuNieto(quienLoEnvia)){
						if (cntGeneral.estado == "esperando"){
							cntGeneral.estado = "alerta";
							cntGeneral.girarse();
							seguir();
						}
					}
				}
			}
		}
	}
	
}