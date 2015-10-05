using UnityEngine;
using System.Collections;

public class generarEnemigos : MonoBehaviour {

	public GameObject enemigo;
	public int minEnemigos = 1, maxEnemigos = 25;
	float delayOleada = 4f, delayEntreEnemigos = 0.01f;
	public int oleadas;

	// Use this for initialization
	void Start () {
		Invoke ("empezarAgenerar", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void empezarAgenerar(){
		StartCoroutine ("generar");
	}

	IEnumerator generar(){
		if (oleadas > 0){
			int numEnemigos = Random.Range (minEnemigos, maxEnemigos);
			yield return StartCoroutine(crearEnemigo(numEnemigos));
			Invoke ("empezarAgenerar", delayOleada);
			oleadas--;
		} else {
			StopCoroutine("crearEnemigo");
		}
	}

	IEnumerator crearEnemigo(int num){
		for(int i = 0; i < num; i++){
			Vector2 pos = new Vector2(transform.position.x + (Random.Range(0, 1)), transform.position.y);
			GameObject ene = (GameObject) Instantiate(enemigo, pos, Quaternion.identity);
			yield return new WaitForSeconds(delayEntreEnemigos);
		}


	}
}
