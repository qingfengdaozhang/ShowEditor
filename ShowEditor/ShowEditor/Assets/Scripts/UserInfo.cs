using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour {
    public static UserInfo Instance { get; set; }
    public Image Style_PHOTO;
    private string id="1";
    private string username;
    private string userpassword;
    private bool isVip;
    private byte[] img;
    private void Awake()
    {
        Instance = this;
    }
    public void setImg(byte[] img)
    {
        this.img = img;
    }
    public void setId (string id)
    {
        this.id = id;
    }
    public void setUsername(string username)
    {
        this.username = username;
    } 
    public void setUserpassword(string userpassword)
    {
        this.userpassword = userpassword;
    }
    public void setVip(bool isvip)
    {
        this.isVip = isvip;
    }
    public string getId()
    {
        return id;
    }
    public string getUsername()
    {
        return username;
    }
    public string getUserpassword()
    {
        return userpassword;
    }
    public bool getIsVip()
    {
        return isVip;
    }
    public byte[] getImg()
    {
        return img;
    }
}
