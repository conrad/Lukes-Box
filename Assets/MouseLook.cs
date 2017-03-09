
using UnityEngine;
using System.Collections;


/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

//[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour 
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion originalRotation;

	bool isRotating = true;


	void Start ()
	{
		originalRotation = transform.localRotation;
	}


	void Update ()
	{
		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1)) {
			isRotating = !isRotating;
		}
			
		if (isRotating) {
			if (axes == RotationAxes.MouseXAndY)
			{
				// Read the mouse input axis
				rotationX += Input.GetAxis("Mouse X") * sensitivityX;
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationX = ClampAngle (rotationX, minimumX, maximumX);
				rotationY = ClampAngle (rotationY, minimumY, maximumY);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
				Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
				transform.localRotation = originalRotation * xQuaternion * yQuaternion;
			}
			else if (axes == RotationAxes.MouseX)
			{
				rotationX += Input.GetAxis("Mouse X") * sensitivityX;
				rotationX = ClampAngle (rotationX, minimumX, maximumX);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
				transform.localRotation = originalRotation * xQuaternion;
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = ClampAngle (rotationY, minimumY, maximumY);
				Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
				transform.localRotation = originalRotation * yQuaternion;
			}	
		}			
	}



	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}	
		return Mathf.Clamp (angle, min, max);
	}
}