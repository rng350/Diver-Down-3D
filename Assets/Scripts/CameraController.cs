using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Vector2 mouseLook;
	private Vector2 smoothVec;
	[SerializeField] float sensitivity;
	[SerializeField] float smoothing;
	[SerializeField] bool invertMouseAxis;
	[SerializeField] GameObject diver;

	void Start()
	{
		diver = this.transform.parent.gameObject;
	}

	void Update()
	{
		Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothVec.x = Mathf.Lerp(smoothVec.x, mouseDelta.x, 1f / smoothing);
		smoothVec.y = Mathf.Lerp(smoothVec.y, mouseDelta.y, 1f / smoothing);
		mouseLook += smoothVec;

		mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
		
		transform.localRotation = Quaternion.AngleAxis((invertMouseAxis? -1 : 1) * mouseLook.y, Vector3.right);
		diver.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, diver.transform.up);
	}
}