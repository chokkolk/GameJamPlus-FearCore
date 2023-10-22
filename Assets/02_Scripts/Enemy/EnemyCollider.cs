using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.OnDead();
        }
    }
}
