using UnityEngine;
using System.Collections;

public class controlPersonajeSonidoSalto : MonoBehaviour {	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void sonarSalto(){
		if (audio != null)
			audio.Play();
	}
}
