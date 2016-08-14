using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
	[FMODUnity.EventRef]
	public string m_soundString;
	FMOD.Studio.EventInstance m_soundEvent;

	float m_volumeValue = 0.0f;

	public Slider m_volumeSlider;
	public Text m_volumeText;

	// Use this for initialization
	void Start ()
	{
		m_soundEvent = FMODUnity.RuntimeManager.CreateInstance(m_soundString);
		m_soundEvent.start();

		m_soundEvent.getVolume(out m_volumeValue);
		m_volumeSlider.value = m_volumeValue;
		m_volumeText.text = (m_volumeValue * 100).ToString("N0");

		m_volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_soundEvent, transform, null);
	}

	void OnDestroy()
	{
		m_soundEvent.release();
	}

	void ValueChangeCheck()
	{
		m_volumeValue = m_volumeSlider.value;
		m_soundEvent.setVolume(m_volumeValue);
		m_volumeText.text = (m_volumeValue * 100).ToString("N0");
	}
}
