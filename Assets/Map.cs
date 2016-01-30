using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public Transform line1;
	public Transform line2;
	public Transform line3;


	/*
	 *  Cada elemento do array corresponde a um ponto, assumindo TOP LEFT 
	 *  Ex: O primeiro índice [0][0] (P) no array dimensional pega os valores
	 *    
	 *        
	 *         0  
	 *    P----------
	 *    |
	 *    |
	 *  2 |
	 *    |
	 *    |
	 */

	public int[,] verticalArray  = new int[,]         { { 2, 2, 0 }, 
												        { 2, 2, 0 },
												        { 0, 0, 0 } };

	public int[,] horizontalArray  = new int[,]       { { 0, 0, 0 }, 
												        { 5, 0, 0 },
												        { 0, 0, 0 } };
	  

	// 0 = Null
	// 1 = Player normal start
	public int[,] posArray         = new int[,]       { { 0, 0, 0 }, 
													    { 0, 0, 0 },
													    { 0, 0, 0 } };

	public int lengthOfStick = 10;  // Stick length

	int l = 0;

	public int level = 1;

	GameObject[] lines;

	void Awake() {
		l = lengthOfStick;
		setLevel (level);	
	}

	void Start() {
		drawLinesForPuzzle ();

		if (lines == null)
			lines = GameObject.FindGameObjectsWithTag("LineDraw");
	}



	void Update() {
		foreach (GameObject line in lines) {
			//Debug.Log ("Drawing line");
			if (line.gameObject != null) {
				LineRenderer renderer = (LineRenderer)line.gameObject.GetComponent<LineRenderer> ();
				LineInfo info = (LineInfo)line.gameObject.GetComponent<LineInfo> ();

				//renderer.SetPosition(0, new Vector3 (row*l, col*l, 0));
				//renderer.SetPosition(1, new Vector3 (row*l, col*l + (1*l)));

				//renderer.SetPosition (0, new Vector3(100,100,100));
				renderer.SetPosition(0, info.startVector3);
				renderer.SetPosition(1, info.endVector3);
			}
		}
	}

	void setLevel(int level) {
		if (level == 1) {
			setLevelOne ();
		} else if (level == 2) {
			setLevelTwo ();
		} else if (level == 3) {
			setLevelThree (); 	
		} else if (level == 4) {
			setLevelFour ();
		} else if (level == 5) {
			setLevelFive ();
		} else if (level == 6) {
			setLevelSix ();
		} else {
			Debug.LogError ("Invalid Level!");
		}
	}

	Vector3 XYtoVector3(int col, int row) {
		return new Vector3 (row * l, col * l, 0);
	}

	public void drawLinesForPuzzle () {
		GameObject[] allLines = GameObject.FindGameObjectsWithTag("LineDraw");
		foreach(GameObject obj in allLines) {
			Destroy (obj);
		}

		for (int row = 0; row < verticalArray.GetLength (0); row++) {
			for (int col = 0; col < verticalArray.GetLength (1); col++) {
				if ((int)verticalArray [row,col] > 0) {
					Transform line = null;


					if ((int)verticalArray [row, col] == 1) {
						line = (Transform) Instantiate (line1, new Vector3 (row*l, col*l, 0), Quaternion.identity);
					} else if ((int)verticalArray [row, col] == 2) {
						line = (Transform) Instantiate (line2, new Vector3 (row*l, col*l, 0), Quaternion.identity);
					} else if ((int)verticalArray [row, col] == 3) {
						line = (Transform) Instantiate (line3, new Vector3 (row*l, col*l, 0), Quaternion.identity);
					} 

					if (line != null) {
						Debug.Log ("Line is not null");
						LineRenderer renderer = (LineRenderer)line.gameObject.GetComponent<LineRenderer> ();

						LineInfo info = (LineInfo)line.gameObject.GetComponent<LineInfo> ();

						info.startVector3 = new Vector3 (row * l, col * l, 0);
						info.endVector3 = new Vector3 (row*l+ (1*l), col*l,0);

						//renderer.SetPosition(0, new Vector3 (row*l, col*l, 0));
						//renderer.SetPosition(1, new Vector3 (row*l, col*l + (1*l)));
					}

				}
			}
		}

		for (int row = 0; row < horizontalArray.GetLength (0); row++) {
			for (int col = 0; col < horizontalArray.GetLength (1); col++) {
				if ((int)horizontalArray [row,col] > 0) {

					Transform line = null;

					if ((int)horizontalArray [row, col] == 1) {
						line = (Transform) Instantiate (line1, new Vector3 (row*l, col*l, 0), Quaternion.identity);
					} else if ((int)horizontalArray [row, col] == 2) {
						line = (Transform) Instantiate (line2, new Vector3 (row*l, col*l, 0), Quaternion.identity);
					} else if ((int)horizontalArray [row, col] == 3) {
						line = (Transform) Instantiate (line3, new Vector3 (row*l, col*l, 0), Quaternion.identity);
					}

					if (line != null) {
						Debug.Log ("Line is not null");
						LineRenderer renderer = (LineRenderer)line.gameObject.GetComponent<LineRenderer> ();

						LineInfo info = (LineInfo)line.gameObject.GetComponent<LineInfo> ();

						info.startVector3 = new Vector3 (row * l, col * l, 0);
						info.endVector3 = new Vector3 (row*l, col*l + (1*l),0);

						Debug.Log ("Setting " + (row * l) + " - " + ((col*l) + (1*l)));
					}
				}
			}
		}

		lines = GameObject.FindGameObjectsWithTag("LineDraw");
	}

	void setLevelOne() {
		verticalArray  = new int[,]       { { 2, 2, 0 }, 
											{ 2, 2, 0 },
											{ 0, 0, 0 } };

		horizontalArray  = new int[,]     { { 0, 0, 0 }, 
										    { 5, 0, 0 },
											{ 0, 0, 0 } };

		posArray         = new int[,]     { { 0, 0, 0 }, 
										    { 1, 0, 0 },
										    { 0, 0, 0 } };
		
	}

	void setLevelTwo() {
		// RED
		verticalArray  = new int[,]       { { 0, 1, 1, 0 }, 
									     	{ 1, 1, 1, 1 },
											{ 0, 1, 1, 0 },
											{ 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     { { 0, 1, 0, 0 }, 
											{ 1, 1, 1, 0 },
											{ 1, 1, 1, 0 },
											{ 0, 1, 0, 0 } };

		posArray         = new int[,]    {  { 0, 0, 0, 0 }, 
											{ 1, 0, 0, 0 },
											{ 0, 0, 0, 0 },
											{ 0, 0, 0, 0 } };

	}

	// DRAGON
	void setLevelThree() {
		// RED
		verticalArray  = new int[,]       { { 0, 0, 0, 0, 0, 0 }, 
											{ 0, 0, 0, 0, 1, 1 },
											{ 1, 0, 0, 1, 0, 0 },
											{ 0, 1, 1, 1, 1, 0 },
											{ 0, 0, 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     { { 0, 0, 0, 0, 0, 0 }, 
											{ 0, 0, 0, 0, 1, 0 },
											{ 1, 0, 0, 1, 0, 0 },
											{ 1, 0, 1, 1, 0, 0 },
											{ 0, 1, 0, 1, 0, 0 } };
		
		posArray         = new int[,]     { { 0, 0, 0, 0, 0, 0 }, 
										    { 0, 0, 0, 0, 0, 0 },
										    { 0, 1, 0, 0, 0, 0 },
										    { 0, 0, 0, 0, 0, 0 },
										    { 0, 0, 0, 0, 0, 0 } };

	}

	// DOUBLE GAME
	//  ____
	// |‗‗‗‗|
	// |____|
	void setLevelFour() {
		// RED
		verticalArray  = new int[,]       { { 1, 0, 0, 0, 1, 0 }, 
											{ 1, 0, 0, 0, 1, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     { { 1, 1, 1, 1, 0, 0 }, 
											{ 2, 2, 2, 2, 0, 0 },
											{ 1, 1, 1, 1, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 } };

		posArray         = new int[,]     { { 2, 0, 0, 0, 0, 0 }, 
											{ 1, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 } };

	}


	/**
	 *  DESCENT
	 *   _ _
	 *  |_║_|_
	 *    |_|_|_
	 *      |_|_|
	 * 
     */
	void setLevelFive() {
		// RED
		verticalArray  = new int[,]    	  { { 1, 2, 1, 0, 0, 0 }, 
											{ 0, 1, 1, 1, 0, 0 },
											{ 0, 0, 1, 1, 1, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     { { 1, 1, 0, 0, 0, 0 }, 
											{ 1, 1, 1, 0, 0, 0 },
											{ 0, 1, 1, 1, 0, 0 },
											{ 0, 0, 1, 1, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 } };

		posArray         = new int[,]     { { 0, 0, 0, 0, 0, 0 }, 
											{ 0, 1, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 },
											{ 0, 0, 0, 0, 0, 0 } };

	}

	/**
	 *    _ _
	 *   |‗║‗|
	 *   |_║_|
	 * 
	 * 
	*/
	void setLevelSix() {
		// RED
		verticalArray  = new int[,]       { { 1, 2, 1, 0 }, 
											{ 1, 2, 1, 0 },
											{ 0, 0, 0, 0 },
											{ 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     { { 1, 1, 0, 0 }, 
											{ 2, 2, 0, 0 },
											{ 1, 1, 0, 0 },
											{ 0, 0, 0, 0 } };

		posArray         = new int[,]    {  { 0, 0, 0, 0 }, 
											{ 0, 1, 0, 0 },
											{ 0, 0, 0, 0 },
											{ 0, 0, 0, 0 } };

	}

	/**
	 *      
	 *    ‗║_║‗
	 *    ‗|_|‗	
	 *     ║ ║
	 * 
	*/
	void setLevelSeven() {
		// RED
		verticalArray  = new int[,]       { { 0, 2, 2, 0 }, 
											{ 0, 1, 1, 0 },
											{ 0, 2, 2, 0 },
											{ 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     { { 0, 0, 0, 0 }, 
											{ 2, 1, 2, 0 },
											{ 2, 1, 2, 0 },
											{ 0, 0, 0, 0 } };

		posArray         = new int[,]    {  { 0, 0, 0, 0 }, 
											{ 0, 0, 0, 0 },
											{ 0, 1, 0, 0 },
											{ 0, 0, 0, 0 } };

	}


}
