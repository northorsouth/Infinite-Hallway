using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[System.Serializable]
	public class Axis
	{
		public string name;
		public float sensitivity;
	}

	[Tooltip("Defaults to attached gameobject")]
	public Transform PlayerTransform = null;

	[Tooltip("Defaults to main camera\nShould be child of player object.")]
	public Transform CameraTransform = null;

	public CharacterController controller = null;

	[Range(0, 500)]
	public float moveSpeed = 300f;

	[Range(0, 500)]
	public float lookSpeed = 100f;
	
	public string HorizontalMovementKey = "Horizontal Movement (Keyboard)";
	public string VerticalMovementKey = "Vertical Movement (Keyboard)";
	public string HorizontalMovementJoystick = "Horizontal Movement (XBox)";
	public string VerticalMovementJoystick = "Vertical Movement (XBox)";

	public string HorizontalLookKey = "Horizontal Look (Keyboard)";
	public string VerticalLookKey = "Vertical Look (Keyboard)";
	public string HorizontalLookJoystick = "Horizontal Look (XBox)";
	public string VerticalLookJoystick = "Vertical Look (XBox)";

	public float minVerticalCamera = 0;
	public float maxVerticalCamera = 180;

	public bool EnableMovement = true;
	public bool EnableLook = true;
	public bool LockCursor = true;
	public bool VisibleCursor = false;
	public bool EnableController = true;
	
	// Use this for initialization
	void Start ()
	{
		if (PlayerTransform == null)
			PlayerTransform = transform;
		
		if (CameraTransform == null)
			CameraTransform = Camera.main.transform;
		
		if (controller == null)
			controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (EnableLook)
		{
			PlayerTransform.Rotate(new Vector3(0f, Input.GetAxis(HorizontalLookKey), 0f) * Time.deltaTime * lookSpeed);
			CameraTransform.Rotate(new Vector3(-Input.GetAxis(VerticalLookKey), 0f, 0f) * Time.deltaTime * lookSpeed);

			if (EnableController)
			{
				PlayerTransform.Rotate(new Vector3(0f, -Input.GetAxis(HorizontalLookJoystick), 0f) * Time.deltaTime * lookSpeed);
				CameraTransform.Rotate(new Vector3(-Input.GetAxis(VerticalLookJoystick), 0f, 0f) * Time.deltaTime * lookSpeed);
			}

			Vector3 cameraRot = CameraTransform.rotation.eulerAngles;
			if (cameraRot.x>80 && cameraRot.x<90) cameraRot.x = 80;
			if (cameraRot.x>270 && cameraRot.x<280) cameraRot.x = 280;
			Debug.Log(cameraRot.x);
			CameraTransform.rotation = Quaternion.Euler(cameraRot);
		}
		
		if (EnableMovement)
		{
			Vector3 moveVec = CameraTransform.forward;
			moveVec.y = 0f;

			controller.SimpleMove(CameraTransform.forward * Input.GetAxis(VerticalMovementKey) * moveSpeed * Time.deltaTime);
			controller.SimpleMove(CameraTransform.right * Input.GetAxis(HorizontalMovementKey) * moveSpeed * Time.deltaTime);

			if (EnableController)
			{
				controller.SimpleMove(CameraTransform.forward * Input.GetAxis(VerticalMovementJoystick) * moveSpeed * Time.deltaTime);
				controller.SimpleMove(CameraTransform.right * Input.GetAxis(HorizontalMovementJoystick) * moveSpeed * Time.deltaTime);
			}
		}

		Cursor.lockState = LockCursor? CursorLockMode.Locked : CursorLockMode.Confined;

		Cursor.visible = VisibleCursor;
	}
}
