using UnityEngine;
using System.Collections;

public class controlTutorialSalto : MonoBehaviour {

	private controlTutorial cntTutorial;
	public int contSalto = 0;
	private bool esPrimerSalto = false;

	void Start () {
		cntTutorial = GetComponent<controlTutorial>();
		NotificationCenter.DefaultCenter().AddObserver(this, "registraSalto");
	}
	
	private void registraSalto(Notification notification){
		if (esPrimerSalto == false){
			esPrimerSalto = true;
			Invoke ("quitarTutorial", 10f);
		}
		contSalto++;
		Debug.Log ("Salto #" + contSalto);
	}
	
	private void quitarTutorial(){
		cntTutorial.quitarTutorial();
	}
	
}
