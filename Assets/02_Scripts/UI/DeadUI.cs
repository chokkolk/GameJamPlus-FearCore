using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUI : MonoBehaviour
{
    [SerializeField] private GameObject _deadUIGameObject;

    private void Awake()
    {
        SetActiveUI(false);
    }

    public void SetActiveUI(bool active)
    {
        _deadUIGameObject.SetActive(active);
    }
}
