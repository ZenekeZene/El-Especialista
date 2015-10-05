using UnityEngine;
using System.Collections;

public class controlBalaGunner : MonoBehaviour {

	[System.NonSerialized]
	public Transform objetivo;
	public float velocidad;
	[System.NonSerialized]
	public int direccion;
	
	void Awake(){
		objetivo = GameObject.FindWithTag("prota").transform;
	}

	void Start () {
		direccion = aqueLadoDeMi(objetivo);
	}
	
	void Update () {
		transform.Translate((Vector3.right * direccion) * Time.deltaTime * velocidad);
	}
	
	public int aqueLadoDeMi(Transform target){
		return ((transform.position.x > target.position.x)? -1 : 1);
	}
}
