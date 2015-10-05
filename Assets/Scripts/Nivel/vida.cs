using UnityEngine;
using System.Collections;

public class vida : MonoBehaviour {

	public bool estaActivo = false;
	private Animator animator;

	void Awake(){
		animator = GetComponent<Animator>();
	}

	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("prota")){
			if (GameObject.Find ("Manager").GetComponent<Manager>().vidaActual < 3){
				if (estaActivo == false){
					estaActivo = true;
					NotificationCenter.DefaultCenter().PostNotification(this, "aumentarVidaProta");
					animator.SetBool("dispararse", true);
					if (audio != null)
						audio.Play();
				}
			}
		}
	}
	
	private void destruir(){
		Destroy(gameObject);
	}
}
