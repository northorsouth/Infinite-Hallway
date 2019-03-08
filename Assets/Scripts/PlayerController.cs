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
	
	public Axis HorizontalMovementKey = new Axis { name="X Move Key", sensitivity=1f };
	public Axis VerticalMovementKey = new Axis { name="Y Move Key", sensitivity=1f };
	public Axis HorizontalMovementJoystick = new Axis { name="X Move Joy", sensitivity=1f	 };
	public Axis VerticalMovementJoystick = new Axis { name="Y Move Joy", sensitivity=1f };

	public Axis HorizontalLookKey = new Axis { name="X Look Key", sensitivity=1f };
	public Axis VerticalLookKey = new Axis { name="Y Look Key", sensitivity=1f };
	public Axis HorizontalLookJoystick = new Axis { name="X Look Joy", sensitivity=100f };
	public Axis VerticalLookJoystick = new Axis { name="Y Look Joy", sensitivity=100f };

	public bool EnableMovement = true;
	public bool EnableLook = true;
	public bool LockCursor = true;
	public bool VisibleCursor = false;
	public bool EnableController = false;
	
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
			PlayerTransform.Rotate(new Vector3(0f, Input.GetAxis(HorizontalLookKey.name), 0f) * Time.deltaTime * HorizontalLookKey.sensitivity);
			CameraTransform.Rotate(new Vector3(-Input.GetAxis(VerticalLookKey.name), 0f, 0f) * Time.deltaTime * VerticalLookKey.sensitivity);

			if (EnableController)
			{
				PlayerTransform.Rotate(new Vector3(0f, -Input.GetAxis(HorizontalLookJoystick.name), 0f) * Time.deltaTime * HorizontalLookJoystick.sensitivity);
				CameraTransform.Rotate(new Vector3(-Input.GetAxis(VerticalLookJoystick.name), 0f, 0f) * Time.deltaTime * VerticalLookJoystick.sensitivity);
			}
		}
		
		if (EnableMovement)
		{
			Vector3 moveVec = CameraTransform.forward;
			moveVec.y = 0f;

			controller.SimpleMove(CameraTransform.forward * Input.GetAxis(VerticalMovementKey.name) * VerticalMovementKey.sensitivity * Time.deltaTime);
			controller.SimpleMove(CameraTransform.right * Input.GetAxis(HorizontalMovementKey.name) * HorizontalMovementKey.sensitivity * Time.deltaTime);

			if (EnableController)
			{
				controller.SimpleMove(CameraTransform.forward * Input.GetAxis(VerticalMovementJoystick.name) * VerticalMovementJoystick.sensitivity * Time.deltaTime);
				controller.SimpleMove(CameraTransform.right * Input.GetAxis(HorizontalMovementJoystick.name) * HorizontalMovementJoystick.sensitivity * Time.deltaTime);
			}
		}

		Cursor.lockState = LockCursor? CursorLockMode.Locked : CursorLockMode.Confined;

		Cursor.visible = VisibleCursor;
	}
}
