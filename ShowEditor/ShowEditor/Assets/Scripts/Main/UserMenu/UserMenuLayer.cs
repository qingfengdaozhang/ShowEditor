using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserMenuLayer : Layer, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    const float END_FACTOR = 0.25f;
    float offSetX;
    SceneTrans trans = SceneStateManager.USERMENU_TRANS;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (trans.InClosing())
        {
            return;
        }
        SceneStateManager.Clear();
        trans.ClearState();
        Vector2 mousePosition;
        GetLocalPosition(eventData.position, out mousePosition);
        offSetX = Position.x - mousePosition.x;
        //Debug.Log(mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (trans.InClosing())
        {
            return;
        }
        Vector2 mousePosition;
        if (GetLocalPosition(eventData.position, out mousePosition))
        {
            float x = mousePosition.x + offSetX;
            x = x > GetBaseWidth() ? GetBaseWidth() : x;
            Position = new Vector2(x, Position.y);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (trans.InClosing())
        {
            return;
        }
        if (Position.x < layer.rect.width * (1f - END_FACTOR))
        {
            trans.StartClosing();
            SceneStateManager.MENU_TRANS.StartTrans();
        }
        else
        {
            SwitchToThis();
        }
    }

    public void SwitchToThis()
    {
        trans.StartTrans();
    }

    // Update is called once per frame
    void Update()
    {
        if (trans.InProcess())
        {
            Vector2 targetPos = new Vector2(GetBaseWidth(), 0);
            if (Vector2.Distance(Position, targetPos) <= 0.1f)
            {
                Position = targetPos;
                trans.CompleteTrans();
            }
            else
            {
                Position = Vector2.Lerp(Position, targetPos, Time.deltaTime * 10f);
            }
        }
        if (trans.InClosing())
        {
            Vector2 targetPos = GetOriginalPosition();
            if (Vector2.Distance(Position, targetPos) <= 0.1f)
            {
                Position = targetPos;
                trans.CompleteClose();
            }
            else
            {
                Position = Vector2.Lerp(Position, targetPos, Time.deltaTime * 10f);
            }
        }
    }

    public override Layers GetSourceLayerId()
    {
        return Layers.USERMENU;
    }
}
