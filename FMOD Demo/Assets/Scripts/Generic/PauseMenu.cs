/*=================================================================
Project:		#AIE FMOD#
Developer:		#Cameron Baron#
Company:		#COMPANY#
Date:			#01/08/2016#
==================================================================*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    // Public Vars

    // Private Vars
    [SerializeField]    Canvas m_pauseMenu;
    
	void Start () 
	{
        m_pauseMenu.enabled = false;

        DontDestroyOnLoad(this);        
	}
	
	void Update () 
	{
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!m_pauseMenu.isActiveAndEnabled)
            {
                //Set active
                ActivateMenu();
            }
            else
            {
                //Deactivate
                DeactivateMenu();
            }
        }
	}

    public void DropDownValueChangedHandler(Dropdown a_value)
    {
        //if (SceneManager.GetSceneAt(a_value.value - 1) != SceneManager.GetActiveScene())
        {
            SceneManager.LoadScene(a_value.value);
            DeactivateMenu();
        }
    }

    #region Private Functions

    void ActivateMenu()
    {
        m_pauseMenu.enabled = true;
        Cursor.visible = true;
    }

    void DeactivateMenu()
    {
        m_pauseMenu.enabled = false;
        Cursor.visible = false;
    }

    #endregion
}
