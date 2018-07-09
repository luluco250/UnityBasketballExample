using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Master : MonoBehaviour {
	public Canvas hudCanvas, pauseCanvas;

	// --- Pause State Stuff ---
	// Was the mouse shown before we paused?
	private static bool _wasMouseShown;
	// What was the timescale before we paused?
	private static float _timeScale;
	private static bool _paused = false;
	public static bool paused {
		get { return _paused; }
		set {
			// If we're actually changing the pause state
			if (value != _paused) {
				if (value) { // If we're pausing...
					_timeScale = Time.timeScale;
					Time.timeScale = 0f;
					_wasMouseShown = showMouse;
					showMouse = true;
				} else { // if we're resuming...
					Time.timeScale = _timeScale;
					showMouse = _wasMouseShown;
				}
				_paused = value;

				instance.hudCanvas.enabled = !value;
				instance.pauseCanvas.enabled = value;
			}
		}
	}

	// --- Mouse State Stuff ---
	private static bool _showMouse = false;
	public static bool showMouse {
		get { return _showMouse; }
		set {
			if (value != _showMouse) {
				if (value) {
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				} else {
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				}
				_showMouse = value;
			}
		}
	}

	// --- Singleton Stuff ---
	private static Master _instance;
	public static Master instance {
		get { return _instance; }
	}

	void OnEnable() {
		if (!hudCanvas || !pauseCanvas)
			enabled = false;
	}

	void Awake() {
		if (_instance == null)
			_instance = this;
		else
			DestroyImmediate(this);


		_wasMouseShown = false;
		_timeScale = Time.timeScale;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update() {
		if (Input.GetButtonDown("Pause"))
			paused = !paused;

		// Cursor settings need to be updated constantly to ensure it works
		Cursor.visible = showMouse;
		Cursor.lockState = showMouse ? CursorLockMode.None
		                             : CursorLockMode.Locked;
	}

	void OnApplicationPause(bool b) {
		if (b)
			paused = true;
	}

	void OnApplicationFocus(bool b) {
		if (!b)
			paused = true;
	}
}
