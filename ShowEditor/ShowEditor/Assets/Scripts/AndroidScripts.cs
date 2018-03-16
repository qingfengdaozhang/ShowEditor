using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidScripts : MonoBehaviour
{
    public static AndroidScripts Instance { set; get; }
    public Image Target_PHOTO;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        string res = jo.Call<string>("Test");
        //DebugText.Out(res);
    }
    /// <summary>
    /// 打开相册
    /// </summary>

    public void OpenPhoto()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenGallery");
    }
    /// <summary>
    /// 打开相机
    /// </summary>

    public void OpenCamera()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("takephoto");
    }

    public void GetImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        StartCoroutine(LoadImage(imagePath));
    }

    public void GetTakeImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        StartCoroutine(LoadImage(imagePath));
    }

    private IEnumerator LoadImage(string imagePath)
    {
        WWW www = new WWW("file://" + imagePath);
        yield return www;
        if (www.error == null)
        {
            //成功读取图片，写自己的逻辑 
           // DebugText.Out("正在缓存");
            Texture2D tex = www.texture;
            CompressPhoto(tex);
            Debug.Log(tex.height + "width:" + tex.width);
            byte[] photoBytes = tex.EncodeToJPG();
            UserInfo.Instance.setImg(www.bytes);
            NetManager.upload();
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));


            var rect = Target_PHOTO.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(tex.width, tex.height);
            SetRectLocalScale(800, 1000, rect);
            rect = UserInfo.Instance.Style_PHOTO.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(tex.width, tex.height);
            SetRectLocalScale(800, 1000, rect);

            Target_PHOTO.sprite = sprite;
            PhotoMainLayer.Instance.SwitchToThis();
        }
        else
        {
            Debug.Log("read error");
        }
    }
    const int MAX_SIZE = 1440000;
    /// <summary>
    /// 超出指定分辨率的话便将其缩小
    /// </summary>
    void CompressPhoto(Texture2D tex)
    {
        int size = tex.width * tex.height;
        if (size > MAX_SIZE)
        {
            float scaleFactor = Mathf.Sqrt(MAX_SIZE / size);
            tex = ScaleTextureBilinear(tex, scaleFactor);
        }
    }
    /// <summary>
    /// 双线性插值缩小图片
    /// </summary>
    /// <param name="originalTexture"></param>
    /// <param name="scaleFactor"></param>
    /// <returns></returns>
    Texture2D ScaleTextureBilinear(Texture2D originalTexture, float scaleFactor)
    {
        Texture2D newTexture = new Texture2D(Mathf.CeilToInt(originalTexture.width * scaleFactor), Mathf.CeilToInt(originalTexture.height * scaleFactor));
        float scale = 1.0f / scaleFactor;
        int maxX = originalTexture.width - 1;
        int maxY = originalTexture.height - 1;
        for (int y = 0; y < newTexture.height; y++)
        {
            for (int x = 0; x < newTexture.width; x++)
            {
                // Bilinear Interpolation  
                float targetX = x * scale;
                float targetY = y * scale;
                int x1 = Mathf.Min(maxX, Mathf.FloorToInt(targetX));
                int y1 = Mathf.Min(maxY, Mathf.FloorToInt(targetY));
                int x2 = Mathf.Min(maxX, x1 + 1);
                int y2 = Mathf.Min(maxY, y1 + 1);

                float u = targetX - x1;
                float v = targetY - y1;
                float w1 = (1 - u) * (1 - v);
                float w2 = u * (1 - v);
                float w3 = (1 - u) * v;
                float w4 = u * v;
                Color color1 = originalTexture.GetPixel(x1, y1);
                Color color2 = originalTexture.GetPixel(x2, y1);
                Color color3 = originalTexture.GetPixel(x1, y2);
                Color color4 = originalTexture.GetPixel(x2, y2);
                Color color = new Color(Mathf.Clamp01(color1.r * w1 + color2.r * w2 + color3.r * w3 + color4.r * w4),
                    Mathf.Clamp01(color1.g * w1 + color2.g * w2 + color3.g * w3 + color4.g * w4),
                    Mathf.Clamp01(color1.b * w1 + color2.b * w2 + color3.b * w3 + color4.b * w4),
                    Mathf.Clamp01(color1.a * w1 + color2.a * w2 + color3.a * w3 + color4.a * w4)
                    );
                newTexture.SetPixel(x, y, color);
            }
        }

        return newTexture;
    }

    /// <summary>
    /// 根据屏幕缩放，比较简单。
    /// </summary>
    /// <param name="maxWidth"></param>
    /// <param name="maxHeight"></param>
    /// <param name="rect"></param>
    void SetRectLocalScale(float maxWidth, float maxHeight, RectTransform transform)
    {
        float width = transform.rect.width;
        float height = transform.rect.height;
        float scaler = 1f;
        if (width > maxWidth && height > maxHeight)
        {
            var woffset = maxWidth - width;
            var hoffset = maxHeight - height;
            if (woffset > hoffset)
            {
                scaler = maxWidth / width;
            }
            else
            {
                scaler = maxHeight / height;
            }
        }
        else
        {

            if (width > height)
            {
                scaler = maxWidth / width;
            }
            else
            {
                scaler = maxHeight / height;
            }
        }
        transform.localScale = new Vector3(scaler, scaler, 1f);
    }
}
