using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayer {
    /// <summary>
    /// 该层的基本宽度
    /// </summary>
    /// <returns></returns>
    float GetBaseWidth();
    float GetBaseHeight();
    /// <summary>
    /// 组件游戏开始位置
    /// </summary>
    /// <returns></returns>
    Vector2 GetOriginalPosition();
    /// <summary>
    /// 该层往指定方向移动的距离
    /// </summary>
    /// <returns></returns>
    float GetMoveDistance();
    void SetAlpha(float alpha);
    float GetAlpha();
    void SetInteractable(bool interactive);
    bool GetInteractable();
    bool CanBeShownByOthers();


    Layers GetSourceLayerId();
    /// <summary>
    /// 当前位置
    /// </summary>
    Vector2 Position { get; set; }
}
