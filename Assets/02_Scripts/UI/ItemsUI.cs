using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour
{
    [SerializeField] private List<Image> _keysUI = new();
    [SerializeField] private List<Image> _medkitsUI = new();

    private PlayerItems _playerItems;


    private void Awake()
    {
        _playerItems = FindObjectOfType<PlayerItems>();
        ResetKeysUI();
        ResetMedkitsUI();
    }

    private void Update()
    {
        for(int i = 0; i < _playerItems.TotalKey; i++)
        {
            _keysUI[i].color = new Color(_keysUI[i].color.r, _keysUI[i].color.g, _keysUI[i].color.b, 1f);
        } 
        
        for(int i = 0; i < _playerItems.TotalMedkit; i++)
        {
            _medkitsUI[i].color = new Color(_medkitsUI[i].color.r, _medkitsUI[i].color.g, _medkitsUI[i].color.b, 1f);
        }
    }

    public void ResetKeysUI()
    {
        _keysUI.ForEach(x => x.color = new Color(x.color.r, x.color.g, x.color.b, 0.3f));
    }     
    public void ResetMedkitsUI()
    {
        _medkitsUI.ForEach(x => x.color = new Color(x.color.r, x.color.g, x.color.b, 0.3f));
    }


}
