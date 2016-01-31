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
	public GameObject endObj;
	public CanvasGroup endScreen;
	public BGImagesBehavior bgBehavior;

	public GameObject gotItBtn;
	public Text attemptTxt;
	public Text attemptEndTxt;

	public Text info;

	float totalCurrentTime = -1;
	float startTotalTime = -1;

	int[] puzzlesChallenge1 = new int[6] {2,11,5,12,8,10};
	public Transform[] ballonsChallenge1;
	int[] puzzlesChallenge2 = new int[6] {14,17,15,16,13,18};
	public Transform[] ballonsChallenge2;

	public static int currentChallenge = 0;
	private int currentPuzzleIndex = 0;

	//private int numberAttempts = 0;
	private static int numberAttempts = 1;
	private bool timerRunning = true;

	public Transform p1;
	public Transform p1reverse;
	public Transform p2;

	// Use this for initialization
	void Start () {
		if(info != null)
		{
			info.text = getTextInfo();
		}

		if(currentChallenge == 0)
		{
			Debug.Log("Current challenge is zero");

			GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject player in allPlayer) {
				Debug.Log("Found Player... will set initial position");

				GridMovable gridMovable = player.GetComponent<GridMovable>();
				gridMovable.SetInitialPosition();
			}
		}
			



	}

	string getTextInfo() {
		return "puzzle " + (currentPuzzleIndex+1) +"<color=#26c6da>/</color>" + puzzlesChallenge1.Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void SetCurrentChallengeNumber(int challengeNumber) {
		Debug.LogWarning("I am setting current challenge number to " + challengeNumber);
		currentChallenge = challengeNumber;
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

	public static void CameraPositionOnly(Vector3 position) {
		Camera.main.transform.position = position;
		Camera.main.orthographicSize = 15;
	}

	public static void CameraPosition(Vector3 position, int size = 15) {
		Camera.main.transform.position = position;
		Camera.main.orthographicSize = size;
	}


	// show retry screen
	IEnumerator FadeIn(CanvasGroup canvasGroup)
	{
		if(retryObj == null) {
			Debug.LogWarning("Retry object is null. If you are on the tutorial then it is ok");
			return false;
		}

		if (!retryObj.activeSelf && canvasGroup == retryScreen) {
			retryObj.SetActive (true);
		} else if (!endObj.activeSelf && canvasGroup == endScreen) {
			endObj.SetActive (true);
		}

		float time = 1f;
		while(canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime / time;
			yield return null;
		}
	}

		
	public void StartPuzzle(float totalTime) {
		Debug.LogWarning("Starting puzzle");
		StartCoroutine(FadeOut(canvasGroup));

		bgBehavior.resetAllImages();
		if ( retryObj.activeSelf ) {
			StartCoroutine(FadeOut(retryScreen));
		}		

		bgBehavior.MoveImage();

		startTotalTime = totalTime;
		totalCurrentTime = totalTime;

		timerRunning = true;
		StartCoroutine(Timer());

		// Coração batendo
		System.Collections.Hashtable hash =
			new System.Collections.Hashtable();
		hash.Add("x", 0.81f);
		hash.Add("y", 0.78f);
		hash.Add("time", 1f);
		hash.Add("looptype", iTween.LoopType.loop);
		iTween.ScaleTo(heart, hash);


		if(currentChallenge == 0) {
		
		}	else if(currentChallenge == 1) {
			SetLevel(puzzlesChallenge1[0]);
		} else if(currentChallenge == 2) {
			SetLevel(puzzlesChallenge2[0]);
		} 


		info.text = getTextInfo();
	}

	private void StopAllPlayers() {
		GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in allPlayer) {
			iTween.Stop(player);
		}
	}


	private void ActivateAllPlayers() {
		if(p1reverse != null)
		{
			p1reverse.gameObject.SetActive(true);
		}

		if(p1 != null)
		{
			p1.gameObject.SetActive(true);
		}

		if(p2 != null)
		{
			p2.gameObject.SetActive(true);
		}
	}

	private void SetLevel(int level) {

		Debug.LogWarning("I am trying to set level " + level);

		grid.map.setLevel(level);

	
		GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in allPlayer) {
			Debug.Log("Found Player... will set initial position");

			GridMovable gridMovable = player.GetComponent<GridMovable>();
			gridMovable.SetInitialPosition();
		}


		if(currentChallenge == 1) {
			ShowBalloon(currentPuzzleIndex, ballonsChallenge1);
		} else if(currentChallenge == 2) {
			ShowBalloon(currentPuzzleIndex, ballonsChallenge2);
		}
	}

	Transform[] currentBalloonArray;

	int lastBalloonIndex = 0;

	private void OnColorUpdated(Color color)
	{
		currentBalloonArray[lastBalloonIndex].GetComponent<Image>().color = color;
	}

	private void OnColorUpdated2(Color color)
	{
		currentBalloonArray[lastBalloonIndex].GetComponent<Image>().color = color;
	}
		
	public void MoveGuiElement(Vector2 position){
		currentBalloonArray[lastBalloonIndex].GetComponent<RectTransform>().anchoredPosition = position;
	}

	private void ShowBalloon(int index, Transform[] balloons) {
		currentBalloonArray = balloons;
		Debug.Log(balloons.Length + "!" + index);

		// Setar color de todos baloes anteriores pra transparente pra garantir que nao vai dar overlap
		for(int i=0; i<balloons.Length; i++)
		{
			balloons[i].GetComponent<Image>().color = Color.clear;
		}

		if(index < balloons.Length) {
			if(balloons[index] != null) {
				lastBalloonIndex = index;

				Transform t = balloons[index];
				GameObject g = t.gameObject;

				Vector2 initialPos = balloons[lastBalloonIndex].GetComponent<RectTransform>().anchoredPosition;
				Vector2 preInitialPos = initialPos - new Vector2(0f,20f);
				t.transform.position = preInitialPos;

				iTween.ValueTo(balloons[lastBalloonIndex].gameObject, iTween.Hash(
					"from", preInitialPos,
					"to", initialPos,
					"time", 1.0f,
					"delay", 0.5f,
					"easeType", iTween.EaseType.easeInOutCubic,
					"onupdatetarget", this.gameObject, 
					"onupdate", "MoveGuiElement"));


				Hashtable tweenParams = new Hashtable();
				tweenParams.Add("from", balloons[lastBalloonIndex].GetComponent<Image>().color);
				tweenParams.Add("to", Color.white);
				tweenParams.Add("time", 1.0f);
				tweenParams.Add("delay", 0.5f);
				tweenParams.Add("onupdate", "OnColorUpdated");
				tweenParams.Add("onupdatetarget", this.gameObject);

				iTween.ValueTo(balloons[lastBalloonIndex].gameObject, tweenParams);

				StartCoroutine(FadeOut(balloons[lastBalloonIndex].gameObject));
			}
		}
	}

	IEnumerator FadeOut(GameObject go) {
		yield return new WaitForSeconds(4f);

		Hashtable tweenParams2 = new Hashtable();
		tweenParams2.Add("from", Color.white);
		tweenParams2.Add("to", new Color(1,1,1,0));
		tweenParams2.Add("time", 1.0f);
		tweenParams2.Add("onupdate", "OnColorUpdated2");
		tweenParams2.Add("onupdatetarget", this.gameObject);

		iTween.ValueTo(go, tweenParams2);
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


			//print (totalCurrentTime);

			float heartRate = startTotalTime - totalCurrentTime;

			//heart.localScale = new Vector3(Mathf.Sin(Time.time * (5 + heartRate/7) ) * 0.1f + 1.0f, Mathf.Sin(Time.time * (5 + heartRate/7)) * 0.1f + 0.95f,1);

			heartFill.fillAmount = qtty;

			if(totalCurrentTime <= 0.0f) {
				TimesUp();
			}
		}
	}


	public void WonPuzzle () {

		totalCurrentTime += 30;

		if(totalCurrentTime > startTotalTime) {
			totalCurrentTime = startTotalTime;
		}

		Debug.Log ("Won PUZZLE");

		StopAllPlayers();

		currentPuzzleIndex++;


		// TODO everything here is hardcoded. But this is the game jam way.
		if(currentChallenge == 0)
		{
			Debug.Log("Show OK I GOT IT button");
			if(gotItBtn != null)
			{
				gotItBtn.SetActive(true);
			}
		}
		else if(currentChallenge == 1)
		{
			if(currentPuzzleIndex > puzzlesChallenge1.Length-1)
			{
				currentPuzzleIndex = 0;
				currentChallenge = 2;

				if(attemptTxt != null) {
					attemptEndTxt.text = "it took you <color=00fff6>" + numberAttempts.ToString() + "</color> attempt(s)";
				}
					
				StartCoroutine (FadeIn (endScreen));

				Debug.Log("FINISHED FULL CHALLENGE 1");
			}
			else
			{
				SetLevel(puzzlesChallenge1[currentPuzzleIndex]);



				// TODO HACK Xunxo só pra mudar em telas impares
				if(currentPuzzleIndex % 2 == 0)
				{
					bgBehavior.FadeInNextImage();
				}
			}
		}
		else if(currentChallenge == 2) {
			if(currentPuzzleIndex > puzzlesChallenge2.Length-1)
			{
				currentPuzzleIndex = 0;
				currentChallenge = 1;

				if(attemptTxt != null) {
					attemptEndTxt.text = "it took you <color=00fff6>" + numberAttempts.ToString() + "</color> attempt(s)";
				}

				StartCoroutine (FadeIn (endScreen));

				Debug.Log("FINISHED FULL GAME");
			}
			else
			{
				SetLevel(puzzlesChallenge2[currentPuzzleIndex]);

				// TODO HACK Xunxo só pra mudar em telas impares
				if(currentPuzzleIndex % 2 == 0)
				{
					bgBehavior.FadeInNextImage();
				}
			}
		}
		else
		{
			//Debug.LogWarning("Challenge not set!");

			numberAttempts++;

			if(attemptTxt != null) {
				attemptEndTxt.text = "it took you <color=00fff6>" + numberAttempts.ToString() + "</color> attempt(s)";
			}

			StartCoroutine (FadeIn (endScreen));
		}



		if(gotItBtn != null) {
			gotItBtn.SetActive (true);
		}

		if(info != null)
		{
			info.text = getTextInfo();
		}
	
	}


	public void LostPuzzle () {
		// increasing the number of attempts in the UI
		numberAttempts++;
		attemptTxt.text = numberAttempts.ToString();

		currentPuzzleIndex = 0;
		//currentChallenge = 1;

		StartCoroutine(FadeIn(retryScreen));

		timerRunning = false;

		Debug.LogError ("!!!!! LOST, CANT MOVE");

	}

	public void EndGame () {
		numberAttempts++;
		attemptTxt.text = "with <color=#00fff6>" + numberAttempts.ToString () + "</color> attempt(s)";
		Debug.Log ("End Game");
	}

	public void TimesUp () {
		// increasing the number of attempts in the UI
		numberAttempts++;
		attemptTxt.text = numberAttempts.ToString();

		currentPuzzleIndex = 0;
		//currentChallenge = 1;

		StartCoroutine(FadeIn(retryScreen));

		timerRunning = false;

		Debug.LogError ("!!!!! LOST TIMES UP");
	}
}