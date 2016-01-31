using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Image heartFill;
	public GameObject heart;
	public Grid grid;

	public CanvasGroup canvasGroup;
	public GameObject retryObj;
	public CanvasGroup retryScreen;
	public BGImagesBehavior bgBehavior;

	public GameObject gotItBtn;
	public Text attemptTxt;

	float totalCurrentTime = -1;
	float startTotalTime = -1;

	int[] puzzlesChallenge1 = new int[3] {2,5,6};


	int currentChallenge = 1;
	private int currentPuzzleIndex = 0;

	//private int numberAttempts = 0;
	private int numberAttempts = 0;
	private bool timerRunning = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	IEnumerator FadeOut(CanvasGroup canvasGroup)
	{
		float time = 1f;
		while(canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime / time;
			yield return null;
		}

		if ( retryObj.activeSelf ) {
			retryObj.SetActive (false);
		}
	}

	public static void CameraPosition(Vector3 position) {
		Camera.main.transform.position = position;
	}


	// show retry screen
	IEnumerator FadeIn(CanvasGroup canvasGroup)
	{
		if ( !retryObj.activeSelf ) {
			retryObj.SetActive (true);
		}

		float time = 1f;
		while(canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime / time;
			yield return null;
		}
	}

		
	public void StartPuzzle(float totalTime) {
		StartCoroutine(FadeOut(canvasGroup));

		if ( retryObj.activeSelf ) {
			StartCoroutine(FadeOut(retryScreen));
		}		

		bgBehavior.MoveImage();

		startTotalTime = totalTime;
		totalCurrentTime = totalTime;

		timerRunning = true;
		StartCoroutine(Timer());

		System.Collections.Hashtable hash =
			new System.Collections.Hashtable();
		hash.Add("x", 0.81f);
		hash.Add("y", 0.78f);
		hash.Add("time", 1f);
		hash.Add("looptype", iTween.LoopType.loop);
		iTween.ScaleTo(heart, hash);


		SetLevel(2);

	}

	private void SetLevel(int level) {
		grid.map.setLevel(level);

		GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in allPlayer) {
			GridMovable gridMovable = player.GetComponent<GridMovable>();
			gridMovable.SetInitialPosition();
		}
	}

	IEnumerator Timer() {
		while (timerRunning) {
			yield return new WaitForSeconds(0.1f);
			totalCurrentTime -= 0.1f;


			// startTotalTime = 1
			// totalCurrentTime = qtty


			float qtty = 0;

			if(startTotalTime != 0.0f) {
				qtty = totalCurrentTime/startTotalTime;
			}


			print (totalCurrentTime);

			float heartRate = startTotalTime - totalCurrentTime;

			//heart.localScale = new Vector3(Mathf.Sin(Time.time * (5 + heartRate/7) ) * 0.1f + 1.0f, Mathf.Sin(Time.time * (5 + heartRate/7)) * 0.1f + 0.95f,1);

			heartFill.fillAmount = qtty;

			if(totalCurrentTime <= 0.0f) {
				TimesUp();
			}
		}
	}


	public void WonPuzzle () {

		Debug.Log ("Won PUZZLE");

		currentPuzzleIndex++;


		if(currentChallenge == 1)
		{
			if(currentPuzzleIndex > puzzlesChallenge1.Length-1)
			{
				Debug.Log("FINISHED FULL CHALLENGE");
			}
			else
			{
				SetLevel(puzzlesChallenge1[currentPuzzleIndex]);
				bgBehavior.FadeInNextImage();
			}
		}
		else
		{
			Debug.LogError("Challenge not set!");
		}



		if(gotItBtn != null) {
			gotItBtn.SetActive (true);
		}
	
	}


	public void LostPuzzle () {
		// increasing the number of attempts in the UI
		numberAttempts++;
		attemptTxt.text = numberAttempts.ToString();

		StartCoroutine(FadeIn(retryScreen));

		timerRunning = false;

		Debug.LogError ("!!!!! LOST, CANT MOVE");
	}

	public void TimesUp () {
		timerRunning = false;
		Debug.LogError ("!!!!! LOST TIMES UP");
	}
}