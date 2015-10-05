using UnityEngine;
using System.Collections;

public class ordenarCapaDibujo : MonoBehaviour {

	void Start () {
		gameObject.renderer.sortingLayerName = "UI";
		gameObject.renderer.sortingOrder = 20;
	}

}
