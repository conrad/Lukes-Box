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

						MeshRenderer cubeMesh = cubes[x,y,z].GetComponent<MeshRenderer>();
						cubeMesh.material.color = map[x,y,z] == 1 ? Color.black : Color.white;
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
						MeshRenderer cubeMesh = cubes[x,y,z].GetComponent<MeshRenderer>();
						cubeMesh.material.color = map[x,y,z] == 1 ? Color.black : Color.white;
					}
				}
			}
		}
	}
}

//	public class Square {
//
//		public ControlNode topLeft, topRight, bottomRight, bottomLeft;
//		public Node centreTop, centreRight, centreBottom, centreLeft;
//
//		public Square (ControlNode _topLeft, ControlNode _topRight, ControlNode _bottomRight, ControlNode _bottomLeft) {
//			topLeft = _topLeft;
//			topRight = _topRight;
//			bottomRight = _bottomRight;
//			bottomLeft = _bottomLeft;
//
//			centreTop = topLeft.right;
//			centreRight = bottomRight.above;
//			centreBottom = bottomLeft.right;
//			centreLeft = bottomLeft.above;
//		}
//	}
//
//	public class Node {
//		public Vector3 position;
//		public int vertexIndex = -1;
//
//		public Node(Vector3 _pos) {
//			position = _pos;
//		}
//	}
//
//	public class ControlNode : Node {
//
//		public bool active;
//		public Node above, right;
//
//		public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) {
//			active = _active;
//			above = new Node(position + Vector3.forward * squareSize/2f);
//			right = new Node(position + Vector3.right * squareSize/2f);
//		}
//	}
//}
