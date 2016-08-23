/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class DownGear : ActionObject
{
    public Car m_car;
    void Start () {
	
	}
	void Update () {
	
	}
    public override void ActionPressed(GameObject sender)
    {
        m_car.DownGear();
    }
}