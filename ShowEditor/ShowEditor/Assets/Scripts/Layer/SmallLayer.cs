using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLayer : Layer, ISmallLayerEx
{
    public SmallLayerGroup group;
    void Start()
    {
        canvas = LayerManager.GetTargetCanvas();
        layer = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originPosition = Position;
        localCanvasTranform = canvas.transform as RectTransform;
        group.Register(this);
        AfterStart();
    }
    public virtual void SwitchToThis(bool immediately = false)
    {
        if (group.NowLayer != this)
        {
            gameObject.SetActive(true);
            group.LastLayer = group.NowLayer;
           // Debug.Log(group.NowLayer);
            group.NowLayer.CloseThis();
            group.NowLayer = this;
           // Debug.Log(group.NowLayer);

        }
    }
    public virtual void CloseThis(bool immediately = false)
    {
    }
    public void _HideOtherLayerInGroup(ref bool isShowing)
    {
        if (isShowing)
        {
            float alpha = Mathf.Lerp(GetAlpha(), 1f, Time.deltaTime * 15f);
            alpha = alpha > 0.98f ? 1f : alpha;
            List<SmallLayer> layers = group.GetLayers();
            for (int i = 0; i < layers.Count; i++)
            {
                var layer = layers[i];
                if (layer != this)
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
                isShowing = false;
            }
        }
    }
}
