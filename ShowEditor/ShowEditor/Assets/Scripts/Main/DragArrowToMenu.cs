using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragArrowToMenu : Layer, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    const float ToMenuMoveFactor = 0.25f;
    SceneTrans mainTrans = SceneStateManager.MAIN_TRANS;
    SceneTrans menuTrans = SceneStateManager.MENU_TRANS;
    bool isDraging = false;

    float offSetY = 0f;

    public override Layers GetSourceLayerId()
    {
        return Layers.MAIN;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (menuTrans.IsNowScene() || menuTrans.InProcess())
        {
            return;
        }
        isDraging = true;
        //Input.multiTouchEnabled = false;
        mainTrans.ClearState();
        Vector2 mousePosition;
        //Debug.Log(rectTransform);
        GetLocalPosition(eventData.position, out mousePosition);
        offSetY = Position.y - mousePosition.y;
        BlurController.StartBlur();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (menuTrans.IsNowScene() || menuTrans.InProcess())
        {
            return;
        }
        if (isDraging == false)
        {
            return;
        }
        Vector2 mousePosition;
        //Debug.Log(eventData.pointerCurrentRaycast.screenPosition);
        if (GetLocalPosition(eventData.position, out mousePosition))
        {
            //Debug.Log(eventData.delta.y);
            Position = new Vector2(Position.x, mousePosition.y + offSetY);
            float factor = (Position.y) / GetBaseHeight();
            BlurController.SetBlurSize(factor);
        }
        if (eventData.delta.y > 20)
        {
            menuTrans.StartTrans();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Position.y > 0 && Vector2.Distance(GetOriginalPosition(), Position) > GetBaseHeight() * ToMenuMoveFactor)
        {
            //DebugText.Out(arrowTrans.anchoredPosition.y+"");
            menuTrans.StartTrans();
        }
        if (menuTrans.IsNowScene() || menuTrans.InProcess())
        {
            return;
        }
        if (isDraging)
        {

            mainTrans.StartTrans();
            isDraging = false;
        }
    }
    void Update()
    {
        //回到开始界面
        if (mainTrans.InProcess())
        {

            if (Vector2.Distance(Position, GetOriginalPosition()) >= 0.1f)
            {
                Position = Vector2.Lerp(Position, GetOriginalPosition(), Time.deltaTime * 15f);
                float factor = (Position.y) / (layer.rect.height);
                BlurController.SetBlurSize(factor);
            }
            else
            {
                Position = GetOriginalPosition();
                mainTrans.CompleteTrans();
                BlurController.DeleteBlur();
            }
        }
        //菜单开启并且没有完全切换到Menu场景时调用。
        if (menuTrans.InProcess())
        {
            Vector2 targetV2 = new Vector2(GetOriginalPosition().x, GetOriginalPosition().y + layer.rect.height);
            if (Vector2.Distance(Position, targetV2) >= 0.1f)
            {
                Position = Vector2.Lerp(Position, targetV2, Time.deltaTime * 10f);
                float factor = (Position.y) / (layer.rect.height);
                BlurController.SetBlurSize(factor);
            }
            else
            {
                Position = targetV2;
                menuTrans.CompleteTrans();
                //Debug.Log("MENU_C_Trans");
            }
        }
    }
    /// <summary>
    /// 返回到开始界面状态
    /// </summary>
    public void BackToMain()
    {
        SceneStateManager.Clear();
        menuTrans.ClearState();
        mainTrans.StartTrans();
    }
    
}
