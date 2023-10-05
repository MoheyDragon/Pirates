using UnityEngine;
using System;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
        [Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public Action <float> OnZoomAction;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}
		public void OnZoom(InputValue value)
        {
			Zoom(value.Get<Vector2>());
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
		public Action Attack;
		public void OnAttack(InputValue value)
        {
			Attack?.Invoke();
        }
		public Action Draw;
		public void OnDraw(InputValue value)
        {
			Draw?.Invoke();
        }
		public Action Interact;
		public void OnInteract(InputValue value)
        {
			Interact?.Invoke();
        }
		public Action SwitchWeapon;
		public void OnSwitchWeapon(InputValue value)
        {
			SwitchWeapon?.Invoke();
        }
		public void OnNextCharacter(InputValue value)
        {
			CharacterSelector.singleton.NextCharacter();
        }
		public void OnPreviousCharacter(InputValue value)
		{
			CharacterSelector.singleton.PreviousCharacter();
		}
		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
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
		public void Zoom(Vector2 zoomValue)
        {
			OnZoomAction?.Invoke(zoomValue.y);
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
	
}