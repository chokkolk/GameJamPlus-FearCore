using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BounceUtil : MonoBehaviour
{
    [Header("Bounce Animation")]
    public float BounceDuration = .1f;
    public float BounceMaxScale = 1.2f;
    public Ease ScaleEase = Ease.OutBack;

    public void Bounce()
    {
        transform.DOScale(BounceMaxScale, BounceDuration).SetEase(ScaleEase).SetLoops(2, LoopType.Yoyo);
    }
}
