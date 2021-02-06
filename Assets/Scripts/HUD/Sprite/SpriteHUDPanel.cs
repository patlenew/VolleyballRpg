using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// World UI
public abstract class SpriteHUDPanel : MonoBehaviour
{
    public virtual void AddListeners()
    {

    }

    public virtual void RemoveListeners()
    {

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
