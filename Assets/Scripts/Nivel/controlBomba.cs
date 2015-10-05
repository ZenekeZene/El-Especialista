using UnityEngine;
using System.Collections;

public class controlBomba : MonoBehaviour {

	public GameObject prefabOnda;

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "bala" || other.gameObject.tag == "onda"){
			explotar();
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
	
	void explotar(){
		for(int i = -5; i < 5; i++){
			GameObject onda = Instantiate(prefabOnda, transform.position, Quaternion.identity) as GameObject;
			//int hor = Random.Range (-10, 10);
			//int ver = Random.Range(2, 5);
			//onda.rigidbody2D.AddForce(new Vector2((i * 500), (i * 500)));
		}
	}
}
