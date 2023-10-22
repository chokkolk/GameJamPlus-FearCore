using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemsUI : MonoBehaviour
{
    public Image ItemImage;
    public BounceUtil BounceUtil;

    // Start is called before the first frame update
    void Awake()
    {
        ItemImage = GetComponent<Image>();
        BounceUtil = GetComponent<BounceUtil>();
    }
}
