using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactionGameObject;

    private void Awake()
    {
        SetActiveUI(false);
    }

    public void SetActiveUI(bool active)
    {
        _interactionGameObject.SetActive(active);
    }
}
