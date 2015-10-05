using UnityEngine;
using System.Collections;

public class controlCamaraEleccion : MonoBehaviour {
	
	public float vel;
	Vector3 dragOrigin;
	
	void Start () {
		//Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y
		
	}
	
	void Update () {
		if ( Input.GetMouseButtonDown(0)){
			dragOrigin = new Vector3 (Input.mousePosition.x, 0f, 0f);
			dragOrigin = camera.ScreenToWorldPoint(dragOrigin);
		}
				
		if ( Input.GetMouseButton(0)){
			
			Vector3 currentPos = new Vector3 (Input.mousePosition.x, 0f, 0f);
			currentPos = camera.ScreenToWorldPoint(currentPos);
			Vector3 movePos = dragOrigin - currentPos;
			Vector3 poss = transform.position + movePos;
			if (poss.x > 4f && poss.x < 13f)
				transform.position = poss;
		}
	}
	
	
}
