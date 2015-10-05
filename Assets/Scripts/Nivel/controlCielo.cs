using UnityEngine;
using System.Collections;

public enum alinearPosicion { Original, Arriba, Medio, Abajo }

public class controlCielo : MonoBehaviour {

	public alinearPosicion alineacion;
	public float desfaseEnY;
	public Color colorOriginal;
	private Color colorActual;

	[Tooltip ("1 para ajustar a el objeto target. 2 para ajustar a la mitad. Tanto con textures como con sprites.")]
	public float ajustarEnAnchura, ajustarEnAltura;
	
	/*void OnSceneGUI(){
		ajustarTamano(ajustarEnAnchura, ajustarEnAltura);
		ajustarAlineacion();
	}*/
	
	void Start () {
		ajustarTamano(ajustarEnAnchura, ajustarEnAltura);
		ajustarAlineacion();
		colorActual = colorOriginal;
		//ajustarColor();
		NotificationCenter.DefaultCenter().AddObserver(this, "ajustarTamanoCielo");
		NotificationCenter.DefaultCenter().AddObserver(this, "ajustarTamanoCieloTorre");
	}

	private void ajustarTamanoCielo(Notification notification){
		ajustarTamano(ajustarEnAnchura, ajustarEnAltura);
		ajustarAlineacion();
	}
	
	private void ajustarTamanoCieloTorre(Notification notification){
		ajustarTamano(ajustarEnAnchura, (name.Equals("cieloAbajo")?2f:ajustarEnAltura));
		ajustarAlineacion();
	}
	
	private void ajustarTamano(float ajustarEnAnchura, float ajustarEnAltura){
		SpriteFunctions.ResizeSpriteToScreen(this.gameObject, Camera.main, ajustarEnAnchura, ajustarEnAltura);
	}
	
	private void ajustarAlineacion(){
		float iniY = Camera.main.transform.position.y;
		if (alineacion.Equals(alinearPosicion.Arriba)){
			iniY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
			iniY = iniY - renderer.bounds.size.y / 2;
		} else if (alineacion.Equals(alinearPosicion.Abajo)){
			iniY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
			iniY = iniY + renderer.bounds.size.y / 2;
		} else if (alineacion.Equals(alinearPosicion.Medio)){
			iniY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y;
			iniY = iniY + desfaseEnY + renderer.bounds.size.y / 2;
		}
		transform.position = new Vector3(Camera.main.transform.position.x, iniY, transform.position.z);
	}
	
	private void ajustarColor(){
		gameObject.GetComponent<SpriteRenderer>().color = new Color(colorActual.r, colorActual.g, colorActual.b);
	}
	
	public void ajustarColor(Color color, float vel){
		gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		iTween.ColorTo(gameObject, color, vel);
	}
	
}
