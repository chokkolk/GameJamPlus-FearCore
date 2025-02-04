using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
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
            if(this.tag == "KeyTag")
            {
                _playerItems.KeysCollected += 1;                
            }   
            else if(this.tag == "MedkitTag")
            {
                _playerItems.MedkitsCollected += 1;
            }

            Destroy(gameObject);
            _interactionUI.SetActiveUI(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerDetected = true;

            if (this.tag == "KeyTag")
            {
                _interactionUI.SetTextUI("Pressione (E) para coletar a chave.");
            }
            else
            {
                _interactionUI.SetTextUI("Pressione (E) para coletar a cura.");
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
