using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class StarterAssetsInputs : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool interact;
	public bool interactTwo;
	public bool bag;
	public bool restart;
	public bool pause;
	public bool action;
	public bool anykey;
	[Header("Movement Settings")]
	public bool analogMovement;
	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;
	public PlayerInput PlayerInput;
	private int perviousHand = 0;
	public bool fullControl = true;

	public static StarterAssetsInputs instance;
    private void Awake()
    {
		instance = this;
    }

    public void SwitchMap()
    {
        if (fullControl)
        {
			if (UiController.instance.inventoryPanel.activeInHierarchy)
            {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			PlayerInput.SwitchCurrentActionMap("Tutorial");
			fullControl = false;
		}
		else
		{
			if (UiController.instance.inventoryPanel.activeInHierarchy)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			PlayerInput.SwitchCurrentActionMap("Player");
			fullControl = true;
		}
	}

	public void SwitchMap(bool tutorial, bool cursorVisible, CursorLockMode lockMode)
	{
		if (tutorial)
        {
			PlayerInput.SwitchCurrentActionMap("Tutorial");
		}
		else
        {
			PlayerInput.SwitchCurrentActionMap("Player");
		}
		Cursor.lockState = lockMode;
		Cursor.visible = cursorVisible;
	}
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnAnykey(InputValue value)
    {
		AnykeyInput(value.isPressed);
    }

	public void OnAction(InputValue value)
    {
		ActionInput(value.isPressed);
    }

	public void OnPause(InputValue value)
    {
		PauseInput(value.isPressed);
    }
	public void OnLook(InputValue value)
	{
		if(cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}
	public void OnInteract(InputValue value)
    {
		InteractInput(value.isPressed);
    }
	public void OnInteractTwo(InputValue value)
    {
		InteractTwoInput(value.isPressed);
    }
	public void OnBag(InputValue value)
    {
		BagInput(value.isPressed);
    }
	public void OnRestart(InputValue value)
    {

		RestartInput(value.isPressed);
    }
	public void OnCraftQ(InputValue value)
    {
		CraftInput(1);
    }
	public void OnCraftE(InputValue value)
    {
		CraftInput(2);
    }

// old input sys if we do decide to have it (most likely wont)...


	public void ActionInput(bool newActionState)
    {
		action = newActionState;
    }

	public void AnykeyInput(bool newAnykeyState)
    {
		anykey = newAnykeyState;
    }
	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void PauseInput(bool newPauseState)
    {
		pause = newPauseState;
    }

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}

	public void InteractInput(bool newInteractState)
    {
		interact = newInteractState;
    }
	public void InteractTwoInput(bool newInteractState)
    {
		interactTwo = newInteractState;
    }

	public void BagInput(bool newBagState)
    {
		bag = newBagState;
    }
	public void RestartInput(bool newRestartState)
    {
		restart = newRestartState;
    }
	public void CraftInput(int hand)
    {
		if(hand != perviousHand)
        {
			perviousHand = hand;
			CraftQTE.instance.craftValue++;
        }
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
	