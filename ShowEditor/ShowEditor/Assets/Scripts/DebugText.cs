using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {
    public DebugText Instance { set; get; }
    public Text debug;
	void Start () {
        Instance = this;
	}
	public void Out(string text)
    {
        debug.text = text;
    }
}
