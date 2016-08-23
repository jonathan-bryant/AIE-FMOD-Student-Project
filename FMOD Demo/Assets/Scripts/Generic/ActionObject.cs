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
    bool m_InUse = false;
    bool m_isHighlighted;

    public void Use(GameObject sender)
    {
        m_InUse = !m_InUse;
        Action(sender, m_InUse);
    }
    protected virtual void Action(GameObject sender, bool a_use)
    {

    }
}
