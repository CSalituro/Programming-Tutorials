using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  private static Controls _controls;
  
  public static void Init(Player myPlayer)
  {
    _controls = new Controls();
    _controls.Permanent.Enable();
    _controls.InGame.Movement.performed += hi =>
    {
      myPlayer.SetMovementDirection(hi.ReadValue<Vector3>());
    };
    _controls.InGame.Jump.started += hello =>
    {
      myPlayer.Jump();
    };

    _controls.InGame.Look.performed += ctx =>
    {
      myPlayer.SetLookRotatiom(ctx.ReadValue<Vector2>());
    };

    _controls.InGame.Shoot.started += ctx =>
    {
      myPlayer.Shoot();
    };
    
    _controls.InGame.Reload.started += ctx =>
    {
      myPlayer.Reload();
    };
  }

  public static void SetGameControls()
  {
    _controls.InGame.Enable();
    _controls.UI.Disable();
  }

  public static void SetUIControls()
  {
    _controls.UI.Enable();
    _controls.InGame.Disable();
  }
}
