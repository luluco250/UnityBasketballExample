using UnityEngine;

public class PickUp : MonoBehaviour {
	public Vector3 offset = new Vector3(0f, -0.2f, 1f);

	private Rigidbody _rb;
	public Rigidbody rb { get { return _rb; }}

	private Collider _col;
	public Collider col { get { return _col; }}

	void Awake() {
		_rb = GetComponent<Rigidbody>();
		_col = GetComponent<Collider>();
	}
}
