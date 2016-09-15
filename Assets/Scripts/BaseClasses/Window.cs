using UnityEngine;
using System.Collections;

public class Window : BaseWindow {

	// Use this for initialization

    public override void WinOnGUI()
    {
        Button(new Rect(0, 0, 100, 100), "asd");
    }

    public override void WinStart()
    {
        this.Position = new Rect(100, 100, 200, 200);
        Debug.Log("WinStart");
    }

    public override void WinUpdate()
    {
        Debug.Log("WinUpdate");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}