using UnityEngine;
using System.Collections;

public class CubeBoxGenerator : MonoBehaviour {

	public CubeGrid cubeGrid;
	public GameObject cube;

	public void GenerateCubeBox(int[,,] map, float cubeSize) {
		if (cubeGrid == null) {
			cubeGrid = new CubeGrid (map, cube, cubeSize);
		} else {
			cubeGrid.UpdateBox(map);
		}
	}
		
	public class CubeGrid {

		public GameObject[,,] cubes;

		public CubeGrid(int[,,] map, GameObject cube, float cubeSize) {
			int nodeCountX = map.GetLength(0);
			int nodeCountY = map.GetLength(1);
			int nodeCountZ = map.GetLength(2);
			float mapWidth = nodeCountX * cubeSize;
			float mapHeight = nodeCountY * cubeSize;
			float mapDepth = nodeCountZ * cubeSize;

			cubes = new GameObject[nodeCountX,nodeCountY, nodeCountZ];

			for (int x = 0; x < nodeCountX; x ++) {
				for (int y = 0; y < nodeCountY; y ++) {
					for (int z = 0; z < nodeCountZ; z ++) {
						Vector3 pos = new Vector3(
							-mapWidth/2 + x * cubeSize + cubeSize/2, 
							-mapHeight/2 + y * cubeSize + cubeSize/2, 
							-mapDepth/2 + z * cubeSize + cubeSize/2
						);

						cubes[x,y,z] = Instantiate(cube, pos, Quaternion.identity) as GameObject;
						SetCubeColor(x, y, z, map[x,y,z]);
					}
				}
			}
		}

		public void UpdateBox(int[,,] map) {
			int nodeCountX = map.GetLength(0);
			int nodeCountY = map.GetLength(1);
			int nodeCountZ = map.GetLength(2);

			for (int x = 0; x < nodeCountX; x++) {
				for (int y = 0; y < nodeCountY; y++) {
					for (int z = 0; z < nodeCountZ; z++) {
						SetCubeColor(x,y,z, map[x,y,z]);
					}
				}
			}
		}

		private void SetCubeColor(int x, int y, int z, int status) {
//			MeshRenderer cubeMesh = cubes[x,y,z].GetComponent<MeshRenderer>();
//			cubeMesh.material.color = status == 1 ? Color.black : Color.white;

			cubes[x, y, z].GetComponent<MeshRenderer>().enabled = status == 1 ? true : false;
		}
	}
}
