using UnityEngine;
using System.Collections;
using System;

public class D3MapGenerator : MonoBehaviour {

	public int width;
	public int height;
	public int depth;
	public float updateTime;

	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;		// Eventaully, make this change dynamically over time

	int[,,] map;
	LifeEngine lifeEngine;

	void Start() {
		GenerateMap();
		lifeEngine = new LifeEngine(map);
		StartCoroutine(UpdateMap());
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			GenerateMap();
		}

	}

	void GenerateMap() {
		map = new int[width, height, depth];
		RandomFillMap();

		CubeBoxGenerator cubeBoxGen = GetComponent<CubeBoxGenerator>();
		cubeBoxGen.GenerateCubeBox(map, 2);
	}

	private IEnumerator UpdateMap() {
		while (true) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					for (int z = 0; z < depth; z++) {
						map[x, y, z] = lifeEngine.calc(x, y, z);
					}
				}
			}
			yield return new WaitForSeconds(updateTime);
			CubeBoxGenerator cubeBoxGen = GetComponent<CubeBoxGenerator>();
			cubeBoxGen.GenerateCubeBox(map, 2);
		}
	}


	void RandomFillMap() {
		if (useRandomSeed) {
			seed = Time.time.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				for (int z = 0; z < depth; z++) {
					map [x, y, z] = (pseudoRandom.Next (0, 100) < randomFillPercent) ? 1 : 0;
				}
			}
		}
	}

//	void OnDrawGizmos() {
//		if (map != null) {
//			for (int x = 0; x < width; x ++) {
//				for (int y = 0; y < height; y++) {
//					for (int z = 0; z < depth; z++) {
//						Gizmos.color = (map [x, y, z] == 1) ? Color.black : Color.white;
//						Vector3 pos = new Vector3 (-width / 2 + x + .5f, -height / 2 + y + .5f, -depth / 2 + z + .5f);
//						Gizmos.DrawCube (pos, Vector3.one);
//					}
//				}
//			}
//		}
//	}
}











//	void SmoothMap() {
//		for (int x = 0; x < width; x ++) {
//			for (int y = 0; y < height; y ++) {
//				for (int z = 0; z < depth; z++) {
//					int neighbourWallTiles = GetSurroundingWallCount (x, y, z);
//
//					if (neighbourWallTiles > 6)
//						map [x, y, z] = 1;
//					else if (neighbourWallTiles < 6)
//						map [x, y, z] = 0;
//				}
//			}
//		}
//	}


//	int GetSurroundingWallCount(int gridX, int gridY, int gridZ) {
//		int wallCount = 0;
//		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
//			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
//				for (int neighbourZ = gridZ - 1; neighbourZ <= gridZ + 1; neighbourZ++) {
//					if (neighbourX >= 0 && neighbourX < width 
//						&& neighbourY >= 0 && neighbourY < height
//						&& neighbourZ >= 0 && neighbourZ < depth
//					) {
//						if (neighbourX != gridX || neighbourY != gridY || neighbourZ != gridZ) {
//							wallCount += map [neighbourX, neighbourY, neighbourZ];
//						}
//					} else {
//						wallCount++;
//					}
//				}
//			}
//		}
//
//		return wallCount;
//	}