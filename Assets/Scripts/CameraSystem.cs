using UnityEngine;
using System.Collections.Generic;

public class CameraSystem : MonoBehaviour {
	[SerializeField]
	public List<Camera> cameras = new List<Camera>();
	
	[SerializeField]
	private int _cameraNumber = 0;
	public int cameraNumber {
		get { return _cameraNumber; }
		set {
			if (value >= 0 && value < cameras.Count) {
				cameras[_cameraNumber].enabled = false;
				cameras[value].enabled = true;
				_cameraNumber = value;
			}
		}
	}

	void OnEnable() {
		if (cameras.Count < 1)
			enabled = false;
	}

	void Start() {
		cameras[0].enabled = true;
		for (int i = 1; i < cameras.Count; ++i)
			cameras[i].enabled = false;
	}

	void Update() {
		if (Input.GetButtonDown("ChangeCamera"))
			cameraNumber = (cameraNumber + 1) % cameras.Count;
	}
}
