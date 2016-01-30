using UnityEngine;
using System.Collections;

public class LineInfo : MonoBehaviour {

	public Vector3 startVector3;
	public Vector3 endVector3;

	public void setStartVector3(Vector3 v) {
		startVector3 = v;
	}

	public Vector3 getStartVector3() {
		return startVector3;
	}

	public void setEndVector3(Vector3 v) {
		endVector3 = v;
	}

	public Vector3 getEndVector3() {
		return endVector3;
	}

}
