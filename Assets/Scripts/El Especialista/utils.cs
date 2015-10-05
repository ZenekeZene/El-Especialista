using UnityEngine;
using System.Collections;

public class utils : MonoBehaviour {

    public static float xMitadPantalla = Screen.width / 2;

    void Start(){

    }
    
    public static bool mitadPantalla(){
    	float x = 0;
    	if (Application.platform == RuntimePlatform.Android)
        	x = Input.GetTouch(0).position.x;
		else if (Application.platform == RuntimePlatform.WindowsEditor ||  Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
			x = Input.mousePosition.x;
		return (x < xMitadPantalla); // true: mitad Izquierda, false : mitad Derecha
    }

    public void moverApunto(Transform quien, string tagElemento, float vel, Vector2 desfase, string funcionAlacabar){
		Transform punto = encontrarNodoMasCercano (quien, tagElemento);
		Vector3 nuevaPos = new Vector3(punto.position.x + desfase.x, punto.position.y + desfase.y, quien.position.z);
        iTween.MoveTo(quien.gameObject, iTween.Hash("position", nuevaPos, "time", vel, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "movetopath", false, "onComplete", funcionAlacabar));
    }
    
	public void moverApunto(Transform quien, Vector3 newPosition, float vel, Vector2 desfase, string funcionAlacabar, GameObject deQuienFuncionAlacabar){
		Vector3 nuevaPos = new Vector3(newPosition.x + desfase.x, newPosition.y + desfase.y, quien.position.z);
		iTween.MoveTo(quien.gameObject, iTween.Hash("position", nuevaPos, "time", vel, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "movetopath", false, "oncompletetarget", deQuienFuncionAlacabar, "onComplete", funcionAlacabar));
    }

    public Transform encontrarNodoMasCercano(Transform quien, string tipoNodo){
        float nearestDistanceSqr = 1000000000f;
        GameObject[] nodosTorre = GameObject.FindGameObjectsWithTag(tipoNodo);
        Transform nodoMasCercano = null;
        
        // loop through each tagged object, remembering nearest one found
        foreach (GameObject nodo in nodosTorre) {
            Vector3 objectPos = nodo.transform.position;
            float distanceSqr = (objectPos - quien.position).sqrMagnitude;
            
            if (distanceSqr < nearestDistanceSqr) {
                nodoMasCercano = nodo.transform;
                nearestDistanceSqr = distanceSqr;
            }
        }
        return nodoMasCercano;
    }
    
	public static bool estaTocando(){
		if (Application.platform == RuntimePlatform.Android){
			if (Input.touchCount > 0){
				if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))// && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
					return false;
				if (Input.GetTouch(0).phase == TouchPhase.Began){
					return true;
				}
			}
		} else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.WindowsPlayer){
			if (Input.GetMouseButtonDown(0)){
				if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
					return false;
				}
				return true;
			}
		}
		return false;
	}
	
	public void cambiarZoom(Camera camara, float hastaAquiDeTam, float vel, string funcionActualizar, string funcionAlacabar, GameObject deQuienFuncionAlacabar){
		iTween.ValueTo (camara.gameObject, iTween.Hash ("from", camara.orthographicSize,
		                                         "to", ((float) hastaAquiDeTam),
		                                         "time", vel,
		                                         "easetype", iTween.EaseType.easeInExpo,
		                                     	 "onupdatetarget", camara.gameObject, 
		                                     	 "onUpdate", funcionActualizar,
		                                         "oncompletetarget", deQuienFuncionAlacabar,
		                                     	 "onComplete", funcionAlacabar));
		
	}
    
	/*
	[VIENEN DE CAMARA, ANTES DE CAMBIAR POR ITWEEN, MOVIAMOS LA CAMARA CON ESTOS METODOS A CIERTOS PUNTOS]
	bool moverA(Transform objetivo, float velocidadMax, float desfaseH, float desfaseV){
		Vector3 posObjetivo = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);
		if (Vector3.Distance(transform.position, posObjetivo) > 1050) {
			transform.position = Vector3.SmoothDamp(transform.position, posObjetivo, ref velocity, dampTime, velocidadMax);
			return false;
		}
		return true;
	}
	
	bool moverB(Transform objetivo, float velocidad, float desfaseH, float desfaseV){
		Vector3 pos = new Vector3(objetivo.position.x + desfaseH, objetivo.position.y + desfaseV, -96);
		if (Vector3.Distance(transform.position, pos) > 10) {
			transform.position = Vector3.MoveTowards(transform.position, pos, 3000 * Time.deltaTime);
			return false;
		}
		return true;	
	}
	
	public bool moverA(Transform objeto, Transform objetivo, float velocidad){
        if (Vector2.Distance(objeto.position, objetivo.position) > 1){
            objeto.position = Vector2.MoveTowards(objeto.position, objetivo.position, Time.deltaTime * velocidad);
            return false;
        }
        return true;
    }
    
	public bool moverA(Transform objeto, Transform objetivo, float velocidad, string coord){
		if (Vector2.Distance(objeto.position, objetivo.position) > 1){
			objeto.position = Vector2.MoveTowards(objeto.position, new Vector2(objetivo.position.x, transform.position.y),Time.deltaTime * velocidad);
			return false;
		}
		return true;
	}*/
}
