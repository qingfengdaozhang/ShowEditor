using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 层级：
/// <para>对同一层的界面进行操作。一层(Layer)可以对应多个场景(Scene)，反之则不然。</para>
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class Layer : MonoBehaviour, ILayer
{
    [HideInInspector]
    public Canvas canvas;
    [HideInInspector]
    public RectTransform layer;
    protected Vector2 originPosition;
    protected RectTransform localCanvasTranform;
    protected CanvasGroup canvasGroup;
    /// <summary>
    /// Layer的RectTranform.anchoredPosition
    /// </summary>
    public Vector2 Position
    {
        get
        {
            return layer.anchoredPosition;
        }

        set
        {
            layer.anchoredPosition = value;
        }
    }
    /// <summary>
    /// 子类必须实现的方法，用于注册该层级。
    /// </summary>
    /// <returns></returns>
    public virtual Layers GetSourceLayerId()
    {
        return Layers.NULL;
    }
    /// <summary>
    /// 是否能被其他Layer的_ShowOtherLayers所显示。
    /// </summary>
    /// <returns></returns>
    public virtual bool CanBeShownByOthers()
    {
        return true;
    }

    public float GetBaseHeight()
    {
        return layer.rect.height;
    }

    public float GetBaseWidth()
    {
        return layer.rect.width;
    }

    public virtual float GetMoveDistance()
    {
        return 0f;
    }
    /// <summary>
    /// 进入游戏时记录的原位置
    /// </summary>
    /// <returns></returns>
    public Vector2 GetOriginalPosition()
    {
        return originPosition;
    }
    /// <summary>
    /// 所在画布的RectTransform
    /// </summary>
    /// <returns></returns>
    public RectTransform GetCanvasRectTransform()
    {
        return localCanvasTranform;
    }
    /// <summary>
    /// 输入事件位置输出在该层里鼠标的相对坐标。
    /// </summary>
    /// <param name="position"></param>
    /// <param name="localPosition"></param>
    /// <returns></returns>
    public bool GetLocalPosition(Vector2 position, out Vector2 localPosition)
    {
        return RectTransformUtility.ScreenPointToLocalPointInRectangle(GetCanvasRectTransform(), position, Camera.main, out localPosition);
    }
    void Start()
    {
        canvas = LayerManager.GetTargetCanvas();
        layer = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originPosition = Position;
        localCanvasTranform = canvas.transform as RectTransform;
        LayerManager.RegisterLayer(GetSourceLayerId(), this);
        AfterStart();
    }
    /// <summary>
    /// 需要额外执行在Start函数里的方法。
    /// </summary>
    public virtual void AfterStart()
    {

    }
    /// <summary>
    /// 设置该层的透明度。
    /// </summary>
    /// <returns></returns>
    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
    /// <summary>
    /// 获取该层透明度。
    /// </summary>
    /// <returns></returns>
    public float GetAlpha()
    {
        return canvasGroup.alpha;
    }
    /// <summary>
    /// 设置该层是否可交互。
    /// </summary>
    /// <param name="interactive"></param>
    public void SetInteractable(bool interactive)
    {
        canvasGroup.interactable = interactive;
        canvasGroup.blocksRaycasts = interactive;
    }
    /// <summary>
    /// 返回该层是否可交互。
    /// </summary>
    /// <returns></returns>
    public bool GetInteractable()
    {
        return canvasGroup.interactable;
    }
    /// <summary>
    /// 在update里调用，当trans出现时，隐藏其他layer，只出现这个layer
    /// </summary>
    /// <param name="trans"></param>
    public void _HideOtherLayers(SceneTrans trans)
    {
        if (trans.InProcess())
        {
            float alpha = Mathf.Lerp(GetAlpha(), 1f, Time.deltaTime * 15f);
            alpha = alpha > 0.98f ? 1f : alpha;
            List<ILayer> layers = LayerManager.GetLayers();
            for (int i = 0; i < layers.Count; i++)
            {
                var layer = layers[i];
                if ((Layer)layer != this)
                {
                    if (layer.GetAlpha() > 1f - alpha)
                    {
                        layer.SetAlpha(1f - alpha);
                    }
                    if (alpha == 1f)
                    {
                        layer.SetInteractable(false);
                    }
                }
                else
                {
                    layer.SetAlpha(alpha);
                }
            }
            if (alpha == 1f)
            {
                trans.CompleteTrans();
            }
        }
    }
    /// <summary>
    /// 在update里调用，当trans关闭时，出现其他layer，消除这个layer
    /// </summary>
    /// <param name="trans"></param>
    public void _ShowOtherLayers(SceneTrans trans)
    {
        if (trans.InClosing())
        {
            float alpha = Mathf.Lerp(GetAlpha(), 0f, Time.deltaTime * 15f);
            alpha = alpha < 0.02f ? 0f : alpha;
            List<ILayer> layers = LayerManager.GetLayers();
            for (int i = 0; i < layers.Count; i++)
            {
                var layer = layers[i];
                if ((Layer)layer != this)
                {
                    if (!layer.CanBeShownByOthers())  continue; 
                    if (layer.GetAlpha() < 1f - alpha)
                        layer.SetAlpha(1f - alpha);
                    layer.SetInteractable(true);
                }
                else
                {
                    layer.SetAlpha(alpha);
                }
            }
            if (alpha == 0f)
            {
                trans.CompleteClose();
                gameObject.SetActive(false);
            }
        }
    }
}
