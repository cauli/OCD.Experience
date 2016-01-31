using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BGImagesBehavior : MonoBehaviour {

	public int animateYto = 270;
	public Vector2 targetPosition;
	RectTransform uGuiElement;

	public Image[] imageStates;

	private int currentImageState = 0;
	private int maxImageStates = 0;
	int lastImageState = 0;

	// Use this for initialization
	void Start () {
		maxImageStates = imageStates.Length;
	}

	public void MoveImage() {

		if ( Application.loadedLevelName == "Scene" ) {
			
			uGuiElement = imageStates[currentImageState].GetComponent<RectTransform>();

			targetPosition = new Vector2(uGuiElement.anchoredPosition.x, animateYto);

			iTween.ValueTo(gameObject, iTween.Hash(
				"from", uGuiElement.anchoredPosition,
				"to", targetPosition,
				"time", 2.0f,
				"easeType", iTween.EaseType.easeInOutCubic,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MoveGuiElement"));
			
		} else {
			print ("outra cena");
		}

	}

	private void OnColorUpdated(Color color)
	{
		imageStates[lastImageState].color = color;
	}

	private void OnColorUpdatedTwo(Color color)
	{
		imageStates[currentImageState].color = color;
	}


	public void FadeInNextImage() {
		Hashtable tweenParams = new Hashtable();
		tweenParams.Add("from", imageStates[currentImageState].color);
		tweenParams.Add("to", Color.clear);
		tweenParams.Add("time", 0.8f);
		tweenParams.Add("onupdate", "OnColorUpdated");
		tweenParams.Add("onupdatetarget", this.gameObject);

		lastImageState = currentImageState;

		iTween.ValueTo(imageStates[lastImageState].gameObject, tweenParams);

		if(currentImageState+1 < maxImageStates)
		{
			currentImageState++;

			if(currentImageState < imageStates.Length) {
				Hashtable tweenParams2 = new Hashtable();
				tweenParams2.Add("from", Color.clear);
				tweenParams2.Add("to", Color.white);
				tweenParams2.Add("time", 0.8f);
				tweenParams2.Add("delay", 0.1f);
				tweenParams2.Add("onupdate", "OnColorUpdatedTwo");
				tweenParams2.Add("onupdatetarget", this.gameObject);

				iTween.ValueTo(imageStates[currentImageState].gameObject, tweenParams2);
			}

		}
		else
		{
			Debug.LogWarning("Not possible to fade images!!");
		}
	}

	public void MoveGuiElement(Vector2 position){
		uGuiElement.anchoredPosition = position;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
