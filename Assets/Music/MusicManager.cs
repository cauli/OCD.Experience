using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public GameObject musicObj;
	public GameObject mainTheme;

	void Awake() {
		DontDestroyOnLoad(musicObj);
		DontDestroyOnLoad(mainTheme);
	}

	public  void PlayPuzzleTrack () {
		
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Scene") {
			mainTheme.SetActive (false);
		}
	}
}
