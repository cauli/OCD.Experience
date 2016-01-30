using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public Map map;
	public int l;

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

	/**
	 *   DEBUGGING 
	 */
	public void PrintVertical() {
		Debug.LogWarning("Vertical");

		string line = "";

		for (int x = 0; x < map.verticalArray.GetLength(0); x += 1) {

			for (int y = 0; y < map.verticalArray.GetLength(1); y += 1) {
				line += map.verticalArray[x, y];
			}

			line += "\n";
		}

		Debug.LogWarning(line);
	}

	public void PrintHorizontal() {
		Debug.LogWarning("Horizontal");

		string line = "";

		for (int x = 0; x < map.horizontalArray.GetLength(0); x += 1) {


			for (int y = 0; y < map.horizontalArray.GetLength(1); y += 1) {
				line += map.horizontalArray[x, y];
			}

			line += "\n";
		}

		Debug.LogWarning(line);
	}
	/**
	 *   END DEBUGGING 
	 */


 	// Use this for initialization
	void Awake () {
		map = this.GetComponent<Map> ();
		l = map.lengthOfStick;	
	}
	
	// Update is called once per frame
	void Update () {

		for (int row = 0; row < map.verticalArray.GetLength (0); row++) {
			for (int col = 0; col < map.verticalArray.GetLength (1); col++) {
				if ((int)map.verticalArray [row,col] > 0) {
					Debug.DrawLine (new Vector3 (row*l, col*l, 0), new Vector3 (row*l+ (1*l), col*l , 0), Color.red);
				}
			}
		}

		for (int row = 0; row < map.horizontalArray.GetLength (0); row++) {
			for (int col = 0; col < map.horizontalArray.GetLength (1); col++) {
				if ((int)map.horizontalArray [row,col] > 0) {
					Debug.DrawLine (new Vector3 (row*l, col*l, 0), new Vector3 (row*l, col*l + (1*l), 0), Color.blue);
				}
			}
		}

	}

	public bool canMove(int col, int row) {

		Debug.LogWarning("CAN MOVE " + map.verticalArray [row, col] );
		// Check can move Up
		if ( (col-1) > 0 )
		{
			if (map.verticalArray [row, col - 1] > 0) {
				return true;
			}
		}
		// Check can move Down
		if ( (col) > 0 )
		{
			if (map.verticalArray [row, col] > 0) {
				return true;
			}
		}
		if ( (row-1) > 0 )
		{
			if (map.verticalArray [row-1, col] > 0) {
				return true;
			}
		}
			
		if ( (row) > 0 )
		{
			if (map.verticalArray [row, col] > 0) {
				return true;
			}
		}

		// Check can move Left
		if ( (row-1) > 0 )
		{
			if (map.horizontalArray [row-1, col] > 0) {
				return true;
			}
		}


		// Check can move Left
		if ( (row) > 0 )
		{
			if (map.horizontalArray [row, col] > 0) {
				return true;
			}
		}

		return false;
	}

	public bool isInsideBounds(int col, int row) {
		if (col < 0 || row < 0) {
			return false;
		}

		if (row < map.verticalArray.GetLength(0) && col < map.verticalArray.GetLength(1))
		{
			return true;
		}
		else  
		{
			return false;
		}
	}

	public bool CheckWon() {
		for (int row = 0; row < map.verticalArray.GetLength (0); row++) {
			for (int col = 0; col < map.verticalArray.GetLength (1); col++) {
				if ((int)map.verticalArray [row,col] > 0) {
					return false;
				}
			}
		}

		for (int row = 0; row < map.horizontalArray.GetLength (0); row++) {
			for (int col = 0; col < map.horizontalArray.GetLength (1); col++) {
				if ((int)map.horizontalArray [row,col] > 0) {
					return false;
				}
			}
		}

		return true;
	}

	public bool doMove(int col, int row, bool vertical) {
		if (vertical) {
			Debug.Log ("map.verticalArray[" + row + "][" + col + "] == " + (int)map.verticalArray [row, col]);

			// Se este elemento for 0, não existem jogadas disponíveis para o usuário
			if ((int)map.verticalArray [row, col] <= 0) {
				
				return false;
			} else {
				map.verticalArray [row, col] = ((int)map.verticalArray [row, col]) - 1;
				return true;
			}

		} else {

			Debug.Log ("map.horizontalArray[" + row + "][" + col + "] == " + (int)map.horizontalArray [row, col]);

			// Se este elemento for 0, não existem jogadas disponíveis para o usuário
			if ((int)map.horizontalArray [row, col] <= 0) {
				return false;
			} else {
				map.horizontalArray [row, col] = ((int)map.horizontalArray [row, col]) - 1;
				return true;
			}
		}
	}
}
