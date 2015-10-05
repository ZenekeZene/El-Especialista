using UnityEngine;
using System.Collections;

public enum desplazamientoScroll { Horizontal, Vertical }
public enum tipoSprite { Textura, Sprite2D }

public class scroll : MonoBehaviour {

	public bool enMovimiento = true;
	public float velocidad;
	private float tiempoInicio;
	public desplazamientoScroll desplazamiento;
	private Vector2 desp;
	public tipoSprite tipo;
	private bool activo = false;
	
	void Start () {
		tiempoInicio = Time.time;
		empezarScroll ();
	}
	
	void empezarScroll(){
		enMovimiento = true;
		tiempoInicio = Time.time;
		activo = true;
	}
	
	void Update () {
		if (activo){
			if (enMovimiento) {
				if (desplazamiento.Equals(desplazamientoScroll.Horizontal))
					desp = new Vector2 (((Time.time - tiempoInicio) * velocidad)%1, 0);
				else
					desp = new Vector2 (0, ((Time.time - tiempoInicio) * velocidad)%1);
				if (tipo.Equals(tipoSprite.Textura))
					renderer.material.mainTextureOffset = desp;
			}
		}
	}
	
	void FixedUpdate(){
		if (activo){
			if (tipo.Equals(tipoSprite.Sprite2D))
				transform.position = new Vector3(transform.position.x - velocidad , transform.position.y, transform.position.z);
		}
	}
}
