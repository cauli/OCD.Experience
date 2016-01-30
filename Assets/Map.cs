using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

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

	void Awake() {
		setLevel (level);	
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
		} else {
			Debug.LogError ("Invalid Level!");
		}


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

	// 
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

}
