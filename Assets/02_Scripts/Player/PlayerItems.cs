using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public int KeyMaxLimit { get; private set; } = 3;
    public int MedkitMaxLimit { get; private set; } = 1;

    // Key
    [SerializeField] private int _keysCollected;
    public int KeysCollected
    {
        get => _keysCollected;
        set => _keysCollected = CheckLimitItem(value, KeyMaxLimit);
    }

    // Water
    [SerializeField] private int _medkitsCollected;
    public int MedkitsCollected
    {
        get => _medkitsCollected;
        set => _medkitsCollected = CheckLimitItem(value, MedkitMaxLimit);
    }

    private int CheckLimitItem(int value, float maxLimit)
    {
        if (value > maxLimit)
        {
            return (int)maxLimit;
        }

        if (value < 0)
        {
            return 0;
        }

        return value;
    }

}
