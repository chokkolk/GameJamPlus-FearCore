using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RespawnManager;

public class CharacterCollider : MonoBehaviour
{
    [SerializeField] private bool _isPlayerDetected;

    private PlayerItems _playerItems;
    private PlayerController _playerController;
    private InteractionUI _interactionUI;
    private RespawnManager _respawnManager;

    private void Awake()
    {
        _playerItems = FindObjectOfType<PlayerItems>();
        _playerController = FindObjectOfType<PlayerController>();
        _interactionUI = FindObjectOfType<InteractionUI>();
        _respawnManager = FindObjectOfType<RespawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerDetected &&
            _playerController.InputManager.WasPlayerInteracted())
        {
            _playerItems.MedkitsCollected -= 1;

            PlayerType pType = _playerController._actualPlayerType == PlayerType.Brother ? PlayerType.Sister : PlayerType.Brother;

            Respawn respawn = _respawnManager.GetRespawnByPlayerType(pType);
            
            Instantiate(this, respawn.transform.position, respawn.transform.rotation);

            Destroy(gameObject);
            _interactionUI.SetActiveUI(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") &&
            _playerItems.MedkitsCollected > 0)
        {
            _isPlayerDetected = true;

            if (_playerController._actualPlayerType == RespawnManager.PlayerType.Brother)
            {
                _interactionUI.SetTextUI("Pressione (E) para curar sua irmã.");
            }
            else
            {
                _interactionUI.SetTextUI("Pressione (E) para curar seu irmão.");
            }

            _interactionUI.SetActiveUI(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerDetected = false;
            _interactionUI.SetActiveUI(false);
        }
    }
}
