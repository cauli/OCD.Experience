using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Image heartFill;
	public GameObject heart;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float totalCurrentTime = -1;
	float startTotalTime = -1;


	public void StartPuzzle(float totalTime) {
		startTotalTime = totalTime;
		totalCurrentTime = totalTime;

		StartCoroutine(Timer());

		System.Collections.Hashtable hash =
			new System.Collections.Hashtable();
		
		hash.Add("x", 0.81f);
		hash.Add("y", 0.78f);
		hash.Add("time", 1f);
		hash.Add("looptype", iTween.LoopType.loop);
		iTween.ScaleTo(heart, hash);

	}

	IEnumerator Timer() {
		while (true) {
			yield return new WaitForSeconds(0.1f);
			totalCurrentTime -= 0.1f;


			// startTotalTime = 1
			// totalCurrentTime = qtty


			float qtty = 0;

			if(startTotalTime != 0.0f) {
				qtty = totalCurrentTime/startTotalTime;
			}




			float heartRate = startTotalTime - totalCurrentTime;

			//heart.localScale = new Vector3(Mathf.Sin(Time.time * (5 + heartRate/7) ) * 0.1f + 1.0f, Mathf.Sin(Time.time * (5 + heartRate/7)) * 0.1f + 0.95f,1);

			heartFill.fillAmount = qtty;

			if(totalCurrentTime <= 0.0f) {
				TimesUp();
			}
		}
	}


	public void WonPuzzle () {
		Debug.Log ("!!!!! WON");	
	}


	public void LostPuzzle () {
		Debug.LogError ("!!!!! LOST, CANT MOVE");
	}

	public void TimesUp () {
		Debug.LogError ("!!!!! LOST TIMES UP");
	}
}