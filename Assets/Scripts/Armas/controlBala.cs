using UnityEngine;
using System.Collections;

public class controlBala : MonoBehaviour {

	public float velocidad;

	void Start () {
		
	}
	
	void Update () {
		
		transform.Translate(Vector3.right * Time.deltaTime * velocidad);
	}
	
}
