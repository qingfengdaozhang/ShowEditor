using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLayerGroup : MonoBehaviour {
    List<SmallLayer> layers = new List<SmallLayer>();
    public SmallLayer startLayer;
    public SmallLayer NowLayer { get; set; }
    public SmallLayer LastLayer { get; set; }
    public List<SmallLayer> GetLayers()
    {
        return layers;
    }
    public void Register(SmallLayer layer) {
        layers.Add(layer);
    }
    public void Reset()
    {
        NowLayer = startLayer;
        LastLayer = null;
        for (int i = 0; i < layers.Count; i++)
        {
            var layer = layers[i];
            if (layer == startLayer)
            {
                layer.SwitchToThis(true);
            }else
            {
                layer.CloseThis(true);
            }
        }
    }
}
