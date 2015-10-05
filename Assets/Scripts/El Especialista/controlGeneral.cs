using UnityEngine;
using System.Collections;

public class controlGeneral : MonoBehaviour {

	public int estado = 0; //-1.-pausado, 0.-inicio, 1.-runner, 2.- Shooter,  3.- torre, 4.- golpeado, 5.-respawn, 6.- exito
	private int estadoAntesDePausar;
	public GameObject prefabBocadilloCalavera;
	
	[System.NonSerialized]
	public Animator animator;
	[System.NonSerialized]
	public Rigidbody2D cuerpoFisico;
	private Manager manager;
	private controlRunner cntRunner;
	private controlShooter cntShooter;
	private controlTorre cntTorre;
	private utils u;
	private Transform nodoCobertura, nodoTorreBase, nodoTorre;
	
	public AudioClip sonidoMuerte;

	void Awake(){
		cntRunner = GetComponent<controlRunner>();
		cntShooter = GetComponent<controlShooter>();
		cntTorre = GetComponent<controlTorre>();
		u = GetComponent<utils>();
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		animator = GetComponent<Animator>();
		cuerpoFisico = GetComponent<Rigidbody2D>();
		
		NotificationCenter.DefaultCenter().AddObserver(this, "pausar");
		NotificationCenter.DefaultCenter().AddObserver(this, "despausar");
	}

	void Start(){
		
	}
	
	public void girarseIzquierda(){
		transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
	}
	
	public void girarseDerecha(){
		transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
	}
	
	private void pausar(Notification notification){
		estadoAntesDePausar = estado;
		estado = -1;
	}
	
	private void despausar(Notification notification){
		estado = estadoAntesDePausar;
	}
	
	private void OnCollisionEnter2D(Collision2D collision){
		Transform other = collision.transform;
		if (other.gameObject.CompareTag("enemigo")){
			quitarVida(true);
			//StartCoroutine(atrasarEnemigo(other));
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision){
		Transform other = collision.transform;
		if (other.gameObject.CompareTag("enemigo")){
			quitarVida(true);
			//StartCoroutine(atrasarEnemigo(other));
		}
	}
	
	public void quitarVida(bool esKinematic){
		if (estado != 4){
			estado = 4;
			animator.SetInteger("Estado", estado);
			cuerpoFisico.isKinematic = esKinematic;
			if (audio != null){
				audio.clip = sonidoMuerte;
				audio.Play();
			}
			sacarBocadilloCalavera();
			NotificationCenter.DefaultCenter().PostNotification(this, "acabarParallax");
			NotificationCenter.DefaultCenter().PostNotification(this, "quitarVidaProta");
		}
	}
	
	public void sacarBocadilloCalavera(){
		GameObject bocadilloCalavera = Instantiate(prefabBocadilloCalavera, transform.position, Quaternion.identity) as GameObject;
	}
	
	/*IEnumerator atrasarEnemigo(Transform enemigo){
		yield return new WaitForSeconds(1);
		//enemigo.GetComponent<Rigidbody2D>().collider2D.isTrigger = true;
		enemigo.GetComponent<Rigidbody2D>().AddForce(new Vector2(30000, 8000));
	}*/

}