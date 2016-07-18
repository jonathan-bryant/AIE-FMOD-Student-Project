using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{
	[FMODUnity.EventRef]
	public string musicString;
	public FMOD.Studio.EventInstance m_musicEvent;

	private string m_levelString = "Level";
	private FMOD.Studio.ParameterInstance m_level;
	[Range(0, 80)]	public float m_levelValue;


	// Use this for initialization
	void Start ()
	{
		m_musicEvent = GetComponentInParent<SoundVFXPlayer>().m_musicEvent;
		Debug.Log(m_musicEvent.isValid());
	}
	void Update()
	{
		m_musicEvent.getParameterValue(m_levelString, out m_levelValue);
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log(col.gameObject.tag.ToString());
		if (col.gameObject.CompareTag("Player"))
		{
			m_musicEvent.setParameterValue(m_levelString, m_levelValue + 20);
			Debug.Log("Triggered!");

			if (gameObject.name == "Trigger5" || gameObject.name == "Trigger6")
			{
				m_musicEvent.setParameterValue(m_levelString, 0);
			}
		}
	}
}
