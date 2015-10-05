using UnityEngine;
using System.Collections;

public class controlEleccionUI : MonoBehaviour {

	void Start () {
	
	}
	
	public void irAmenuPrincipal(){
		Application.LoadLevel("menu");
	}
	
	public void irApresentacion(){
		Application.LoadLevel("presentacion");
	}

}
