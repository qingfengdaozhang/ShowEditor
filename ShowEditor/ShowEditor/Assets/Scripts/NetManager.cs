using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetManager : MonoBehaviour
{
    public Image TargetImage;
    private const string url = "http://1753050yk8.imwork.net/";
    public static NetManager Instance { get; set; }
    private const string FAILED = "failed";
    private const string SUCCESS = "success";
    private void Awake()
    {
        Instance = this;
    }
    public static Coroutine signin()
    {
        return Instance.StartCoroutine(Instance.Signin());
    }
    public static Coroutine register()
    {
        return Instance.StartCoroutine(Instance.Register());
    }
    public static Coroutine upload()
    {
        return Instance.StartCoroutine(Instance.uploadimg());
    }
    public static Coroutine transformation(string name)
    {
        return Instance.StartCoroutine(Instance.Transformation(name));
    }
    /// <summary>
    /// 登录
    /// </summary>
    IEnumerator Signin()
    {
        string username = UserInfo.Instance.getUsername();
        string password = UserInfo.Instance.getUserpassword();
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        WWW w = new WWW(url + "signin", form);
        yield return w;
        if (w.isDone && !string.IsNullOrEmpty(w.text))
        {
            if (w.text == FAILED)
            {
                ///登录失败
                Debug.Log("登录失败");
            }
            else
            {
                UserInfo.Instance.setId(w.text);
                ///登陆成功
                Debug.Log("登陆成功");
            }
        }
        else
        {
            // 无法连接服务器
        }
    }
    /// <summary>
    /// 注册
    /// </summary>
    /// <returns></returns>
    IEnumerator Register()
    {
        string username = UserInfo.Instance.getUsername();
        string password = UserInfo.Instance.getUserpassword();
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        WWW w = new WWW(url + "register", form);
        yield return w;
        if (w.isDone && !string.IsNullOrEmpty(w.text))
        {
            if (w.text == FAILED)
            {
                ///注册失败
                Debug.Log("注册失败");
            }
            else
            {
                UserInfo.Instance.setId(w.text);
                ///注册成功
                Debug.Log("注册成功");
            }
        }
        else
        {
            //无法连接服务器
        }
    }
    /// <summary>
    /// 上传
    /// </summary>
    /// <returns></returns>
    IEnumerator uploadimg()
    {
        byte[] img = UserInfo.Instance.getImg();
        string id = UserInfo.Instance.getId();
        string filename = Guid.NewGuid().ToString();
        WWWForm form = new WWWForm();
        form.AddField("userid", id);
        form.AddBinaryData("img", img, filename);
        WWW w = new WWW(url + "uploadimg", form);
        yield return w;
        if (w.isDone && string.IsNullOrEmpty(w.text))
        {
            if (w.text == FAILED)
            {
                Debug.Log("失败");
            }
            else
            {
                Debug.Log("成功");
            }
        }
        else
        {
            //无法连接服务器
        }
    }
    IEnumerator Transformation(string name)
    {
        string id = UserInfo.Instance.getId();
        WWWForm form = new WWWForm();
        form.AddField("userid", id);
        form.AddField("modelname", name);
        WWW w = new WWW(url + "transformation", form);
        yield return w;
        if (w.isDone)
        {
            if (w.text == FAILED)
            {
                Debug.Log("图片过大");
            }
            else
            {
                var oldSprite = UserInfo.Instance.Style_PHOTO.sprite;
                UserInfo.Instance.Style_PHOTO.sprite = null;
                Destroy(oldSprite);
                Texture2D tex = w.texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

                UserInfo.Instance.Style_PHOTO.sprite = sprite;
                TargetImage.color = new Color(255f, 255f, 255f, 0f);

            }
        }
        else
        {
            //无法连接服务器
        }
    }
}