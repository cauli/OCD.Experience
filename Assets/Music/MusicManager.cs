using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public GameObject musicObj;

	void Awake() {
		DontDestroyOnLoad(musicObj);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
