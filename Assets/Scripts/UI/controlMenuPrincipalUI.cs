using UnityEngine;
using System.Collections;

public class controlMenuPrincipalUI : MonoBehaviour {

	void Start () {
	
	}
	
	public void jugar(){
		Application.LoadLevel("eleccion");
	}
	
	public void salir(){
		Application.Quit();
	}
}
