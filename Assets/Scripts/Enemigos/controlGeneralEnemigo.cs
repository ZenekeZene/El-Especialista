using UnityEngine;
using System.Collections;

public class controlGeneralEnemigo : MonoBehaviour {

	public string estado;
	[System.NonSerialized]
	public Transform objetivo;
	public int vida;
	public float velMin, velMax, vel;
	public bool estaSuelto;
	[System.NonSerialized]
	public Animator animator;
	
	public ParticleSystem efectoSangre, efectoEscoria, efectoExplosionMuerte;

	void Awake(){
		animator = GetComponent<Animator>();
		objetivo = GameObject.FindWithTag("prota").transform;
	}

	void Start(){
		vel = Random.Range(velMin, velMax);
		estaSuelto = transform.parent == null;
		girarse ();
		
	}
	
	void OnLevelWasLoaded(int level){
		if (Application.loadedLevelName != "eleccion")
			objetivo = GameObject.FindWithTag("prota").transform;
	}

	public int aqueLadoDeMi(Transform target){
		return ((transform.position.x > target.position.x)? 1 : -1);
	}

	public void girarse(){
		int direction = aqueLadoDeMi(objetivo);
		transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
	}

	public void restarVida(bool sinMatar){
		if (audio != null)
			audio.Play ();
		if (vida > 1)
			vida--;
		else {
			if (sinMatar == false)
				muerto ();
		}
	}

	public bool soySuHijo(Transform padre){
		if (transform == null || padre == null) return false;
		return (transform.IsChildOf(padre));
	}
	
	public bool soySuNieto(Transform abuelo){
		if (transform.parent == null) return false;
		return (transform.parent.transform.IsChildOf(abuelo));
	}
	
	public bool soySuBiznieto(Transform bisabuelo){ // En caso de los voladores (porque tienen un gameObject padre contenedor de por medio)
		return (transform.parent.transform.parent.transform.IsChildOf(bisabuelo));
	}
	
	public void dispararEfecto(string tipo){
		ParticleSystem particulas = null;
		if (tipo == "sangre")
			particulas = efectoSangre;
		else if (tipo == "escoria")
			particulas = efectoEscoria;
		else if (tipo == "muerte")
			particulas = efectoExplosionMuerte;
		dispararParticulas(particulas);
	}
	
	public void dispararParticulas(ParticleSystem prefabEfecto){
		Vector3 position = transform.position;
		ParticleSystem efecto = GameObject.Instantiate(prefabEfecto, position, efectoEscoria.transform.rotation) as ParticleSystem;
		efecto.renderer.sortingLayerName = "sangre";
		int direction = aqueLadoDeMi(objetivo);
		efecto.particleSystem.startSpeed = efecto.particleSystem.startSpeed * direction;
		efecto.Play();
		Destroy(efecto.gameObject, efecto.duration + efecto.startLifetime); 
	}
	
	public void muerto(){
		if (audio != null)
			audio.Play ();
		dispararEfecto("muerte");
		NotificationCenter.DefaultCenter().PostNotification(this, "restarEnemigo"); //solo hacerlo cuando pertenezca a cobertura
		if (estaSuelto == false){
			if (transform.parent.gameObject.name == "Zombie Volador") // Chapuza para los voladores (y para cualquiera cuyo padre es un contenedor 'vacio')
				Destroy (transform.parent.gameObject);
		}
		desaparecerHijos();
		GetComponent<BoxCollider2D>().enabled = false;
		if (audio != null){
			if (audio.clip != null)
				Destroy (this.gameObject, audio.clip.length);
		}
			
		else
			Destroy (this.gameObject);
	}
	
	private void desaparecerHijos(){
		foreach(Transform pieza in transform){
			if (pieza.GetComponent<SpriteRenderer>())
				pieza.GetComponent<SpriteRenderer>().enabled = false;
			if (pieza.childCount > 0){
				foreach(Transform hijoDePieza in pieza){
					if (hijoDePieza.GetComponent<SpriteRenderer>())
						hijoDePieza.GetComponent<SpriteRenderer>().enabled = false;
				}
			}
		}
	}
	
	public void eliminar(){
		Destroy (gameObject);
	}
	
}