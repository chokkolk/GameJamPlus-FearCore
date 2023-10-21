using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeUtil : MonoBehaviour
{
    [Header("Fade Animation")]
    public float TransitionDuration = 5f;
    public float FadeDuration = 1f;
    public Ease ScaleEase = Ease.OutBack;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        FadeOut();
    }

    public IEnumerator DoDeathFade()
    {
        FadeIn();

        yield return new WaitForSeconds(TransitionDuration);

        FadeOut();
    }

    public void FadeIn()
    {
        _image.DOFade(1f, FadeDuration);
    }

    public void FadeOut()
    {
        _image.DOFade(0f, FadeDuration);
    }
}
