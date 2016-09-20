/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     No                                                              |
|   Description:                The base class for all objects that can be intacted with.       |
|   Derived classes will inherit the appropriate classes.                                       |
===============================================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionObject : MonoBehaviour
{
    public KeyCode[] m_actionKeys; //List of all keys that can be pressed to activate the ActionObject

    //When the key has been pressed that frame
    public virtual void ActionPressed(GameObject a_sender, KeyCode a_key)
    {

    }
    //When the key has been released that frame
    public virtual void ActionReleased(GameObject a_sender, KeyCode a_key)
    {

    }
    //When the key has been held down for more than one frame
    public virtual void ActionDown(GameObject a_sender, KeyCode a_key)
    {

    }
}
