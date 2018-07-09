using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Cameras/Observer")]
public class ObserverCamera : MonoBehaviour {
	public Transform target;
	public float lag = 10f;

	void OnValidate() {
		lag = lag < 0.01f ? 0.01f : lag;
	}

	void OnEnable() {
		if (!target)
			enabled = false;
	}

	void Update() {
		var originRot = transform.rotation;
		transform.LookAt(target);
		transform.rotation = Quaternion.Lerp(
			originRot, transform.rotation, Time.deltaTime / lag
		);
	}
}
