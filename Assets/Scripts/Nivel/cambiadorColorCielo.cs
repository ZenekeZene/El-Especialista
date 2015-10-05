using UnityEngine;
using System.Collections;

[System.Serializable]
public class coloresYvelocidadCambioColor{
	public Color[] colores;
	public float velocidadAcambiar;
}

public class cambiadorColorCielo : MonoBehaviour {
	
	[Tooltip ("1.-Cielo arriba, 2.-Cielo fondo, 3.-Cielo abajo.")]
	public coloresYvelocidadCambioColor diccioColor;
	//public Color[] colores;
	private bool activado = false;
	//public float velocidadAcambiar;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("prota")){
			if (activado == false){
				activado = true;
				NotificationCenter.DefaultCenter().PostNotification(this, "ajustarColorCielos", diccioColor);
			}
		}
	}
}
