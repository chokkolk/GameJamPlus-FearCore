using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static RespawnManager;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public PlayerItems PlayerItems;
    public InputManager InputManager;

    public bool IsDead;
    public bool IsRespawned;
    public bool WasInterectedWithItem;

    public PlayerType _actualPlayerType;
    private PlayerType _previusPlayerType;

    [SerializeField] private float _walkSpeed = 3.0f;
    [SerializeField] private float _crouchSpeed = 1.5f;
    [SerializeField] private float _runSpeed = 5.0f;
    [SerializeField] public float _stamina = 100.0f;
    [SerializeField] private float decreaseStaminaSpeed = 10f;
    [SerializeField] private float increaseStaminaSpeed = 10f;
                     
    [SerializeField]  private float _standHeight = 0.7f;
    [SerializeField] private float _crouchHeight = -0.2f;
                     
    [SerializeField]  private GameObject playerHead;
                     
    [SerializeField] private DeadUI _deadUI;
    [SerializeField] private ItemsContainerUI _itemsContainerUI;

    [SerializeField] private GameObject _brotherPrefab;
    [SerializeField] private GameObject _sisterPrefab;
    [SerializeField] private GameObject _keyPrefab;

    protected bool _isRunning;
    protected bool _isCrouching;
    protected bool _closeToItem;

    private float _actualSpeed;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;

    private CharacterController _controller;
    private Transform _cameraTransform;
    private FadeUtil _fadeUtil;
    private RespawnManager _respawnManager;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        InputManager = InputManager.Instance;
        _cameraTransform = Camera.main.transform;

        _actualSpeed = _walkSpeed;
        PlayerItems = GetComponent<PlayerItems>();
        
        _fadeUtil = FindObjectOfType<FadeUtil>();
        _deadUI = FindObjectOfType<DeadUI>();

        _respawnManager = FindObjectOfType<RespawnManager>();
                                     
        _actualPlayerType = _actualPlayerType == PlayerType.None ? PlayerType.Brother : _actualPlayerType;

        Respawn();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if(!_isCrouching) RunAction();

        //CrouchAction();
    }

    private void FixedUpdate()
    {
        if(!IsDead && IsRespawned)
        {
            MovementAction();
        }
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
        if (InputManager.IsPlayerRunning() && _stamina > 0)
        {
            if (_stamina > 0f)
            {
                _stamina -= 1f * Time.deltaTime * decreaseStaminaSpeed;
            }

            _actualSpeed = _runSpeed;
            _isRunning = true;
        }
        else
        {
            _actualSpeed = _walkSpeed;
            _isRunning = false;

            if (_stamina < 100f)
            {
                _stamina += 1f * Time.deltaTime * increaseStaminaSpeed;
            }
        }
    }    
    
    //private IEnumerator OnRecoveryStamina()
    //{
    //    _stamina -= 1f * Time.deltaTime * decreaseStaminaSpeed;
    //}

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

    #region Dead Actions
    public void SetDead(bool isDead)
    {
        IsDead = isDead;
    }

    public void OnDead()
    {
        Cursor.visible = true;

        IsDead = true;

        _fadeUtil.FadeIn();

        _previusPlayerType = _actualPlayerType;

        GameObject actualPlayerPrefab = _actualPlayerType == PlayerType.Brother ? _brotherPrefab : _sisterPrefab;

        InstantiateObjects(actualPlayerPrefab);

        _respawnManager.SetPlayerTypeAvailability(_actualPlayerType, false);

        _actualPlayerType = _respawnManager.GetNextPlayerTypeAvailable().Value;

        //TODO: implementar logica de quando o player type for NONE

        _deadUI.SetActiveUI(true);
        //TODO: implementar respawn

        Respawn();
    }

    private void Respawn()
    {
        Respawn nextRespawn = _respawnManager.GetRespawnByPlayerType(_actualPlayerType);

        this.transform.position = nextRespawn.transform.position;

        Invoke(nameof(SetRespawn), 1); 
    }

    // NÃO EXCLUIR ESTE MÉTODO, POIS ELE GARANTE QUE O PLAYER INTANCIA NO RESPAWN ANTES 
    //  DO "CharacterController" CONTROLAR A POSIÇÃO DO PLAYER.
    private void SetRespawn()
    {
        IsRespawned = true;
    }

    private void InstantiateObjects(GameObject actualPlayerPrefab)
    {
        Instantiate(actualPlayerPrefab, this.transform.position, this.transform.rotation);

        Vector3 keyRespawnPosition = this.transform.position + new Vector3(0f, 3f, 0f);

        for (int i= 0; i < PlayerItems.KeysCollected; i++)
        {
            Instantiate(_keyPrefab, keyRespawnPosition, this.transform.rotation);
        }

        PlayerItems.KeysCollected = 0;
        PlayerItems.MedkitsCollected = 0;
    }
    #endregion
}
