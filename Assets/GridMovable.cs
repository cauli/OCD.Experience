using UnityEngine;
using System.Collections;

public class GridMovable : MonoBehaviour {

	public Grid map;

	public int l = -1;

	int currentX;
	int currentY;

	enum Way{ Vertical, Horizontal };
	enum Direction{ Up, Down, Left, Right };

	// Use this for initialization
	void Start () {

		// Setando o length para o mesmo do Grid PAI
		l = map.lengthOfStick;

		this.transform.position = GetStartPosition (map);
	}
			
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up"))
			MoveUp ();

		if (Input.GetKeyDown ("left"))
			MoveLeft ();

		if (Input.GetKeyDown ("right"))
			MoveRight ();

		if (Input.GetKeyDown ("down"))
			MoveDown ();
	}


	bool MoveIfPossible(int destinyX,  int destinyY, int currentX, int currentY, Way way, Direction direction) {

		int posX = -100;
		int posY = -100;

		/*
		 *  VER PAPEL 2 PARA EXPLICACAO
		 * 
		 * 
		 */
		if (direction == Direction.Up || direction == Direction.Left) {
			posX = destinyX;
			posY = destinyY;
		} else {
			posX = currentX;
			posY = currentY;
		}

		// To move
		if (map.isInsideBounds (destinyX, destinyY)) {
			Debug.Log ("Destiny Inside bounds X: " + destinyX + " --- " + " Y: " + destinyY + "");

			//if(map.doMove(posX, posY, (way == Way.Vertical) )) {

				map.PrintVertical ();
				map.PrintHorizontal ();

				this.transform.position = XYtoVector3 (destinyX, destinyY);

				currentX = destinyX;
				currentY = destinyY;
			/*}
			else {
				Debug.Log ("But cannot move X: " + destinyX + " --- " + " Y: " + destinyY + "");	
			  return false;
			}*/

			return true;
		}
		else {
			return false;
		}
	}

	void MoveUp() {
		Debug.Log ("Current X : " + currentX + " --- " + " Current Y : " + currentY);

		int destinyY = currentY - 1;
		int destinyX = currentX;

		if (MoveIfPossible (destinyX, destinyY, currentX, currentY, Way.Vertical, Direction.Up)) {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " DONE!!!!!!!!");
		} else {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " NOT POSSIBLE");
		}

	}
		
	void MoveDown() {
		Debug.Log ("Current X : " + currentX + " --- " + " Current Y : " + currentY);

		int destinyY = currentY + 1;
		int destinyX = currentX;

		if (MoveIfPossible (destinyX, destinyY, currentX, currentY, Way.Vertical, Direction.Down)) {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " DONE!!!!!!!!");
		} else {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " NOT POSSIBLE");
		}

	}

	void MoveLeft() {	
		Debug.Log ("Current X : " + currentX + " --- " + " Current Y : " + currentY);
		
		int destinyY = currentY;
		int destinyX = currentX-1;

		if (MoveIfPossible (destinyX, destinyY, currentX, currentY, Way.Horizontal, Direction.Left)) {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " DONE!!!!!!!!");
		} else {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " NOT POSSIBLE");
		}

	}

	void MoveRight() {
		Debug.Log ("Current X : " + currentX + " --- " + " Current Y : " + currentY);

		int destinyY = currentY;
		int destinyX = currentX+1;

		if (MoveIfPossible (destinyX, destinyY, currentX, currentY, Way.Horizontal, Direction.Up)) {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " DONE!!!!!!!!");
		} else {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " NOT POSSIBLE");
		}

	}


	/**
	 *  Returns the specified position of the player by the grid. 
	 */
	Vector3 GetStartPosition(Grid map) {
		for (int row = 0; row < map.posArray.GetLength (0); row++) {
			for (int col = 0; col < map.posArray.GetLength (1); col++) {
				if ((int)map.posArray [row,col] == 1) {
					currentY = row;
					currentX = col;

					return XYtoVector3 (currentX, currentY);
				}
			}

		}

		return Vector3.zero;
	}


	Vector3 XYtoVector3(int col, int row) {
		return new Vector3 (row * l, col * l, 0);
	}
}
