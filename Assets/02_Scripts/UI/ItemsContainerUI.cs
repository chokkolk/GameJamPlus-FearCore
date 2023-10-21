using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsContainerUI : MonoBehaviour
{
    [SerializeField] private List<ItemsUI> _keysUI = new();
    [SerializeField] private List<ItemsUI> _medkitsUI = new();

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
            _keysUI[i].ItemImage.color = new Color(_keysUI[i].ItemImage.color.r, _keysUI[i].ItemImage.color.g, _keysUI[i].ItemImage.color.b, 1f);
            _keysUI[i].BounceUtil.Bounce();
        } 
        
        for(int i = 0; i < _playerItems.TotalMedkit; i++)
        {
            _medkitsUI[i].ItemImage.color = new Color(_medkitsUI[i].ItemImage.color.r, _medkitsUI[i].ItemImage.color.g, _medkitsUI[i].ItemImage.color.b, 1f);
            _medkitsUI[i].BounceUtil.Bounce();
        }
    }

    public void ResetKeysUI()
    {
        _keysUI.ForEach(x => x.ItemImage.color = new Color(x.ItemImage.color.r, x.ItemImage.color.g, x.ItemImage.color.b, 0.3f));
    }     
    public void ResetMedkitsUI()
    {
        _medkitsUI.ForEach(x => x.ItemImage.color = new Color(x.ItemImage.color.r, x.ItemImage.color.g, x.ItemImage.color.b, 0.3f));
    }


}
