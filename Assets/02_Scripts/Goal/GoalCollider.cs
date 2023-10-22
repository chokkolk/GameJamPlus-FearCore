using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    [SerializeField] private bool _isPlayerDetected;

    private PlayerItems _playerItems;
    private PlayerController _playerController;
    private InteractionUI _interactionUI;

    private void Awake()
    {
        _playerItems = FindObjectOfType<PlayerItems>();
        _playerController = FindObjectOfType<PlayerController>();
        _interactionUI = FindObjectOfType<InteractionUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerDetected &&
            _playerController.InputManager.WasPlayerInteracted())
        {
            if (this.tag == "DoorTag" && _playerItems.KeysCollected >= 3)
            {
                // TODO: finalizar o game
                _interactionUI.SetActiveUI(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerDetected = true;

            if (_playerItems.KeysCollected < 3)
            {
                _interactionUI.SetTextUI("Ainda falta chave.");
            }
            else
            {
                _interactionUI.SetTextUI("Pressione (E) para escapar da casa.");
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
