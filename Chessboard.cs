using UnityEngine;
using System.Collections;

public class Chessboard : MonoBehaviour {

	public int m_iSize = 10;
	GameObject[,] m_Grid;



	// Use this for initialization
	void Start () {
		m_Grid = new GameObject[m_iSize, m_iSize];

		for (int i = 0; i < m_iSize; i++) {
			for (int j = 0; j < m_iSize; j++) {
				GameObject kachel = GameObject.CreatePrimitive (PrimitiveType.Quad);

				float r = Random.value;
				if (r < 0.5f) {
					kachel.GetComponent<Renderer> ().material.color = Color.black;
				}
				m_Grid [i, j] = kachel;
				kachel.transform.position = new Vector3 (i + .5f, j + .5f, 0);
				kachel.transform.parent = this.transform;
				kachel.name = "Kachel (" + i + "," + j + ")";
			}
		}
		Camera.main.transform.position = new Vector3 (m_iSize / 2, m_iSize / 2, -10);
		Camera.main.orthographicSize = m_iSize;
	}



	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			ToggleMouseField();
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			KillAll ();
		}

		// Leertaste drücken, um jeden Schritt einzeln zu betrachten
		if (Input.GetKeyDown(KeyCode.Space) == false)
			return;
		// 2D Array, welches die Anzahl der Nachbarn speichert
		int [,] NumberNeighbours = new int[m_iSize, m_iSize];
		for (int iCol = 0; iCol < m_iSize; iCol++){
			for (int iRow = 0; iRow < m_iSize; iRow++){
				NumberNeighbours[iCol, iRow]= GetAliveNeighbours(iCol, iRow);
			}
		}


		for (int iCol = 0; iCol < m_iSize; iCol++){
			for (int iRow = 0; iRow < m_iSize; iRow++) {
				int AliveNeighbours = NumberNeighbours[iCol, iRow];
				// tile dead
			if (m_Grid [iCol, iRow].GetComponent<Renderer>().material.color == Color.white) {
					if (AliveNeighbours == 3) {
						m_Grid [iCol, iRow].GetComponent<Renderer>().material.color = Color.black;
					}
				}
				// tile alive
				else {
					if (AliveNeighbours < 2 || AliveNeighbours > 3){
						m_Grid [iCol, iRow].GetComponent<Renderer>().material.color = Color.white;
					}
				}

			}
		}
		print ("Anzahl Nachbarn" + GetAliveNeighbours (1, 1) + " !");

	}	





 	int GetAliveNeighbours(int _iColumn, int _iRow){
		// Anzahl der lebenden Nachbarn deklarieren
	
		int iAliveNeighbours = 0;

		for (int iCol = _iColumn-1; iCol <= _iColumn+1; iCol++){
			for (int iRow = _iRow-1; iRow <= _iRow+1; iRow++){
				if ((iCol == _iColumn && iRow == _iRow) == false && iCol >= 0 && iCol < m_iSize && iRow >= 0 && iRow < m_iSize && m_Grid[iCol, iRow].GetComponent<Renderer>().material.color == Color.black){ //check bounds
					iAliveNeighbours++;
				}
			}
		}


		return iAliveNeighbours;


	}
		/*
		// Spalte, Zeile: links, mitte
		int iLeftCol = _iColumn - 1;
		if (iLeftCol >= 0 && m_Grid[iLeftCol, _iRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		// Spalte, Zeile: rechts, mitte
		int iRightCol = _iColumn + 1;
		if (iRightCol < m_iSize && m_Grid[iRightCol, _iRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		// Spalte, Zeile: mitte, oben
		int iUpRow = _iRow + 1;
		if (iUpRow < m_iSize && m_Grid[_iColumn, iUpRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		// Spalte, Zeile: mitte, unten
		int iDownRow = _iRow - 1;
		if (iDownRow >= 0 && m_Grid[_iColumn, iDownRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		// Spalte, Zeile: links, oben
		if (iLeftCol >= 0 && iUpRow < m_iSize && m_Grid[iLeftCol, iUpRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		// Spalte, Zeile: links, unten
		if (iLeftCol >= 0 && iDownRow >= 0 && m_Grid[iLeftCol, iDownRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		// Spalte, Zeile: rechts, oben
		if (iRightCol < m_iSize && iUpRow < m_iSize && m_Grid[iRightCol, iUpRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		// Spalte, Zeile: rechts, unten
		if (iRightCol < m_iSize &&iDownRow >= 0 && m_Grid[iRightCol, iDownRow].GetComponent<Renderer>().material.color == Color.black){
			iAliveNeighbours++;}
		
		return iAliveNeighbours;
	}
	*/

	void KillAll(){
			for (int iCol = 0; iCol < m_iSize; iCol++) {
				for (int iRow = 0; iRow < m_iSize; iRow++) {
					if (m_Grid [iCol, iRow].GetComponent<Renderer> ().material.color == Color.black) {
						m_Grid [iCol, iRow].GetComponent<Renderer> ().material.color = Color.white;
					}
				}
			}
		}

	void Toggle(int _iCol, int _iRow){
		
		if (m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color == Color.black) {
			m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color = Color.white;
		} 
		else {
			m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color = Color.black;
		}
		
	}



	bool GetAlive(int _iCol, int _iRow){
		return (m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color == Color.black);
		        }



	void SetAlive(int _iCol, int _iRow, bool _bAlive){
		if (_bAlive)
			m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color = Color.black;
		else
			m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color = Color.white;
	}



	void ToggleMouseField(){
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		int xIndex = (int)mouseWorldPos.x;
		int yIndex = (int)mouseWorldPos.y;
		Toggle(xIndex, yIndex);
	}
	



}


		























