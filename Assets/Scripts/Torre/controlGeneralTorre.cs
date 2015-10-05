using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlGeneralTorre : MonoBehaviour {

		public int pisos;
		public int vidaPorPiso;

		private float alturaPorPiso;
		private bool estaActivo = false;
		private bool estaDestruida = false;
		public int vidaActualDePiso;
		private TextMesh vidaUI;
		
		private Transform sueloTorre;
		private Transform physics, atrezzo;
		private controlEnemigosTorre cntEnemigos;
		private List<Transform> piezasTorre;
		
		public GameObject prefabEfectoHumo;
		private float anchoCuerpoTorre;
		
		void Awake(){
			sueloTorre = transform.FindChild("sueloTorre");
			physics = transform.FindChild("_fisics");
			atrezzo = transform.FindChild("_atrezzo");
			alturaPorPiso = calcularAlturaPorPiso();
			cntEnemigos = transform.FindChild("_hordas").GetComponent<controlEnemigosTorre>();
			piezasTorre = buscarPiezasTorre();
			anchoCuerpoTorre = piezasTorre[0].renderer.bounds.size.x;
		}
		
		void Start () {
			dibujarVidaUI();

			NotificationCenter.DefaultCenter().AddObserver(this, "empezarTorre");
			NotificationCenter.DefaultCenter().AddObserver(this, "restarVidaTorre");
			NotificationCenter.DefaultCenter().AddObserver(this, "acabarTorre");
		}

		private void dibujarVidaUI() {
			vidaUI = transform.Find("vidaUI").GetComponent<TextMesh>();
			vidaActualDePiso = vidaPorPiso;
			vidaUI.text = "" + vidaActualDePiso;
		}
		
		private float calcularAlturaPorPiso(){
			//Bounds bounds = physics.FindChild("paredTorre1").renderer.bounds;
			//return (bounds.size.y / pisos);
			return atrezzo.FindChild ("cuerpoTorre").renderer.bounds.size.y;
		}

		void empezarTorre(Notification notification){
			if (estaActivo == false){
				estaActivo = true;
				cntEnemigos.activarHordas();
			}
		}
		
		private void acabarTorre(Notification notification){
			if (estaActivo == true){
				estaActivo = true;
				if (estaDestruida == false)
					abrirPuerta();
			}
		}
		
		void restarVidaTorre(Notification notificacion){
			if (estaActivo == true){
				if (notificacion.sender.transform.IsChildOf(transform)){
					if (pisos > 0){
						if (vidaActualDePiso >= 1){
							vidaActualDePiso -= 1;
							vidaUI.text = "" + vidaActualDePiso;
							rotarTorre();
						} else {
							bajarPisoTorre();
							vidaActualDePiso = vidaPorPiso;
						}
					} else if (pisos == 1){
						physics.FindChild("paredTorre1").gameObject.SetActive(false);
						physics.FindChild("paredTorre2").gameObject.SetActive(false);
					} else if (pisos <= 0)
						destruirTorre();
				}
			}
		}
		
		private void bajarPisoTorre(){
			sueloTorre.Translate(0, -alturaPorPiso - 50, 0);
			physics.localScale -= new Vector3(0, alturaPorPiso/5, 0);
			generarEfecto(piezasTorre[0].position);
			piezasTorre[0].gameObject.SetActive(false);
			piezasTorre.RemoveAt(0);
			pisos--;
			
		}
		
		private void generarEfecto(Vector3 position){
			float tamCuerpoTorreMedio = anchoCuerpoTorre/2;
			for(int i = 0; i < 5; i++){
				GameObject humo = Instantiate(prefabEfectoHumo, new Vector3(position.x - tamCuerpoTorreMedio + (i * 200f), position.y, position.z), Quaternion.identity) as GameObject;
				humo.transform.localScale = new Vector3(Random.Range (1, 3), Random.Range (1, 3), 1);
			}
		}
		
		private void rotarTorre(){
			
		}
		
		private void destruirTorre(){
			Destroy(atrezzo.gameObject);
			Destroy(physics.gameObject);
			Destroy(sueloTorre.gameObject);
			estaDestruida = true;
		}
		
		private void abrirPuerta(){
			physics.FindChild("puerta2").gameObject.SetActive(false);
			physics.FindChild ("paredTorre2").collider2D.isTrigger = true;
		}
		
		private List<Transform> buscarPiezasTorre(){
			List<Transform> piezasTorre = new List<Transform>();
			foreach(Transform pieza in atrezzo)
				piezasTorre.Add (pieza);
			return piezasTorre;
		}
		
	}
