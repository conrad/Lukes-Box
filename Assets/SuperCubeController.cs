using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class to coordinate changes between the game and scripts related to the SuperCube.
 */ 
public class SuperCubeController : MonoBehaviour 
{
	public float updateTime;

	bool running;
	Coroutine runAutomationCoroutine;

	Edge3DMapGenerator mapGenerator;
	Edge3DMeshGenerator meshGenerator;


	void Start () 
	{
		mapGenerator = GetComponent<Edge3DMapGenerator>();
		int[,,] map = mapGenerator.GenerateInitialGameOfLifeMap();

		meshGenerator = GetComponent<Edge3DMeshGenerator>();
		meshGenerator.Generate(map);

		running = true;
		runAutomationCoroutine = StartCoroutine(RunAutomaton());
	}


	void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			if (running) {
				StopCoroutine(runAutomationCoroutine);
			} else {
				runAutomationCoroutine = StartCoroutine(RunAutomaton());
			}
			running = !running;
		}
	}


	private IEnumerator RunAutomaton()
	{
		while (true) {
			int[,,] map = mapGenerator.UpdateMap();
			meshGenerator.Render(map);
			yield return new WaitForSeconds(updateTime);
		}
	}
	

}
