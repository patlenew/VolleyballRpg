using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFader : MonoBehaviour
{
    protected CanvasGroup CanvasGroup
    {
        get
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
            }

            return _canvasGroup;
        }
    }

    private CanvasGroup _canvasGroup;

    [Header("Fader Settings")]
    [SerializeField] private float _showDuration = 0.5f;
    [SerializeField] private float _hideDuration = 0.5f;

    private Coroutine _currentFadeCoroutine;

    public void Show(float delay = 0f)
    {
        StopFadeCoroutine();

        _currentFadeCoroutine = StartCoroutine(FadeCoroutine(1f, _showDuration));
    }

    private IEnumerator FadeCoroutine(float goal, float duration)
    {
        float t = 0f;
        float step = 1f / duration;
        float start = CanvasGroup.alpha;

        while (t < 1f)
        {
            yield return null;

            t += Time.deltaTime * step;

            CanvasGroup.alpha = Mathf.Lerp(start, goal, t);
        }

        CanvasGroup.alpha = goal;

        _currentFadeCoroutine = null;
    }

    public void Hide(float delay = 0f)
    {
        StopFadeCoroutine();

        _currentFadeCoroutine = StartCoroutine(FadeCoroutine(0f, _hideDuration));
    }

    private void StopFadeCoroutine()
    {
        if (_currentFadeCoroutine != null)
        {
            StopCoroutine(_currentFadeCoroutine);
            _currentFadeCoroutine = null;
        }
    }
}
