using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public int KeyMaxLimit { get; private set; } = 3;
    public int MedkitMaxLimit { get; private set; } = 1;

    // Key
    [SerializeField] private int _totalKey;
    public int TotalKey
    {
        get => _totalKey;
        set => _totalKey = CheckLimitItem(value, KeyMaxLimit);
    }

    // Water
    [SerializeField] private int _totalMedkit;
    public int TotalMedkit
    {
        get => _totalMedkit;
        set => _totalMedkit = CheckLimitItem(value, MedkitMaxLimit);
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
