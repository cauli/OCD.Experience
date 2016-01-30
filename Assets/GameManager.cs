using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject gotItBtn;
	public Text attemptTxt;

	//private int numberAttempts = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WonPuzzle () {
		
		gotItBtn.SetActive (true);

		Debug.Log ("!!!!! WON");	
	}


	public void LostPuzzle () {
		// increasing the number of attempts in the UI
		//numberAttempts++;
		//attemptTxt.text = numberAttempts.ToString();

		Debug.LogError ("!!!!! LOST, CANT MOVE");
	}
}