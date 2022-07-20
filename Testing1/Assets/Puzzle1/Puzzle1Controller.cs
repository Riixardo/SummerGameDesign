using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Controller : MonoBehaviour
{
    public float Proximity = 10f;
    public int mouseIndex = 0;
    public bool OnAutomaticMode = true;
    public GameObject tilesHolder;
    int matrixSize, matrixLength, automaticPlayerCount;
    GameObject mainCamera;
    Puzzle1Identifier[] puzzle1;
    Puzzle1Identifier[,] puzzle12D;
    void Awake() {
        matrixLength = (int)Mathf.Sqrt(tilesHolder.transform.childCount);
        puzzle12D = new Puzzle1Identifier[matrixLength, matrixLength];
        for(int i = 0; i < matrixLength; i++) {
            for(int j = 0; j < matrixLength; j++) {
                puzzle12D[i, j] = tilesHolder.transform.GetChild(i * matrixLength + j).GetComponent<Puzzle1Identifier>();
            }
        }
        matrixSize = puzzle12D.GetLength(0) * puzzle12D.GetLength(1);
        Debug.Log(matrixSize);
    }
    void Start()
    {
        for(int i = 0; i < puzzle12D.GetLength(0); i++) {
            for(int j = 0; j < puzzle12D.GetLength(1); j++) {
                Debug.Log(puzzle12D[i, j].Identifier);
            }
        }
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
    void Update()
    {
        if(OnAutomaticMode) {
            var i = Random.Range(1, matrixSize + 1);
            //Debug.Log(i);
            LightUpMatrix(i);
        }
        else CheckButtonPress();
    }
    void CheckButtonPress() {
        if(Input.GetMouseButtonDown(mouseIndex)) {			
			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				if(hit.collider.GetComponent<Puzzle1Identifier>() && hit.collider.transform.parent.Equals(tilesHolder.transform)) { 
                    int ID = hit.collider.GetComponent<Puzzle1Identifier>().Identifier;
					if((hit.collider.transform.position - this.transform.position).magnitude < Proximity) {
						LightUpMatrix(ID);
					}
				}
			}
		}
    }
    void LightUpMatrix(int ID) {
        int colIndex = (ID - 1) % matrixLength;
        int rowIndex = (ID - 1) / matrixLength;
        if(puzzle12D[rowIndex, colIndex].Light.enabled) return;
        puzzle12D[rowIndex, colIndex].ChangeLight();
        if(colIndex + 1 > -1 && colIndex + 1 < matrixLength) {
            puzzle12D[rowIndex, colIndex + 1].ChangeLight();
        }
        if(colIndex - 1 > -1 && colIndex < matrixLength) {
            puzzle12D[rowIndex, colIndex - 1].ChangeLight();
        }
        if(rowIndex + 1> -1 && rowIndex + 1< matrixLength) {
            puzzle12D[rowIndex + 1, colIndex].ChangeLight();
        }
        if(rowIndex - 1 > -1 && rowIndex - 1 < matrixLength) {
            puzzle12D[rowIndex - 1, colIndex].ChangeLight();
        }
        automaticPlayerCount++;
        for(int i = 0; i < puzzle12D.GetLength(0); i++) {
            for(int j = 0; j < puzzle12D.GetLength(1); j++) {
                if(puzzle12D[i, j].Light.enabled == false) return;
            }
        }
        Debug.Log("The Matrix of side length " + matrixSize + " took " + automaticPlayerCount + " trials to complete.");
        OnAutomaticMode = false;
    }
}
