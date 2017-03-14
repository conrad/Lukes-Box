using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Edge3DMapGenerator : MonoBehaviour 
{
	public int width;
	public int height;
	public int depth;

	int[,,] map;
	LifeEngine lifeEngine;


	public int[,,] GenerateInitialGameOfLifeMap()
	{
		GenerateEmptyMap();

		// Add filled cells according to an initial Conway's Game of Life example.
		int z = depth-1;
		int[] wallCenter = new int[2];
		wallCenter[0] = (int) Mathf.Floor(width / 2);
		wallCenter[1] = (int) Mathf.Floor(height / 2);

		// Left Box
		map[1, wallCenter[1], z] = 1;
		map[1, wallCenter[1]+1, z] = 1;
		map[2, wallCenter[1], z] = 1;
		map[2, wallCenter[1]+1, z] = 1;

		// Left Moving Unit
		map[7, wallCenter[1], z] = 1;
		map[7, wallCenter[1]+1, z] = 1;
		map[7, wallCenter[1]+2, z] = 1;
		map[8, wallCenter[1], z] = 1;
		map[8, wallCenter[1]+1, z] = 1;
		map[8, wallCenter[1]+2, z] = 1;
		map[9, wallCenter[1]-1, z] = 1;
		map[9, wallCenter[1]+3, z] = 1;
		map[11, wallCenter[1]-2, z] = 1;
		map[11, wallCenter[1]-1, z] = 1;
		map[11, wallCenter[1]+3, z] = 1;
		map[11, wallCenter[1]+4, z] = 1;

		// Middle Whirlpool
		map[16, wallCenter[1]+2, z] = 1;
		map[16, wallCenter[1]+3, z] = 1;
		map[17, wallCenter[1]-1, z] = 1;
		map[17, wallCenter[1], z] = 1;
		map[18, wallCenter[1]-1, z] = 1;
		map[18, wallCenter[1], z] = 1;
		map[19, wallCenter[1]-2, z] = 1;
		map[19, wallCenter[1]-1, z] = 1;
		map[20, wallCenter[1]-1, z] = 1;
		map[20, wallCenter[1]+1, z] = 1;
		map[21, wallCenter[1], z] = 1;
		map[21, wallCenter[1]+1, z] = 1;

		// Right Moving Unit
		map[24, wallCenter[1], z] = 1;
		map[24, wallCenter[1]+1, z] = 1;
		map[24, wallCenter[1]+5, z] = 1;
		map[24, wallCenter[1]+6, z] = 1;
		map[25, wallCenter[1], z] = 1;
		map[25, wallCenter[1]+1, z] = 1;
		map[25, wallCenter[1]+5, z] = 1;
		map[25, wallCenter[1]+6, z] = 1;
		map[27, wallCenter[1]+2, z] = 1;
		map[27, wallCenter[1]+3, z] = 1;
		map[27, wallCenter[1]+4, z] = 1;
		map[28, wallCenter[1]+2, z] = 1;
		map[28, wallCenter[1]+3, z] = 1;
		map[28, wallCenter[1]+4, z] = 1;
		map[29, wallCenter[1]+3, z] = 1;

		// Right Box
		map[35, wallCenter[1]+2, z] = 1;
		map[35, wallCenter[1]+3, z] = 1;
		map[36, wallCenter[1]+2, z] = 1;
		map[36, wallCenter[1]+3, z] = 1;

		return map;
	}

	void GenerateEmptyMap()
	{
		if (map == null) {
			map = new int[width, height, depth];
		}

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				for (int z = 0; z < depth; z++) {
					map [x, y, z] = 0;		// Tuple.Create(1,0);
				}
			}
		}
	}


	public int[,,] UpdateMap() 
	{
		if (lifeEngine == null) {
			if (map == null) {
				GenerateInitialGameOfLifeMap();
			}

			lifeEngine = new LifeEngine(map);
		}

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				for (int z = 0; z < depth; z++) {
					map[x, y, z] = lifeEngine.CalcCellInSuperCube(x, y, z);
				}
			}
		}

		return map;
	}

}




//	void GenerateMap() {
//		map = new int[width, height, depth];
//		RandomFillMap();
//
//		CubeBoxGenerator cubeBoxGen = GetComponent<CubeBoxGenerator>();
//		cubeBoxGen.GenerateCubeBox(map);
//	}


//	private IEnumerator UpdateMap() {
//		while (true) {
//			for (int x = 0; x < width; x++) {
//				for (int y = 0; y < height; y++) {
//					for (int z = 0; z < depth; z++) {
//						map[x, y, z] = lifeEngine.calc(x, y, z);
//					}
//				}
//			}
//			yield return new WaitForSeconds(updateTime);
//			CubeBoxGenerator cubeBoxGen = GetComponent<CubeBoxGenerator>();
//			cubeBoxGen.GenerateCubeBox(map);
//		}
//	}





/*
 * Tuple<int, int>[] tuples =
 * {
 * 	 Tuple.Create(50, 350),
 *   Tuple.Create(50, 650),
 * };
 */ 
