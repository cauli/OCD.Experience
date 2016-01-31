using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	void Update () {
		if ( Input.GetKeyUp(KeyCode.Escape) ) {
			Application.Quit ();
		}
	}

	public void LoadScene (string scene) {		
		Application.LoadLevel (scene);
	}

	public void Wiki () {
		Application.OpenURL ("https://en.wikipedia.org/wiki/Obsessive%E2%80%93compulsive_disorder");
	}

}
