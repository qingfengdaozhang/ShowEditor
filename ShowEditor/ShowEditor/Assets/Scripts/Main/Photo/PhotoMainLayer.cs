using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoMainLayer : Layer {
    public static PhotoMainLayer Instance { set; get; }
    public SmallLayerGroup group;
    private SceneTrans trans = SceneStateManager.PHOTO_MAIN_TRANS;
    void Awake()
    {
        Instance = this;
    }
    public override void AfterStart()
    {
        gameObject.SetActive(false);
    }
    public void SwitchToThis()
    {
        gameObject.SetActive(true);
        SetInteractable(true);
        group.Reset();
        trans.StartTrans();
    }
    public void CloseIt()
    {
        trans.StartClosing();
        SetInteractable(false);
        SceneStateManager.MENU_TRANS.StartTrans();
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
        return Layers.PHOTO;
    }
}
