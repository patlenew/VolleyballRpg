using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    protected RectTransform rectTransform => transform as RectTransform;

    [Header("Base UI Panel Settings")]
    [SerializeField] private CanvasFader _fader;

    public void Show(bool instant = false, float delay = 0f)
    {
        if (instant)
        {
            _fader.Show(delay);
        }
        else
        {
            _fader.Show_Instant();
        }
    }

    public void Hide(bool instant = false, float delay = 0f)
    {
        if (instant)
        {
            _fader.Hide(delay);
        }
        else
        {
            _fader.Hide_Instant();
        }
    }
}
