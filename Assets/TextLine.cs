using UnityEngine;
using System.Collections;

public class TextLine : MonoBehaviour {

	public LineInfo parentLineInfo;
	//public float zRot = 90f;

	// Use this for initialization
	void Start () {
		parentLineInfo = this.transform.parent.transform.parent.gameObject.GetComponent<LineInfo>();

		if(parentLineInfo != null)
		{
			Debug.Log(parentLineInfo.getStartVector3());

			this.transform.position = Vector3.Lerp(parentLineInfo.getStartVector3(), parentLineInfo.getEndVector3(),0.5f);

			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2f);

			//Vector3 angle = parentLineInfo.getEndVector3() - parentLineInfo.getStartVector3();
			//this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, 90, this.transform.localEulerAngles.z );
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(parentLineInfo != null) {
			/*Vector3 axis = new Vector3(0f,0f,zRot);
		
			transform.parent.transform.rotation = Quaternion.LookRotation(axis);*/

		}
	}
}
