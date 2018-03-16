using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurController : MonoBehaviour
{
    public static BlurController Instance { get; set; }
    public Image image;
    static Material mat;
    const float MAX_BLUR_SIZE = 1.5f;
    static bool bluring = false;
    //static float blurSize = 0f;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        mat = image.material;
        Debug.Log(mat);
        DeleteBlur();
    }
    /// <summary>
    /// 开始blur（不会自动模糊，需要控制模糊程度(0~1))
    /// </summary>
    /// <param name="cover"></param>
    public static void StartBlur()
    {
        Instance.gameObject.SetActive(true);
        bluring = true;
    }
    public static void DeleteBlur()
    {
        bluring = false;
        SetBlurSize(0f);
        Instance.gameObject.SetActive(false);
    }
    public static void SetBlurSize(float process)
    {
        //mat.SetFloat("_Size", process * MAX_BLUR_SIZE);
        var oColor = Instance.image.color;
        Color color = new Color(oColor.r,oColor.g,oColor.b,process * 0.5f);
        Instance.image.color = color;
    }
    void Update()
    {

    }
}
