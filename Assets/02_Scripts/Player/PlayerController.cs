using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed = 3.0f;
    [SerializeField]
    private float _crouchSpeed = 1.5f;
    [SerializeField]
    private float _runSpeed = 5.0f;
    
    [SerializeField]
    private float _initialHeight = 0f;
    [SerializeField]
    private float _crouchHeight = -0.2f;

    [SerializeField]
    private GameObject playerSprite;

    protected bool isRunning;
    protected bool isCrouching;

    private float _actualSpeed;

    private CharacterController _controller;
    private InputManager _inputManager;
    private Transform _cameraTransform;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _inputManager = InputManager.Instance;
        _cameraTransform = Camera.main.transform;

        _actualSpeed = _walkSpeed;
    }

    void Update()
    {
        RunAction();

        CrouchAction();

        MovementAction();


    }

    #region Actions
    private void MovementAction()
    {
        Vector2 movement = _inputManager.GetPlayerMovement();

        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0f;

        _controller.Move(move * Time.deltaTime * _actualSpeed);
    }

    private void RunAction()
    {
        if (!isCrouching)
        {
            if (_inputManager.IsPlayerRunning())
            {
                _actualSpeed = _runSpeed;
                isRunning = true;
            }
            else
            {
                _actualSpeed = _walkSpeed;
                isRunning = false;
            }
        }
    }     

    private void CrouchAction()
    {
        if (!isRunning)
        {
            if (_inputManager.IsPlayerCrouching())
            {
                playerSprite.transform.position = new Vector3(transform.position.x, _crouchHeight, transform.position.z);
                //_controller.center = new Vector3(0f, _crouchHeight, 0f);
                //_controller.height = 0.1f;

                _actualSpeed = _crouchSpeed;
                isCrouching = true;
            }
            else
            {
                playerSprite.transform.position = new Vector3(transform.position.x, _initialHeight, transform.position.z);
                //_controller.center = new Vector3(0f, _initialHeight, 0f);
                //_controller.height = 2;

                _actualSpeed = _walkSpeed;
                isCrouching = false;
            }
        }
    }
    #endregion
}
