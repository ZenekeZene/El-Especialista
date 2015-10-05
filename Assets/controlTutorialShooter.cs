using UnityEngine;
using System.Collections;

public class controlTutorialShooter : MonoBehaviour {

	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this, "acabarShooter");
	}
	
	private void acabarShooter(Notification notification){
		NotificationCenter.DefaultCenter().PostNotification(this, "reanudaAlpersonaje");
	}
	
}
