using UnityEngine;
using System.Collections;

public class PuzzleTrack : MonoBehaviour {

	public GameObject puzzleObj;
	public GameObject puzzleIntro;
	public GameObject puzzleMain;

	void Awake() {
		DontDestroyOnLoad(puzzleObj);
		DontDestroyOnLoad(puzzleIntro);
		DontDestroyOnLoad(puzzleMain);

		AudioSource main = puzzleMain.GetComponent<AudioSource> ();
		main.PlayDelayed (27);
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != "Scene" && Application.loadedLevelName != "SecondStage") {
			puzzleIntro.SetActive (false);
			puzzleMain.SetActive (false);
		} else {
			//puzzleIntro.SetActive (true);
			puzzleMain.SetActive (true);
		}
	}
}
