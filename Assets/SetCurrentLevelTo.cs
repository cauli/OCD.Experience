using UnityEngine;
using System.Collections;

public class SetCurrentLevelTo : MonoBehaviour {

	public GameManager gameManager;
	public int toWhatLevel = 2;

	// Use this for initialization
	void Start () {
		gameManager.SetCurrentChallengeNumber(toWhatLevel);
	}

	// Update is called once per frame
	void Update () {

	}
}

