using UnityEngine;
using System.Collections;

public class checkPoint : MonoBehaviour {

	public bool superado = false;
	private Animator animator;
	
	void Awake(){
		animator = transform.FindChild("Star").GetComponent<Animator>();
	}
	
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("prota")){
			if (superado == false){
				superado = true;
				NotificationCenter.DefaultCenter().PostNotification(this, "registrarCheckPoint", transform.position);
				if (audio != null)
					audio.Play();
			}
			animator.SetBool("estaSuperado", superado);
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("prota")){
			animator.SetBool ("estaSuperado", false);
		}
	}
}
