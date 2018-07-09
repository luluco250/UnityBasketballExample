using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CameraSystem))]
public class CameraSystemEditor : Editor {
	SerializedProperty cameras;
	SerializedProperty cameraNumber;

	void OnEnable() {
		cameras = serializedObject.FindProperty("cameras");
		cameraNumber = serializedObject.FindProperty("_cameraNumber");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();

		EditorGUILayout.PropertyField(cameras, new GUIContent("Cameras"), true);

		int cn = EditorGUILayout.IntField("Camera #", cameraNumber.intValue);
		
		for (int i = 0; i < targets.Length; ++i) {
			var cs = (CameraSystem)targets[i];

			cs.cameraNumber = cn;
		}

		serializedObject.ApplyModifiedProperties();
	}
}
