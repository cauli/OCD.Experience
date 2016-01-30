using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BGImagesBehavior : MonoBehaviour {

	public int animateYto = 270;
	public Vector2 targetPosition;
	RectTransform uGuiElement;

	// Use this for initialization
	void Start () {

	}

	public void MoveImage() {
		uGuiElement = this.GetComponent<RectTransform>();

		targetPosition = new Vector2(uGuiElement.anchoredPosition.x, animateYto);

		iTween.ValueTo(gameObject, iTween.Hash(
			"from", uGuiElement.anchoredPosition,
			"to", targetPosition,
			"time", 2.0f,
			"easeType", iTween.EaseType.easeInOutCubic,
			"onupdatetarget", this.gameObject, 
			"onupdate", "MoveGuiElement"));
	}

	public void MoveGuiElement(Vector2 position){
		uGuiElement.anchoredPosition = position;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
