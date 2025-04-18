using UnityEngine;
using UnityEngine.Events;
using PanettoneGames.GenEvents;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Send Input as event to the TutorialManager")]
		public IntEvent tutorialEvents;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		//Player input does not send these events automatically So I am having to grab these manually.
        private void OnEnable()
        {
        }
        private void OnDisable()
        {
        }
        public void OnMove(InputAction.CallbackContext context)
		{
			if(TutorialManager.currentEvent == TutorialManager.TutorialEventIDs.DirectionInputEvent)
				tutorialEvents.Raise((int)TutorialManager.TutorialEventIDs.DirectionInputEvent);
			
			MoveInput(context.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext context)
		{
			if(cursorInputForLook)
			{
				if(TutorialManager.currentEvent == TutorialManager.TutorialEventIDs.MouseInputEvent)
					tutorialEvents.Raise((int)TutorialManager.TutorialEventIDs.MouseInputEvent);
				LookInput(context.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			JumpInput(true);
		}

		// Sprinting is kind of broken rn, Im just going to leave it enabled for now
		// But this is kind of a gamey feature anyways.
		public void OnSprint(InputAction.CallbackContext context)
		{
			SprintInput(true);
		}

		public void OnSprintCanceled(InputAction.CallbackContext context) 
		{
			SprintInput(false);
		}
#endif


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