using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleTest : MonoBehaviour {
    public void UploadImg()
    {        
    }
    public void Style_Tf()
    {
        UserInfo.Instance.setId("1");
        NetManager.transformation(this.name);
    }
}
