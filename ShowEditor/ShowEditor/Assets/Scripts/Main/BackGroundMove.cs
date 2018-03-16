using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMove : MonoBehaviour
{
    const float offsetPixel = 200f;
    public Image bg;
    Transform bgTrans;
    Vector3 originV3;
    void Start()
    {
        SceneStateManager.NowScene = Scene.MAIN;
        bgTrans = bg.transform;
        var ori = bgTrans.localPosition;
        originV3 = new Vector3(ori.x, ori.y, ori.z);
    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneStateManager.MAIN_TRANS.IsNowScene())
        //{
            float gx = -Input.acceleration.x * offsetPixel;
            float gy = -Input.acceleration.y * offsetPixel;
            Vector3 nowPos = bgTrans.localPosition;
            Vector3 targetPos = new Vector3(originV3.x + gx, originV3.y + gy, originV3.z);
            bgTrans.localPosition = Vector3.Lerp(nowPos, targetPos, Time.deltaTime * 3f);

        //}
    }
}
