using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

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
	// 1 = Player start
	// 2 = Player end
	public int[,] posArray         = new int[,]       { { 0, 0, 0 }, 
													    { 1, 2, 0 },
													    { 0, 0, 0 } };

	public int lengthOfStick = 10;  // Stick length
	int l = 0;


	/**
	 *   DEBUGGING 
	 */
	public void PrintVertical() {
		Debug.LogWarning("Vertical");

		string line = "";

		for (int x = 0; x < verticalArray.GetLength(0); x += 1) {

			for (int y = 0; y < verticalArray.GetLength(1); y += 1) {
				line += verticalArray[x, y];
			}

			line += "\n";
		}

		Debug.LogWarning(line);
	}

	public void PrintHorizontal() {
		Debug.LogWarning("Horizontal");

		string line = "";

		for (int x = 0; x < horizontalArray.GetLength(0); x += 1) {


			for (int y = 0; y < horizontalArray.GetLength(1); y += 1) {
				line += horizontalArray[x, y];
			}

			line += "\n";
		}

		Debug.LogWarning(line);
	}
	/**
	 *   END DEBUGGING 
	 */


 	// Use this for initialization
	void Start () {
		l = lengthOfStick;	
	}
	
	// Update is called once per frame
	void Update () {

		for (int row = 0; row < verticalArray.GetLength (0); row++) {
			for (int col = 0; col < verticalArray.GetLength (1); col++) {
				if ((int)verticalArray [row,col] > 0) {
					Debug.DrawLine (new Vector3 (row*l, col*l, 0), new Vector3 (row*l+ (1*l), col*l , 0), Color.red);
				}
			}
		}

		for (int row = 0; row < horizontalArray.GetLength (0); row++) {
			for (int col = 0; col < horizontalArray.GetLength (1); col++) {
				if ((int)horizontalArray [row,col] > 0) {
					Debug.DrawLine (new Vector3 (row*l, col*l, 0), new Vector3 (row*l, col*l + (1*l), 0), Color.blue);
				}
			}
		}

	}

	public bool isInsideBounds(int col, int row) {
		if (col < 0 || row < 0) {
			return false;
		}

		if (row < verticalArray.GetLength(0) && col < verticalArray.GetLength(1))
		{
			return true;
		}
		else  
		{
			return false;
		}
	}

	public bool doMove(int col, int row, bool vertical) {
		if (vertical) {
			// Se este elemento for 0, não existem jogadas disponíveis para o usuário
			if ((int)verticalArray [row, col] <= 0) {
				return false;
			} else {
				verticalArray [row, col] = ((int)verticalArray [row, col]) - 1;
				return true;
			}

		} else {
			// Se este elemento for 0, não existem jogadas disponíveis para o usuário
			if ((int)horizontalArray [row, col] <= 0) {
				return false;
			} else {
				horizontalArray [row, col] = ((int)horizontalArray [row, col]) - 1;
				return true;
			}
		}
	}
}
