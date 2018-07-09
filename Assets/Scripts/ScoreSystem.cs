using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
	public Text scoreText;

	private static ScoreSystem _instance;
	public static ScoreSystem instance { get { return _instance; } }

	private static long _score = 0;
	public static long score {
		get { return _score; }
		set {
			_score = value;
			instance.scoreText.text = score.ToString();
		}
	}

	void OnEnable() {
		if (!scoreText)
			enabled = false;
	}

	void Awake() {
		if (_instance == null)
			_instance = this;
		else
			DestroyImmediate(this);
	}

	void Update() {
		instance.scoreText.text = score.ToString();
	}
}
