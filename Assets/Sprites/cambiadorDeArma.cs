using UnityEngine;
using System.Collections;

public class cambiadorDeArma : MonoBehaviour {

	public GameObject arma;
	private bool estaActivo = false;
	//public ParticleSystem efectoCambioArma;
	private Animator animator;
	
	void Awake(){
		animator = GetComponent<Animator>();
	}
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("prota")){
			if (estaActivo == false){
				estaActivo = true;
				other.GetComponent<controlShooter>().cambiarArma(arma);
				dispararEfecto();
			}
		}
	}
	
	public void dispararEfecto(){
		/*Vector3 position = transform.position;
		ParticleSystem escoria = GameObject.Instantiate(efectoCambioArma, position, efectoCambioArma.transform.rotation) as ParticleSystem;
		escoria.renderer.sortingLayerName = "sangre";
		escoria.particleSystem.startSpeed = escoria.particleSystem.startSpeed;
		escoria.Play();
		Destroy(escoria.gameObject, escoria.duration + escoria.startLifetime); */
		if (audio != null)
			audio.Play ();
		animator.SetBool("dispararse", true);
	}
	
	private void morir(){
		Destroy(gameObject);
	}
}
