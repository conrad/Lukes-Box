using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour 
{
	MeshRenderer mesh;
	Animator anim;	

	int expandStateHash = Animator.StringToHash("Expand");	// More efficient than comparing state name strings.
	int shrinkStateHash = Animator.StringToHash("Shrink");
	int idleStateHash   = Animator.StringToHash("Idle");



	void Awake() 
	{
		anim = GetComponent<Animator>();		
		mesh = GetComponent<MeshRenderer>();
		mesh.material.color = new Color( Random.value, Random.value, Random.value, 1.0f );
	}


		
	public void Activate() 
	{
		anim.SetTrigger("ExpandIt");
	}



	public void Deactivate()
	{
		anim.SetTrigger("ShrinkIt");
	}

}
