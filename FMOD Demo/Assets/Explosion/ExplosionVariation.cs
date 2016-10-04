using UnityEngine;
using System.Collections;

public class ExplosionVariation : MonoBehaviour {
	public float loopDuration;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float r = Mathf.Sin((Time.time / loopDuration) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin((Time.time / loopDuration + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin((Time.time / loopDuration + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;
		GetComponent<MeshRenderer>().material.SetVector("_ChannelFactor", new Vector4(r,g,b,0));
	}
}
