/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using System.Collections;

public class UpLoad : ActionObject
{
    public Car m_car;
    void Start () {
	
	}
	void Update () {
	
	}
    protected override void Action(GameObject sender, bool a_use)
    {
        m_car.UpLoad();
    }
}