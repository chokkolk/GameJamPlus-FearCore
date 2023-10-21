using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerItems PlayerItems;
    public InputManager InputManager;

    [SerializeField]
    private float _walkSpeed = 3.0f;
    [SerializeField]
    private float _crouchSpeed = 1.5f;
    [SerializeField]
    private float _runSpeed = 5.0f;
    
    [SerializeField]
    private float _standHeight = 0.7f;
    [SerializeField]
    private float _crouchHeight = -0.2f;

    [SerializeField]
    private GameObject playerHead;

    [SerializeField]
    private GameObject _interactionUI;

    protected bool _isRunning;
    protected bool _isCrouching;
    public bool WasInterectedWithItem;
    protected bool _closeToItem;

    private float _actualSpeed;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;

    private CharacterController _controller;
    private Transform _cameraTransform;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        InputManager = InputManager.Instance;
        _cameraTransform = Camera.main.transform;

        _actualSpeed = _walkSpeed;
        PlayerItems = GetComponent<PlayerItems>();
    }

    void Update()
    {
        if(!_isCrouching) RunAction();

        //CrouchAction();
    }

    private void FixedUpdate()
    {
        MovementAction();
    }

    #region Movement Actions
    private void MovementAction()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = InputManager.GetPlayerMovement();

        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0f;

        _controller.Move(move * Time.fixedDeltaTime * _actualSpeed);

        // Gravity effect
        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        _controller.Move(playerVelocity * Time.fixedDeltaTime);
    }

    private void RunAction()
    {
        if (InputManager.IsPlayerRunning())
        {
            _actualSpeed = _runSpeed;
            _isRunning = true;
        }
        else
        {
            _actualSpeed = _walkSpeed;
            _isRunning = false;
        }
    }     

    private void CrouchAction()
    {
        
        if (InputManager.IsPlayerCrouching())
        {
            playerHead.transform.position = new Vector3(transform.position.x, _crouchHeight, transform.position.z);
            //_controller.center = new Vector3(0f, _crouchHeight, 0f);
            //_controller.height = 0.1f;

            _actualSpeed = _crouchSpeed;
            _isCrouching = true;
        }
        else
        {
            playerHead.transform.position = new Vector3(transform.position.x, _standHeight, transform.position.z);
            //_controller.center = new Vector3(0f, _initialHeight, 0f);
            //_controller.height = 2;

            _actualSpeed = _walkSpeed;
            _isCrouching = false;
        }
    }
    #endregion

    #region Interaction Actions

    //private void OnPlayerInteracted()
    //{
    //    if (!WasInterectedWithItem &&
    //        _closeToItem &&
    //        InputManager.WasPlayerInteracted())
    //    {
    //        WasInterectedWithItem = true;
    //    }
    //}

    //private void OnInteraction(GameObject gameObject)
    //{
    //    gameObject.SetActive(false);
    //    WasInterectedWithItem = false;
    //    _closeToItem = false;

    //    _interactionUI.SetActive(false);
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == _itemTag)
    //    {
    //        if (WasInterectedWithItem)
    //        {
    //            OnInteraction(other.gameObject);
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == _itemTag)
    //    {
    //        _closeToItem = true;
    //        _interactionUI.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == _itemTag)
    //    {
    //        _closeToItem = false;
    //        _interactionUI.SetActive(false);
    //    }
    //}
    #endregion
}
