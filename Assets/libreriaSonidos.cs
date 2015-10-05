using UnityEngine;
using System.Collections;

public class libreriaSonidos : MonoBehaviour {

	public AudioClip[] sonidosZombiesMorir;
	public AudioClip[] musicaFondo;
	
	void Start () {
	
	}
	
	public AudioClip dameSonidoMorirAleatorio(){
		return (sonidosZombiesMorir[Random.Range(0, sonidosZombiesMorir.Length)]);
	}
	
	public AudioClip dameMusicaFondoAleatoria(){
		return (musicaFondo[Random.Range(0, musicaFondo.Length)]);
	}
}
