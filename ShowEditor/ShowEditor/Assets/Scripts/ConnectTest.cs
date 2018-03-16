using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectTest : MonoBehaviour
{
    public static string SERVER = "http://192.168.1.121:8000/change";
    public Sprite testImage;
    public Image getImage;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine(TestPost());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator TestPost()
    {
        Debug.Log("运行");
        WWWForm form = new WWWForm();
        form.AddField("cmd", "cubist.ckpt-done");
        //byte[] bs = testImage.texture.EncodeToPNG();
        //form.AddBinaryData("img", bs, "filename", "image/png");
        WWW w = new WWW(SERVER, form);
        yield return w;
        if (w.isDone && w.error == null)
        {
            Debug.Log(w.text);
            if (w.texture != null)
            {
                Texture2D tex = w.texture;
                Sprite spr = Sprite.Create(tex, new Rect(0,0, tex.width,tex.height),new Vector2(0.5f,0.5f));
                getImage.sprite = spr;
                yield return null;
            }
        }
    }
}
