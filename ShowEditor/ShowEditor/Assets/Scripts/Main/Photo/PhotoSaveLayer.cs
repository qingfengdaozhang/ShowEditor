using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSaveLayer : Layer {
    public PhotoMainLayer mainLayer;
    private SceneTrans trans = SceneStateManager.PHOTO_COMPLETE_TRANS;
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
        SceneStateManager.PHOTO_MAIN_TRANS.StartTrans();
        mainLayer.SetInteractable(true);
    }
    void Update()
    {
        _ShowOtherLayers(trans);
        _HideOtherLayers(trans);
    }
    public override bool CanBeShownByOthers()
    {
        return false;
    }
    public override Layers GetSourceLayerId()
    {
        return Layers.COMPLETE_PHOTO;
    }
}
