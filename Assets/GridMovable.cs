using UnityEngine;
using System.Collections;

public class GridMovable : MonoBehaviour {

	public GameManager manager;

	public Grid grid;

	private int l = -1;

	public int id = -1;

	int currentX;
	int currentY;

	bool active = false;

	enum Way{ Vertical, Horizontal };
	enum Direction{ Up, Down, Left, Right };

	// Use this for initialization
	void Start () {

		// Setando o length para o mesmo do Grid PAI
		l = grid.l;

		gameObject.transform.position = GetStartPosition (grid);
	}
			
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up")) {
			MoveUp ();
		}

		if (Input.GetKeyDown ("left")) {
			MoveLeft ();
		}

		if (Input.GetKeyDown ("right")) {
			MoveRight ();
		}

		if (Input.GetKeyDown ("down")) {
			MoveDown ();
		}
	}

	void CompletedMove () {
		//grid.map.drawLinesForPuzzle ();
	}

	bool MoveIfPossible(int destinyX,  int destinyY, int cX, int cY, Way way, Direction direction) {

		int posX = -100;
		int posY = -100;

		Debug.Log ("--------- " + direction);
		/*
		 *  VER PAPEL 2 PARA EXPLICACAO
		 * 
		 * 
		 */
		if (direction == Direction.Up || direction == Direction.Left) {	
			Debug.Log ("UP or LEFT!");	
			posX = destinyX;
			posY = destinyY;
		} else {
			Debug.Log ("RIGHT or DOWN!");	
			posX = cX;
			posY = cY;
		}

		// To move
		if (grid.isInsideBounds (destinyX, destinyY)) {	
			Debug.Log ("But current sending  X: " + cX + " --- " + " Y: " + cY + "");	
			Debug.Log ("Destiny Inside bounds X: " + destinyX + " --- " + " Y: " + destinyY + "");
			Debug.Log ("But position sending  X: " + posX + " --- " + " Y: " + posY + "");

			if(grid.doMove(posX, posY, (way == Way.Vertical) )) {

				grid.PrintVertical ();
				grid.PrintHorizontal ();

				// this.transform.position = XYtoVector3 (destinyX, destinyY);

				Vector3 destinyVector3 = XYtoVector3 (destinyX, destinyY);

				iTween.MoveTo(gameObject,iTween.Hash("x",destinyVector3.x,"y",destinyVector3.y,"time",0.5,"oncomplete","CompletedMove","looptype",iTween.LoopType.none));				

				grid.map.drawLinesForPuzzle ();

				currentX = destinyX;
				currentY = destinyY;

				if (grid.CheckWon ()) {
					manager.WonPuzzle ();
				} else {
					// Nessa posição nova, se o usuário não puder se mexer, perdeu o jogo.
					if (!grid.canMove (currentX, currentY)) {
						manager.LostPuzzle ();
					}
				}
			}
			else {
				Debug.Log ("But cannot move X: " + destinyX + " --- " + " Y: " + destinyY + "");	
			  return false;
			}

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

		if (MoveIfPossible (destinyX, destinyY, currentX, currentY, Way.Horizontal, Direction.Right)) {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " DONE!!!!!!!!");
		} else {
			Debug.Log ("Moving to DestinyX : " + destinyX + " --- " + " DestinyY : " + destinyY + " NOT POSSIBLE");
		}

	}


	/**
	 *  Returns the specified position of the player by the grid. 
	 */
	Vector3 GetStartPosition(Grid grid) {

		Debug.Log ("ID " + id);

		for (int row = 0; row < grid.map.posArray.GetLength (0); row++) {
			for (int col = 0; col < grid.map.posArray.GetLength (1); col++) {
				if ((int)grid.map.posArray[row, col] == id) {
					currentY = row;
					currentX = col;

					active = true;

					gameObject.SetActive (true);

					//Debug.Log ("VALXY " + (int)grid.map.posArray [row, col] + "CX : " + row + " CY :" + col);

					return XYtoVector3 (col, row);
				} else {
					// Debug.Log ("NOT " + id + " - on  " + row + " " + col  + " got "  + (int)grid.map.posArray[row, col]);
				}
			}

		}

		active = false;
		gameObject.SetActive(false);
		return Vector3.zero;
	}


	Vector3 XYtoVector3(int col, int row) {
		return new Vector3 (row * l, col * l, 0);
	}
}
