using UnityEngine;

public class Basketball : MonoBehaviour {
	public float respawnDistance = 50f;
	public Collider topScoreCollider;
	public Collider bottomScoreCollider;

	Vector3 originalPosition;
	int scoreState = 0;

	private Rigidbody _rb;
	public Rigidbody rb {
		get { return _rb; }
	}
	
	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}

	void Start() {
		originalPosition = transform.position;
	}

	void Update() {
		if (transform.position.magnitude > respawnDistance)
			Reset();
	}

	void Reset() {
		rb.velocity = Vector3.zero;
		transform.position = originalPosition; //new Vector3(0f, 2f, 0f);
		transform.rotation = Quaternion.identity;
	}

	void OnTriggerEnter(Collider c) {
		if (c == topScoreCollider)
			if (scoreState == -1)
				scoreState = -2;
			else
				scoreState = 1;
		else if (c == bottomScoreCollider)
			if (scoreState == 1)
				scoreState = 2;
			else
				scoreState = -1;
	}

	void OnTriggerExit(Collider c) {
		if (scoreState == 2 && c == topScoreCollider)
			++ScoreSystem.score;
		else if (scoreState == -2 && c == bottomScoreCollider)
			--ScoreSystem.score;
		scoreState = 0;
	}
}
