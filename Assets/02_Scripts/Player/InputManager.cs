using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }

    private PlayerControls _playerControls;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _playerControls = new PlayerControls();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }                            

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return _playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool IsPlayerRunning()
    {
        return _playerControls.Player.Run.IsPressed();
    }

    public bool IsPlayerCrouching()
    {
        return _playerControls.Player.Crouch.IsPressed();
    }


    public bool WasPlayerInteracted()
    {
        return _playerControls.Player.Interact.WasPressedThisFrame();
    }
}
