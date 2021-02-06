using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteHUDManager : Singleton<SpriteHUDManager>
{
    [SerializeField] private SpriteHUDPanel[] _panels;

    protected override void Init_Awake()
    {
        base.Init_Awake();

        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].AddListeners();
            _panels[i].Hide();
        }
    }

    private void Start()
    {
        GameEvents.SpriteHUD_Show.Register(Show);
        GameEvents.SpriteHUD_Hide.Register(Hide);
    }

    private void OnDestroy()
    {
        GameEvents.SpriteHUD_Show.Remove(Show);
        GameEvents.SpriteHUD_Hide.Remove(Hide);

        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].RemoveListeners();
        }
    }

    public void Show(Type panelType)
    {
        SpriteHUDPanel panel = GetPanel(panelType);

        SetPanelParent(panel);

        panel.Show();
    }

    private void SetPanelParent(SpriteHUDPanel panel)
    {
        Transform spriteHudParent = CameraManager.Instance.SpriteHudHelper;

        if (panel.transform.parent != spriteHudParent)
        {
            panel.transform.SetParent(spriteHudParent);
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localRotation = Quaternion.identity;
        }
    }

    public void Hide(Type panelType)
    {
        SpriteHUDPanel panel = GetPanel(panelType);

        panel.Hide();
    }

    #region Helpers

    private SpriteHUDPanel GetPanel(Type panelType)
    {
        SpriteHUDPanel panel = _panels.FirstOrDefault(desiredPanel => desiredPanel.GetType() == panelType);

        if (panel == null)
        {

        }

        return panel;
    }

    #endregion
}
