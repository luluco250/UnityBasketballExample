using UnityEngine;

public class MenuHandler : MonoBehaviour {
	public void OnQuit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
