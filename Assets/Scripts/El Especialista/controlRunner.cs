using UnityEngine;
using System.Collections;

public class controlRunner : MonoBehaviour {

	private controlGeneral gen;

	public bool estaEnSuelo;
	public bool tieneParedDer;

	public float velMinima;
	public float velActual;
	public float fuerzaSalto;

    private float xMitadPantalla;

	public Transform comprobadorSuelo;
	public float comprobadorRadio;
	private Transform comprobadorPared;
	private controlPersonajeSonidoSalto cntSonidoSalto;
	public LayerMask mascaraSuelo;
    private Animator animator;
	private BoxCollider2D collider;
	private Vector3 s, c;
	private Ray ray;
	private RaycastHit hit;

	void Awake() {
		gen = GetComponent<controlGeneral>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        comprobadorPared = transform.FindChild("comprobadorPared");
		cntSonidoSalto = transform.FindChild("Tronco").GetComponent<controlPersonajeSonidoSalto>();
	}

	void Start () {
        s = collider.size;
        c = collider.center;
        //gen.estado = 1;
        animator.SetInteger("Estado", gen.estado);
	}

	void Update () {
		if (gen.estado == 0){
			if (utils.estaTocando()){
				gen.estado = 1;
				animator.SetInteger("Estado", gen.estado);
				NotificationCenter.DefaultCenter().PostNotification(this, "empezarParallax");
			}
		} else if (gen.estado == 1){
			if (utils.estaTocando ()){
				if (utils.mitadPantalla()){
					if (estaEnSuelo == true)
						saltar();
				}
			}
		}
		
		animator.SetBool("EstaEnSuelo", estaEnSuelo);
		animator.SetBool("TieneParedDer", tieneParedDer);
	}

    void FixedUpdate() {
    	if (gen.estado == 1){
			//estaEnSuelo = comprobarSiEstaEnSuelo();
			estaEnSuelo = Physics2D.OverlapCircle (comprobadorSuelo.position, comprobadorRadio, mascaraSuelo);
			correr();
			tieneParedDer = comprobarSiTieneParedDerecha();
			aplicarFuerzaSiEstaBloqueado();
        }
    }
	
	bool comprobarSiEstaEnSuelo(){
        
		Vector2 pos = transform.position;
		for(int i = 0; i < 3; i++) {
			float x = (pos.x + c.x - s.x/2) + (s.x/2 * i); // Left, centre and then rightmost point of collider
			float y = pos.y + c.y + s.y/2 * -1 - 20;
			ray = new Ray(new Vector2(x,y), new Vector2(0, -1));
			Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics2D.Raycast(ray.origin, -Vector2.up, 10, mascaraSuelo)){ // Linecast mas preciso, mas lento, probar
				estaEnSuelo = true;
				return true;
			}
		}
		return false;
	}
	
	bool comprobarSiTieneParedDerecha(){
		RaycastHit2D hit2 = Physics2D.Raycast(comprobadorPared.position, Vector2.right, 10f, mascaraSuelo);
		return (hit2.collider!=null);
	}
	
	public void correr(){
		if (estaEnSuelo == true){
			if (rigidbody2D.velocity.x < 200f)
				rigidbody2D.AddForce(new Vector2(velActual, 0), ForceMode2D.Force);
				//rigidbody2D.velocity = new Vector2(22, 0);
		}
	}
	
	void aplicarFuerzaSiEstaBloqueado(){
		if ((rigidbody2D.velocity.x < 3) && (tieneParedDer == false)) {
			if ((rigidbody2D.velocity.y < 1) && (tieneParedDer == false)){
				rigidbody2D.AddForce(new Vector2(200, 100), ForceMode2D.Force);
			}
		}
	}
	
	public void saltar(){
		//if (rigidbody2D.velocity.y < 20)
		rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, fuerzaSalto);
		//rigidbody2D.AddForce (new Vector2(25, fuerzaSalto));
		cntSonidoSalto.sonarSalto();
	}
	
	public void saltar(float xx, float yy){
		//if (rigidbody2D.velocity.y < 20)
		rigidbody2D.AddForce (new Vector2(xx, yy));
	}
	
}
