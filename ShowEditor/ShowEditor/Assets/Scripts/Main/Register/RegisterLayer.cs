using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterLayer : Layer
{
    private SceneTrans trans = SceneStateManager.REGISTER_TRANS;
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

    public override bool CanBeShownByOthers()
    {
        return false;
    }

    public override Layers GetSourceLayerId()
    {
        return Layers.REGISTER;
    }
    void Update()
    {
        _HideOtherLayers(trans);
        if (GetAlpha()==0f)
        {
            gameObject.SetActive(false);
        }
    }
}
