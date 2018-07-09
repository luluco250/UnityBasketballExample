using UnityEngine;
using System;

[ExecuteInEditMode]
public class DayCycleSystem : MonoBehaviour {
	public Light sun;
	public float sunIntensity = 1.2f;
	public float skyIntensity = 1.2f;

	[Range(0f, 1f)]
	public float debugTime = 0.25f;
	public bool useDebugTime = false;

	public int hour, minute, second, millisecond;
	public bool useCustomTime = false;

	private DayCycleSystem _instance;
	public DayCycleSystem instance { get { return _instance; }}

	void OnValidate() {
		hour = Mathx.Clamp(hour, 0, 23);
		minute = Mathx.Clamp(minute, 0, 59);
		second = Mathx.Clamp(second, 0, 59);
		millisecond = Mathx.Clamp(millisecond, 0, 999);
	}

	void OnEnable() {
		if (!sun)
			enabled = false;
	}

	void Awake() {
		if (_instance == null)
			_instance = this;
		else
			DestroyImmediate(this);
	}

	void Update() {
		var now = DateTime.Now;
		if (useCustomTime)
			now = new DateTime(1996, 7, 10, hour, minute, second, millisecond);
		//var now = new DateTime(1996, 7, 10, hour, minute, second, millisecond);

		float h = now.Millisecond / 1000f;
		h += now.Second; h /= 60f;
		h += now.Minute; h /= 60f;
		h += now.Hour; h /= 24f;

		/*float h = now.Hour;
		h += now.Minute / 60f;
		h += now.Second / 60f / 60f;
		h += now.Millisecond / 60f / 60f / 1000f;*/

		//float t = Mathf.Repeat(h / 24f - 0.25f, 1f);
		float t = Mathf.Repeat(h - 0.25f, 1f);
		if (useDebugTime)
			t = debugTime;

		sun.intensity = sunIntensity;

		sun.transform.rotation = Quaternion.Euler(new Vector3(
			t * 360f,
			-30f,
			0f
		));

		RenderSettings.ambientIntensity =
			(Mathf.Abs(Mathf.Repeat(t - 0.25f, 1f) - 0.5f) * 2f) * skyIntensity;
	}
}
