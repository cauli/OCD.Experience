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
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != "Scene" || Application.loadedLevelName != "SecondStage") {
			puzzleMain.SetActive (false);
		}
	}
}
