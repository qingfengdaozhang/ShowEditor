using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 负责管理当前界面的状态。
/// </summary>
public class SceneStateManager
{
    public static SceneTrans MAIN_TRANS = new SceneTrans(Scene.MAIN);
    public static SceneTrans MENU_TRANS = new SceneTrans(Scene.MENU);
    public static SceneTrans USERMENU_TRANS = new SceneTrans(Scene.USERMENU);
    public static SceneTrans LOGIN_TRANS = new SceneTrans(Scene.LOGIN);
    public static SceneTrans REGISTER_TRANS = new SceneTrans(Scene.REGISTER);
    public static SceneTrans SETTING_TRANS = new SceneTrans(Scene.SETTING);
    public static SceneTrans PHOTO_MAIN_TRANS = new SceneTrans(Scene.PHOTO_MAIN);
    public static SceneTrans PHOTO_COMPLETE_TRANS = new SceneTrans(Scene.PHOTO_COMPLETE);
    public static Scene NowScene
    {
        get
        {
            return nowScene;
        }

        set
        {
            lastScene = nowScene;
            nowScene = value;
        }
    }
    private static Scene nowScene;
    private static Scene lastScene;
    public static void Clear()
    {
        NowScene = Scene.NULL;
    }
    /// <summary>
    /// 返回之前一个场景。
    /// </summary>
    /// <returns></returns>
    public static Scene GetLastScene()
    {
        return lastScene;
    }
}

public class SceneTrans
{
    bool isProcess = false;
    bool isClosing = false;
    Scene targetScene;
    public SceneTrans(Scene targetScene)
    {
        this.targetScene = targetScene;
    }
    /// <summary>
    /// 停止转化，并将当前场景转化为目标场景。
    /// </summary>
    /// <returns></returns>
    public void CompleteTrans()
    {
        isProcess = false;
        isClosing = false;
        SceneStateManager.NowScene = targetScene;
    }
    /// <summary>
    /// 停止关闭。
    /// </summary>
    public void CompleteClose()
    {
        isProcess = false;
        isClosing = false;
    }
    /// <summary>
    /// 是否当前场景是目标场景。
    /// </summary>
    /// <returns></returns>
    public bool IsNowScene()
    {
        return SceneStateManager.NowScene == targetScene;
    }
    /// <summary>
    /// 是否在转化中。
    /// </summary>
    /// <returns></returns>
    public bool InProcess()
    {
        return isProcess;
    }
    public bool InClosing()
    {
        return isClosing;
    }
    /// <summary>
    /// 开始转化
    /// </summary>
    public void StartTrans()
    {
        isProcess = true;
        isClosing = false;
        //SceneStateManager.Clear();
    }
    /// <summary>
    /// 开始关闭
    /// </summary>
    public void StartClosing()
    {
        isClosing = true;
        isProcess = false;
    }
    /// <summary>
    /// 清空状态
    /// </summary>
    public void ClearState()
    {
        isProcess = false;
        isClosing = false;
    }
}
public enum Scene
{
    NULL,
    MAIN,
    MENU,
    USERMENU,
    LOGIN,
    REGISTER,
    SETTING,
    PHOTO_MAIN,
    PHOTO_COMPLETE
}
