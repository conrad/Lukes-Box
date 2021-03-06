﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge3DMeshGenerator : MonoBehaviour 
{
	public CubeGrid cubeGrid;
	public GameObject cube;
	public int cellSize;
	public int anchorX, anchorY, anchorZ;


	public void Generate(int[,,] map) {
		if (cubeGrid == null) {
			Vector3 anchor = new Vector3(anchorX, anchorY, anchorZ);
			cubeGrid = new CubeGrid (this.gameObject, map, cube, cellSize, anchor);
		} else {
			cubeGrid.Render(map);
		}
	}


	public void Render(int[,,] map)
	{
		cubeGrid.Render(map);
	}


	public class CubeGrid {

		public GameObject[,,] cubes;
		public GameObject parent;

		public CubeGrid(
			GameObject _parent,
			int[,,] map, 
			GameObject cube, 
			float cellSize, 
			Vector3 anchor
		) {
			parent = _parent;

			int nodeCountX  = map.GetLength(0);
			int nodeCountY  = map.GetLength(1);
			int nodeCountZ  = map.GetLength(2);
			float mapWidth  = nodeCountX * cellSize;
			float mapHeight = nodeCountY * cellSize;
			float mapDepth  = nodeCountZ * cellSize;

			cubes = new GameObject[nodeCountX,nodeCountY, nodeCountZ];

			for (int x = 0; x < nodeCountX; x ++) {
				for (int y = 0; y < nodeCountY; y ++) {
					for (int z = 0; z < nodeCountZ; z ++) {
						if (IsOnMapEdge(x, y, z, map)) {
							Vector3 pos = new Vector3(
								anchor.x + -mapWidth/2 + x * cellSize + cellSize/2, 
								anchor.y + -mapHeight/2 + y * cellSize + cellSize/2, 
								anchor.z + -mapDepth/2 + z * cellSize + cellSize/2
							);

							cubes[x,y,z] = Instantiate(cube, pos, Quaternion.identity) as GameObject;
							cubes[x,y,z].transform.parent = parent.transform;
							InitialRender(x, y, z, map[x,y,z]);
						}
					}
				}
			}
		}


		public void Render(int[,,] map) {
			int nodeCountX = map.GetLength(0);
			int nodeCountY = map.GetLength(1);
			int nodeCountZ = map.GetLength(2);

			for (int x = 0; x < nodeCountX; x++) {
				for (int y = 0; y < nodeCountY; y++) {
					for (int z = 0; z < nodeCountZ; z++) {
						if (IsOnMapEdge(x, y, z, map)) {
							UpdateCube(x,y,z, map[x,y,z]);
						}						
					}
				}
			}
		}


		private void InitialRender(int x, int y, int z, int status)
		{
			cubes[x, y, z].GetComponent<MeshRenderer>().enabled = status == 1 ? true : false;
		}


		private void UpdateCube(int x, int y, int z, int status) 
		{
			CubeController cubeController = cubes[x, y, z].GetComponent<CubeController>();

			if (status == 1) {
				cubes[x, y, z].GetComponent<MeshRenderer>().enabled = true;
				cubeController.Activate();
			} else if (status == 0) {
				cubeController.Deactivate();
			}


			//			MeshRenderer cubeMesh = cubes[x,y,z].GetComponent<MeshRenderer>();
			//			cubeMesh.material.color = status == 1 ? Color.black : Color.white;


			//			if (status == 1) {
			//				cubes[x, y, z].gameObject.transform.localScale = Vector3.Lerp(
			//					cubes[x, y, z].gameObject.transform.localScale, 
			//					new Vector3(.5f, .5f, .5f),
			//					.5f
			//				);
			//			} else {
			//				cubes[x, y, z].gameObject.transform.localScale = Vector3.Lerp(
			//					cubes[x, y, z].gameObject.transform.localScale, 
			//					new Vector3(0f, 0f, 0f),
			//					.5f
			//				);
			//			}
		}


		bool IsOnMapEdge(int x, int y, int z, int[,,] map)
		{
			return x == 0 || x == map.GetLength(0) - 1 || y == 0 || y == map.GetLength(1) - 1 || z == 0 || z == map.GetLength(2) - 1;
		}
	}

										
}
