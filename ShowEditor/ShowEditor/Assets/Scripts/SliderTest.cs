using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour {
    public Image Target_PHOTO;
    public Image Style_PHOTO;
    public Slider slider;
    public void  OnSliderChanged()
    {
        Target_PHOTO.color = new Color(255, 255, 255,1- slider.value);
        Style_PHOTO.color = new Color(255, 255, 255, slider.value);
    }
}
