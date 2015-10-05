using UnityEngine;
using System.Collections;

public class controlDiapositivaPersonajes : MonoBehaviour {

	private GameObject _zombies;
	public bool puedeTocarParaZombies = false;
	//private int contZombies = 0;
	
	void Start () {
	
	}
	
	void Update () {
		if (puedeTocarParaZombies){
			if (utils.estaTocando()){
				puedeTocarParaZombies = false;
				aparecerZombie();
			}
		}
	}
	
	void OnEnable(){
		_zombies = transform.parent.FindChild("_personajes").gameObject;
		_zombies.SetActive(true);
		puedeTocarParaZombies = true;
	}
	
	private void aparecerZombie(){
		if (_zombies.transform.childCount != 0)
			_zombies.transform.GetChild(0).gameObject.SetActive(true);
		else
			ensenarElResto();
	}
	
	public void puedeTocarParaOtroZombie(){
		Debug.Log ("puedeTocarParaOtroZombie");
		Invoke ("ahoraPuedeTocarParaOtroZombie", 2f);
	}
	
	private void ahoraPuedeTocarParaOtroZombie(){
		puedeTocarParaZombies = true;
	}
	
	private void ensenarElResto(){
		puedeTocarParaZombies = false;
		GameObject _resto = transform.parent.FindChild("panel").FindChild ("_resto").gameObject;
		_resto.SetActive(true);
	}
}
