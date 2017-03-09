using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class to coordinate changes between the game and scripts related to the SuperCube.
 */ 
public class SuperCubeController : MonoBehaviour 
{
	Edge3DMapGenerator mapGenerator;
	Edge3DMeshGenerator meshGenerator;

	void Start () {
		mapGenerator = GetComponent<Edge3DMapGenerator>();
		int[,,] map = mapGenerator.GenerateInitialGameOfLifeMap();

		meshGenerator = GetComponent<Edge3DMeshGenerator>();
		meshGenerator.GenerateCubeBox(map);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
