using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public Transform line1;
	public Transform line2;
	public Transform line3;
	public Transform line4;
	public Transform line5;
	public Transform line6;


	public Transform lineCount;



	public GameManager gameManager;

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

	public int l = 6;  // Stick length

	public int level = 1;
	public float playerScale = 2;

	public float totalTime = 20.0f;

	public Vector2 cameraOffset = new Vector2(10f, 10f);

	GameObject[] lines;

	void Awake() {
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
			if(line != null)
			{		
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

	public void setLevel(int level) {
		if (level == 0) {
			setLevelZero ();
		} else if (level == 1) {
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
		} else if (level == 7) {
			setLevelSeven ();
		} else if (level == 8) {
			setLevelEight ();
		} else if (level == 9) {
			setLevelNine ();
		} else if (level == 10) {
			setLevelTen ();
		}else {
			Debug.LogError ("Invalid Level!");
		}

		drawLinesForPuzzle();

		//gameManager.StartPuzzle(totalTime);
	}

	Vector3 XYtoVector3(int col, int row) {
		return new Vector3 (row * l, col * l, 0);
	}

	public void drawLinesForPuzzle () {
		GameObject[] allLines = GameObject.FindGameObjectsWithTag("LineDraw");
		foreach(GameObject obj in allLines) {
			Destroy (obj);
		}

		GameObject[] allCount = GameObject.FindGameObjectsWithTag("LineCount");
		foreach(GameObject obj in allCount) {
			Destroy (obj);
		}


		for (int row = 0; row < verticalArray.GetLength (0); row++) {
			for (int col = 0; col < verticalArray.GetLength (1); col++) {
				if ((int)verticalArray [row,col] > 0) {
					Transform line = null;

					int v = (int)verticalArray [row, col];

					if (v == 1) {
						line = (Transform)Instantiate (line1, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 2) {
						line = (Transform)Instantiate (line2, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 3) {
						line = (Transform)Instantiate (line3, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 4) {
						line = (Transform)Instantiate (line4, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 5) {
						line = (Transform)Instantiate (line5, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 6) {
						line = (Transform)Instantiate (line6, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else {
						if (v != 0) {
							Debug.LogError ("Erro occurred when instantiating line horizontal of VALUE " + v);
						}
					}


					if (line != null) {
						LineRenderer renderer = (LineRenderer)line.gameObject.GetComponent<LineRenderer> ();

						LineInfo info = (LineInfo)line.gameObject.GetComponent<LineInfo> ();

						info.startVector3 = new Vector3 (row * l, col * l, 0);
						info.endVector3 = new Vector3 (row*l+ (1*l), col*l,0);

						//Quaternion a = Quaternion.Euler(270f, 0f, 0f) * Quaternion.Euler(0f, 0f, 0f);
						Transform count = (Transform)Instantiate (lineCount, new Vector3 (row * l + (0.5f*l), col * l, 0.0f), Quaternion.identity);

						count.GetComponent<TextMesh>().text = v.ToString();

						count.transform.Rotate(new Vector3(0f, 270f, 90f));

					}
				}
			}
		}

		for (int row = 0; row < horizontalArray.GetLength (0); row++) {
			for (int col = 0; col < horizontalArray.GetLength (1); col++) {
				if ((int)horizontalArray [row,col] > 0) {

					Transform line = null;

					int v = (int)horizontalArray [row, col];

					if (v == 1) {
						line = (Transform)Instantiate (line1, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 2) {
						line = (Transform)Instantiate (line2, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 3) {
						line = (Transform)Instantiate (line3, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 4) {
						line = (Transform)Instantiate (line4, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 5) {
						line = (Transform)Instantiate (line5, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else if (v == 6) {
						line = (Transform)Instantiate (line6, new Vector3 (row * l, col * l, 0), Quaternion.identity);
					} else {
						if (v != 0) {
							Debug.LogError ("Erro occurred when instantiating line horizontal of VALUE " + v);
						}
					}


					if (line != null) {
						LineRenderer renderer = (LineRenderer)line.gameObject.GetComponent<LineRenderer> ();

						LineInfo info = (LineInfo)line.gameObject.GetComponent<LineInfo> ();

						info.startVector3 = new Vector3 (row * l, col * l, 0);
						info.endVector3 = new Vector3 (row*l, col*l + (1*l),0);


						Transform count = (Transform)Instantiate (lineCount, new Vector3 (row * l , col * l + (0.5f*l), 0.0f), Quaternion.identity);

						count.GetComponent<TextMesh>().text = v.ToString();

						count.transform.Rotate(new Vector3(0f, 270f, 90f));

					}
				}
			}
		}

		lines = GameObject.FindGameObjectsWithTag("LineDraw");
	}




	/**
	 *  LEVELS START HERE 
	 * 
	 * 
	 * 
	 * 
	 */

	/**
	*	  _
	*   _║_|
	*
	*	Tutorial introdutório
	*
	*/
	void setLevelZero() {
		verticalArray  = new int[,]       
		  { { 0, 2, 1 }, 
			{ 0, 0, 0 },
			{ 0, 0, 0 } };

		horizontalArray  = new int[,]    
		  { { 0, 1, 0 }, 
			{ 1, 1, 0 },
			{ 0, 0, 0 } };

		posArray         = new int[,]    
		  { { 0, 0, 0 }, 
			{ 1, 0, 0 },
			{ 0, 0, 0 } };

		l = 10;

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

		l = 10;
		
	}

	/**
	 *        _
	 * 		_|_|_
	 *     |_| |_|
	 * 		 
	 * 
	 * 
	 */
	void setLevelTwo() {
		// RED
		verticalArray  = new int[,]       { { 0, 1, 1, 0 }, 
									     	{ 1, 1, 1, 1 },
											{ 0, 0, 0, 0 },
											{ 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     { { 0, 1, 0, 0 }, 
											{ 1, 1, 1, 0 },
											{ 1, 0, 1, 0 },
											{ 0, 0, 0, 0 } };

		posArray         = new int[,]    {  { 0, 0, 0, 0 }, 
											{ 1, 0, 0, 0 },
											{ 0, 0, 0, 0 },
											{ 0, 0, 0, 0 } };

		l = 6;

		GameManager.CameraPosition(new Vector3(80.4f, 79.9f, -84.9f));
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

		l = 3;

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

		l = 5;
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
											{ 0, 0, 1, 2, 1, 0 },
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




	/**
	 *    ‗   ‗
	 *   ║‗║‗║‗║
	 *   ║‗║‗║‗║
	 *     ║‗║
	 * 
	 */
	void setLevelEight() {
		// RED
		verticalArray = new int[,] {
			{ 3, 3, 3, 3, 0, 0 }, 
			{ 2, 2, 2, 2, 0, 0 },
			{ 0, 3, 3, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 }
		};
				

		// BLUE
		horizontalArray = new int[,] {
			{ 3, 0, 3, 0, 0, 0 }, 
			{ 3, 2, 3, 0, 0, 0 },
			{ 2, 3, 2, 0, 0, 0 },
			{ 0, 3, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 }
		};

		posArray = new int[,] {
			{ 0, 0, 0, 0, 0, 0 }, 
			{ 1, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 }
		};

	}

	/**
	 * 
	 *   ‗‗‗‗‗‗
	 *   ‗‗‗‗‗‗ 
	 * 
	 */
	void setLevelNine() {
		// RED
		verticalArray = new int[,] {
			{ 0, 0, 2, 0, 0, 0 }, 
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 }
		};


		// BLUE
		horizontalArray = new int[,] {
			{ 2, 2, 2, 2, 0, 0 }, 
			{ 2, 2, 2, 2, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 }
		};

		posArray = new int[,] {
			{ 1, 0, 0, 0, 0, 0 }, 
			{ 0, 0, 0, 0,-1, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0 }
		};

		l = 10;
	}

	/**
	 * 	 1	_
	 *   |_|_|_
	 *   |_|_|_|
	 * 	   |_| |
	 *         -1
	 * 
	 */
	void setLevelTen() {
		// RED
		verticalArray  = new int[,]       
		  { { 1, 1, 1, 0 }, 
			{ 1, 1, 1, 1 },
			{ 0, 1, 1, 1 },
			{ 0, 0, 0, 0 } };

		// BLUE
		horizontalArray  = new int[,]     
		  { { 0, 1, 0, 0 }, 
			{ 1, 1, 1, 0 },
			{ 1, 1, 1, 0 },
			{ 0, 1, 0, 0 } };

		posArray         = new int[,]   
		 {  { 1, 0, 0, 0 }, 
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0,-1 } };
	}

}
