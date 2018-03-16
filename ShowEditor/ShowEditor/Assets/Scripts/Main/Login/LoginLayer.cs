using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginLayer : Layer
{
    private SceneTrans trans = SceneStateManager.LOGIN_TRANS;
    public override void AfterStart()
    {
        gameObject.SetActive(false);
    }
    public void SwitchToThis()
    {
        gameObject.SetActive(true);
        SetInteractable(true);
        trans.StartTrans();
    }
    public void CloseIt()
    {
        trans.StartClosing();
        SetInteractable(false);
        SceneStateManager.USERMENU_TRANS.StartTrans();
    }
    void Update()
    {
        _ShowOtherLayers(trans);
        _HideOtherLayers(trans);
    }
    public override Layers GetSourceLayerId()
    {
        return Layers.LOGIN;
    }
}
