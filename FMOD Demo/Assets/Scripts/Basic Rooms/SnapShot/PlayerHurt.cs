using UnityEngine;
using System.Collections;

public class PlayerHurt : MonoBehaviour
{
    [FMODUnity.EventRef]    public string m_event;      // Sound event to play after player falls down.
    FMOD.Studio.EventInstance m_hurtEvent;              
    [SerializeField]    Transform m_resetPosition;      // Position to reset the player back to.
    [SerializeField]    GameObject m_objectToFace;      // Object to face upon resetting position.
    public float m_resetTimer;
    
	void Start ()
    {
        
	}
	
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Play Hurt sound
            // Apply visual effects
            // After amount of time, reset player back to upper platform at a position.
            StartCoroutine(ResetPlayerAfterTimer(col.gameObject));
        }
    }

    IEnumerator ResetPlayerAfterTimer(GameObject a_player)
    {
        a_player.GetComponent<ActorControls>().DisableMovement = true;
        // Lock player controls/position.
        // Start the sound event.
        if (m_event != "")
            m_hurtEvent.start();
        // Wait for timer.
        yield return new WaitForSeconds(m_resetTimer);
        // Stop the sound event.
        if (m_event != "")
            m_hurtEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        // Reset players position.
        a_player.transform.position = m_resetPosition.position;
        a_player.transform.LookAt(m_objectToFace.transform);
        a_player.GetComponent<ActorControls>().DisableMovement = false;
    }
}
