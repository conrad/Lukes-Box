﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEngine {

	int[,,] map;
	int width;
	int height;
	int depth;


	public LifeEngine(int[,,] _map) {
		map = _map;
		width = map.GetLength(0);
		height = map.GetLength(1);
		depth = map.GetLength(2);
	}


	public int calc(int x, int y, int z) {
		if (map [x, y, z] == 1) {
			return calcOne(x, y, z);
		} 

		return calcZero(x, y, z);
	}


	int calcZero(int x, int y, int z) {
		int neighbors = GetNeighborCount(x, y, z);

		if (neighbors >= 5) {
			return 1;
		}

		return 0;
	}


	int calcOne(int x, int y, int z) {
		int neighbors = GetNeighborCount(x, y, z);

		if (neighbors < 5) {
			return 0;
		}

		if (neighbors > 5) {
			return 0;
		}

		return 1;
	}


	int GetNeighborCount(int gridX, int gridY, int gridZ) {
		int neighborCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
				for (int neighbourZ = gridZ - 1; neighbourZ <= gridZ + 1; neighbourZ++) {
					if (neighbourX >= 0 && neighbourX < width 
						&& neighbourY >= 0 && neighbourY < height
						&& neighbourZ >= 0 && neighbourZ < depth
					) {
						if (neighbourX != gridX || neighbourY != gridY || neighbourZ != gridZ) {
							neighborCount += map[neighbourX, neighbourY, neighbourZ];
						}
					}
				}
			}
		}
	
		return neighborCount;
	}

}


/*
 * Rules for Conway's Game of Life:
 * For a space that is 'populated':
 * 		Each cell with one or no neighbors dies, as if by solitude.
 * 		Each cell with four or more neighbors dies, as if by overpopulation.
 * 		Each cell with two or three neighbors survives.
 * For a space that is 'empty' or 'unpopulated'
 * 		Each cell with three neighbors becomes populated.
 */ 
