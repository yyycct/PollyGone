using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControlManager : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public float height;
	[Header("Movement Settings")]
	public bool analogMovement;
	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;
	public PlayerInput PlayerInput;
	private int perviousHand = 0;
	public bool fullControl = true;


	public static CameraControlManager instance;
	private void Awake()
	{
		instance = this;
	}

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}
	
	public void OnLook(InputValue value)
	{
		if (cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnHeight(InputValue value)
    {
		HeightInput(value.Get<float>());
    }

	// old input sys if we do decide to have it (most likely wont)...



	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	}

	
	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	
	public void HeightInput(float newHeightDirection)
    {
		height = newHeightDirection;
    }

	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}

}
