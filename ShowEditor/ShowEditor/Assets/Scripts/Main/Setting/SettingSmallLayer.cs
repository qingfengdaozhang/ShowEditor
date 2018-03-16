using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSmallLayer : SmallLayer
{
    bool isShowing = false;
    bool isClosing = false;
    Vector2 targetR;
    public override void AfterStart()
    {
        targetR = new Vector2(GetOriginalPosition().x + GetBaseWidth(), GetOriginalPosition().y);
    }
    public override void SwitchToThis(bool immediately = false)
    {
        base.SwitchToThis(immediately);
        gameObject.SetActive(true);
        isShowing = true;
        isClosing = false;
        SetInteractable(true);
        Position = targetR;
        if (immediately) Position = GetOriginalPosition();
    }
    public override void CloseThis(bool immediately = false)
    {
        base.CloseThis(immediately);
        isClosing = true;
        isShowing = false;
        SetInteractable(false);
        Position = GetOriginalPosition();
        if (immediately) Position = targetR;
    }
    private void Update()
    {
        if (isShowing)
        {
            SetAlpha(Mathf.Lerp(GetAlpha(), 1f, Time.deltaTime*5f));
            Position = Vector2.Lerp(Position, GetOriginalPosition(), Time.deltaTime * 7f);
            if (Vector2.Distance(Position, GetOriginalPosition()) < 0.5f)
            {
                SetAlpha(1f);
                Position = GetOriginalPosition();
                isShowing = false;
            }
            isShowing = isShowing && !isClosing;
        }
        if (isClosing)
        {
            SetAlpha(Mathf.Lerp(GetAlpha(), 0f, Time.deltaTime*15f));
            Position = Vector2.Lerp(Position, targetR, Time.deltaTime * 7f);
            if (Vector2.Distance(Position, targetR) < 0.5f)
            {
                SetAlpha(0f);
                Position = targetR;
                gameObject.SetActive(false);
                isClosing = false;
            }
            isClosing = isClosing && !isShowing;
        }
    }
}
