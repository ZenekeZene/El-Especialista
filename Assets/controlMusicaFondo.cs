using UnityEngine;
using System.Collections;

public class controlMusicaFondo : MonoBehaviour {

	void Start () {
		if (audio != null){
			audio.clip = GameObject.Find("libreriaSonidos").GetComponent<libreriaSonidos>().dameMusicaFondoAleatoria();
			audio.Play();
		}
	}

}
