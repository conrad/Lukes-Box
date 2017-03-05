using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour 
{
	float alphaLevel; 
	MeshRenderer meshRenderer;

	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		alphaLevel = meshRenderer.material.color.a;
	}

	void Update()
	{
		if (alphaLevel > 0f) {
			alphaLevel -= 0.1f;
			Debug.Log(alphaLevel);
			meshRenderer.material.color = new Color(1f, 1f, 1f, alphaLevel);

			if (alphaLevel < 0.1f) {
				alphaLevel = 0f;
				meshRenderer.material.color = new Color(1f, 1f, 1f, alphaLevel);
			}
		}
	}
}




//	[SerializeField] private float fadePerSecond = 2.5f;
//
//	private void Update() 
//	{
//		Material material = GetComponent<MeshRenderer>().material;
//		Color color = material.color;
//
//		material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
//
//		Debug.Log("update");	
//	}
//}


//	void LerpAlpha()
//	{
//		float lerp = Mathf.PingPong (Time.time, duration) / duration;
//
//		alpha = Mathf.Lerp(0.0, 1.0, lerp) ;
//		renderer.material.color.a = alpha;
//	}
//}
