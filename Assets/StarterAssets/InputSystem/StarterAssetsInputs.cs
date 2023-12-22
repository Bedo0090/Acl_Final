using UnityEngine;
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
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;
		public bool reload;
		public bool switchWeapon;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			if (PlayerManager.isGrappled == false)
				MoveInput(value.Get<Vector2>());
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

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
#endif
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		public void OnReload(InputValue value)
		{
			ReloadInput(value.isPressed);
		}

		public void OnSwitchWeapon(InputValue value)
        {
			SwitchWeaponInput(value.isPressed);
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
			if (aim)
				sprint = false;
			else
				sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			if (SceneSwitch.inventorySceneActive == true)
				aim = false;
			else
				aim = newAimState;
			if (aim)
				sprint = false;
		}

		public void ShootInput(bool newShootState)
        {
			if (aim)
				shoot = newShootState;
        }

		public void ReloadInput(bool newAimState)
		{
			reload = newAimState;
		}

		public void SwitchWeaponInput(bool newSwitchWeaponState)
        {
			switchWeapon = newSwitchWeaponState;
        }

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public static void SetCursorState(bool newState)
		{
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = !newState;
        }
	}
	
}