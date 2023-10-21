using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactionGameObject;
    [SerializeField] private TextMeshProUGUI _interactionUIText;

    private void Awake()
    {
        SetActiveUI(false);
    }

    public void SetActiveUI(bool active)
    {
        _interactionGameObject.SetActive(active);
    }

    public void SetTextUI(string text)
    {
        _interactionUIText.text = text;
    }
}
