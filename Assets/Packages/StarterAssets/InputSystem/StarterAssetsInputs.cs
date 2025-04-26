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
		// This is how the tutorial Manager, and all the various parts of the tutorial talk to eachother.
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
        public void OnMove(InputAction.CallbackContext context)
		{
			// This is where the move event is sent to the tutorial manager. To keep it from spamming events, the tutorial manager
			// has to be looking for this specific event. And all the event id's are stored in the tutorial manager, to make
			// sure you dont have to be running all over the place chaning ids.
			if(tutorialEvents != null && TutorialManager.currentEvent == TutorialManager.TutorialEventIDs.DirectionInputEvent)
				tutorialEvents.Raise((int)TutorialManager.TutorialEventIDs.DirectionInputEvent);
			
			MoveInput(context.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext context)
		{
			// This is where the cursor look around event is sent to the tutorial manager. To keep it from spamming events, the tutorial manager
			// has to be looking for this specific event. And all the event id's are stored in the tutorial manager, to make
			// sure you dont have to be running all over the place chaning ids.
			if(cursorInputForLook)
			{
				if(tutorialEvents != null && TutorialManager.currentEvent == TutorialManager.TutorialEventIDs.MouseInputEvent)
					tutorialEvents.Raise((int)TutorialManager.TutorialEventIDs.MouseInputEvent);
				LookInput(context.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			JumpInput(true);
		}

		// Spriting does not disable automatically. I dont really think this program should require
		// Sprinting anyways. So I am going to turn it off.
		public void OnSprint(InputAction.CallbackContext context)
		{
			// SprintInput(true);
		}

		public void OnSprintCanceled(InputAction.CallbackContext context) 
		{
			// SprintInput(false);
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
			// SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			// Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}