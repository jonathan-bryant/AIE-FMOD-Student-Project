/* ========================================================================================== */
/*                                                                                            */
/* FMOD Studio - C# Wrapper . Copyright (c), Firelight Technologies Pty, Ltd. 2004-2016.      */
/*                                                                                            */
/* ========================================================================================== */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionObject : MonoBehaviour
{
    public KeyCode[] m_actionKeys;
    public virtual void ActionPressed(GameObject a_sender, KeyCode a_key)
    {

    }
    public virtual void ActionReleased(GameObject a_sender, KeyCode a_key)
    {

    }
    public virtual void ActionDown(GameObject a_sender, KeyCode a_key)
    {

    }
}
