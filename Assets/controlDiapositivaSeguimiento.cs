using UnityEngine;
using System.Collections;

public class controlDiapositivaSeguimiento : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseUpAsButton(){
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}
}
