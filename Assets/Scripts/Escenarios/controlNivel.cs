using UnityEngine;
using System.Collections;

public class controlNivel : MonoBehaviour {
	
	private int indexNivel;
	private controlNivelesEleccion cntNivelesEleccion;
	public string nombreNivel;
	
	void Start () {
		indexNivel = int.Parse(name.Substring(6));
		cntNivelesEleccion = transform.parent.GetComponent<controlNivelesEleccion>();
	}
	
	void Update () {
	
	}
	
	void OnMouseUpAsButton (){
		if (cntNivelesEleccion.estaCargando == false)
			NotificationCenter.DefaultCenter().PostNotification(new Notification(this, "nivelSeleccionado", indexNivel));
	}
	
	public void activar(){
		StartCoroutine("CargarNivel");
	}
	
	IEnumerator CargarNivel () {
		//GameObject.Find("Manager").GetComponent<Manager>().estado = "jugando";
		//GameObject.Find("Manager").GetComponent<Manager>().esReinicioDeNivel = true;
		AsyncOperation operation = Application.LoadLevelAsync(nombreNivel);
		while(!operation.isDone) {
			yield return operation.isDone;
			Debug.Log("loading progress: " + operation.progress);
		}
		Debug.Log("load done");
	}
}
