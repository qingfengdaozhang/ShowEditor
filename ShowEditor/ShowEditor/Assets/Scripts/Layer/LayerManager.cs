using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public Canvas targetCanvas;
    public static LayerManager Instance { get; set; }
    static Dictionary<Layers, ILayer> layersDic = new Dictionary<Layers, ILayer>();
    /// <summary>
    /// 注册指定Layer。
    /// </summary>
    /// <param name="layerId"></param>
    /// <param name="layer"></param>
    public static void RegisterLayer(Layers layerId, ILayer layer)
    {
        if (layersDic.ContainsKey(layerId))
        {
            layersDic[layerId] = layer;
        }
        else
        {
            layersDic.Add(layerId, layer);
        }
    }
    /// <summary>
    /// 获取所有Layers.
    /// </summary>
    /// <returns></returns>
    public static List<ILayer> GetLayers()
    {
        return new List<ILayer>(layersDic.Values);
    }
    private void Awake()
    {
        Instance = this;
    }
    public static Canvas GetTargetCanvas()
    {
        return Instance.targetCanvas;
    }
}
/// <summary>
/// 层级id枚举。
/// </summary>
public enum Layers
{
    NULL,
    MAIN,
    USERMENU,
    LOGIN,
    REGISTER,
    SETTING,
    PHOTO,
    COMPLETE_PHOTO
}