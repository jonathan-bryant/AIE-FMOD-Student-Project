/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		FMOD
Date:			24/08/2016
==================================================================*/

using UnityEngine;

public class QualitySettingsScript : MonoBehaviour 
{
#if UNITY_EDITOR || DEVELOPMEN_BUILD
    void OnGUI()
    {
        string[] names = QualitySettings.names;
        GUILayout.BeginVertical();
        int i = 0;
        while (i < names.Length)
        {
            if (GUILayout.Button(names[i]))
                QualitySettings.SetQualityLevel(i, true);

            i++;
        }
        GUILayout.EndVertical();
    }
#endif
}
