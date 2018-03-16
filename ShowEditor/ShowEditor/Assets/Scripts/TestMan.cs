using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMan : MonoBehaviour {

	public void Set()
    {
        UserInfo.Instance.setUsername("Test");
        UserInfo.Instance.setUserpassword("123456");
    }
    public  void Signin()
    {
        Set();
        Debug.Log("登录账户");
        NetManager.signin();
    }
}
