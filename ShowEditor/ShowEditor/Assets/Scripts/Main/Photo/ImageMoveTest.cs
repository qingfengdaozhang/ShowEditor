using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMoveTest : MonoBehaviour {
    // 记录上一次手机触摸位置判断用户是在做放大还是缩小手势  
    private Vector2 oldPosition1 = new Vector2(0, 0);
    private Vector2 oldPosition2 = new Vector2(0, 0);
    public ScrollRect rect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 1)
        {
            rect.movementType = ScrollRect.MovementType.Clamped;
            // 前两只手指触摸类型都为移动触摸  
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                // 计算出当前两点触摸点的位置  
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;
                // 函数返回真为放大，返回假为缩小  
                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    // 放大系数超过3以后不允许继续放大  
                    // 这里的数据是根据我项目中的模型而调节的，大家可以自己任意修改  
                    transform.localScale = transform.localScale * (1f + Time.deltaTime);
                }
                else
                {
                    // 缩小系数返回18.5后不允许继续缩小  
                    // 这里的数据是根据我项目中的模型而调节的，大家可以自己任意修改  
                    transform.localScale = transform.localScale * (1f - Time.deltaTime);
                }
                // 备份上一次触摸点的位置，用于对比  
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;

            }
        }
        else
        {
            rect.movementType = ScrollRect.MovementType.Unrestricted;
        }
    }
    // 函数返回真为放大，返回假为缩小  
    bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        // 函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势  
        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));

        if (leng1 < leng2)
        {
            // 放大手势  
            return true;
        }
        else
        {
            // 缩小手势  
            return false;
        }
    }
}
