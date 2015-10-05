using UnityEngine;
using System.Collections;

public class videoController : MonoBehaviour {
	#if UNITY_EDITOR
		private MovieTexture movie;
	#endif
	
	#if UNITY_WEBPLAYER
		private MovieTexture movie2;
	#endif

	void Start () {
		
		#if UNITY_EDITOR
			movie = renderer.material.mainTexture as MovieTexture;
		#endif
		
		#if UNITY_WEBPLAYER
			movie2 = renderer.material.mainTexture as MovieTexture;
		#endif
		
	}
	
	void OnMouseUpAsButton(){
		#if UNITY_EDITOR
			movie.Play ();
		#endif
		
		#if UNITY_WEBPLAYER
			movie2.Play ();
		#endif
		
		#if UNITY_ANDROID
		Handheld.PlayFullScreenMovie("https://youtu.be/wD1tPhrLrjY", Color.black, FullScreenMovieControlMode.CancelOnInput);
		#endif
	}
	
}
