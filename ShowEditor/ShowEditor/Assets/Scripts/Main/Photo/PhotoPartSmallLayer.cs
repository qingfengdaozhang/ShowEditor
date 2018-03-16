using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPartSmallLayer : SmallLayer {
    bool isShowing = false;
    public override void SwitchToThis(bool immediately = false)
    {
        base.SwitchToThis(immediately);
        gameObject.SetActive(true);
        isShowing = true;
        SetInteractable(true);
        if (immediately) Position = GetOriginalPosition();
    }
    public override void CloseThis(bool immediately = false)
    {
        base.CloseThis(immediately);
        isShowing = false;
        SetInteractable(false);
        Position = GetOriginalPosition();
    }
    private void Update()
    {
        _HideOtherLayerInGroup(ref isShowing);
    }
}
