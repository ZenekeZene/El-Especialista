using UnityEngine;
using System.Collections;

public class controlBrazo : MonoBehaviour {

	private Vector3 initialPosition, movePosition;
	
	void Start () {
		
	}
	
	void Update () {
	
		var pos = Camera.main.WorldToScreenPoint(transform.position);
		var dir = Input.mousePosition - pos;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
	}
}
