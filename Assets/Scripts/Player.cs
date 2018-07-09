using UnityEngine;

public class Player : MonoBehaviour {
	public Vector2 sensitivity = Vector2.one;
	public float speed = 1f;
	public float stopSpeed = 1f;
	public float throwForce = 10f;
	public float throwMaxTime = 0.3f;
	public float maxPickupDistance = 5f;
	public float kickForce = 5f;

	Rigidbody rb;
	Vector3 rot = Vector3.zero;
	Camera cam;
	PickUp pickup = null;
	float currentThrow = -1f;
	int throwState = 0;
	float throwStartTime;

	void OnValidate() {
		throwMaxTime = throwMaxTime < 0f ? 0f : throwMaxTime;
	}
	
	void Awake() {
		rb = GetComponent<Rigidbody>();
		cam = GetComponentInChildren<Camera>();
	}

	void FixedUpdate() {
		rot.x -= Input.GetAxis("CameraY") * sensitivity.x;
		rot.y += Input.GetAxis("CameraX") * sensitivity.y;

		rot.x = Mathf.Clamp(rot.x, -90f, 90f);
		rot.y = Mathf.Repeat(rot.y, 360f);

		rb.rotation = Quaternion.Euler(Vector3.Scale(rot, rb.transform.up));

		Vector3 mov = new Vector3(
			Input.GetAxis("MoveX"),
			0f,
			Input.GetAxis("MoveY")
		);
		mov.Normalize();
		mov *= speed;

		rb.AddRelativeForce(mov, ForceMode.Impulse);
		rb.AddForce(-rb.velocity * stopSpeed);
	}

	void Update() {
		if (Input.GetButtonDown("PickUp")) {
			if (pickup == null) {
				RaycastHit hit;
				if (Touch(out hit))
					PickUp(hit.transform.gameObject.GetComponent<PickUp>());
			} else {
				Throw();
			}
		}

		if (Input.GetButtonDown("Kick")) {
			var feetPos = transform.position;
			feetPos -= transform.up * 0.5f;
			RaycastHit hit;
			if (Physics.Raycast(feetPos, transform.forward, out hit, maxPickupDistance)) {
				var obj = hit.transform.gameObject.GetComponent<PickUp>();
				if (obj)
					obj.rb.AddForce(transform.forward * kickForce, ForceMode.Impulse);
			}
		}
	/*}

	void LateUpdate() {*/
		cam.transform.rotation = Quaternion.Euler(rot);

		if (pickup) {
			var offset = pickup.offset;
			offset.z -= currentThrow;
			pickup.transform.localPosition = offset;
			pickup.transform.localRotation = Quaternion.identity;

			switch (throwState) {
				case 0:
					currentThrow = 0f;
					if (Input.GetButton("Throw")) {
						throwState = 1;
						throwStartTime = Time.time;
					}
					break;
				case 1:
					currentThrow = Time.time - throwStartTime;
					
					if (currentThrow >= 1f || !Input.GetButton("Throw")) {
						throwState = 2;
						currentThrow = currentThrow > 1f ? 1f : currentThrow;
					}
					break;
				case 2:
					Throw(currentThrow * throwForce);
					break;
			}	
		}
	}

	bool Touch(out RaycastHit hit) {
		return Physics.Raycast(
			cam.transform.position, cam.transform.forward, out hit, 
			maxPickupDistance
		);
	}

	void PickUp(PickUp b) {
		if (b) {
			b.rb.useGravity = false;
			b.transform.parent = cam.transform;
			b.col.enabled = false;
			pickup = b;
		}
	}

	void Throw(float force = 0f) {
		if (pickup) {
			pickup.rb.angularVelocity = rb.angularVelocity;
			pickup.rb.velocity = rb.velocity;
			pickup.rb.useGravity = true;
			pickup.transform.localPosition = new Vector3(0f, 0f, 1f);
			pickup.transform.parent = null;
			pickup.col.enabled = true;
			pickup.rb.AddForce(cam.transform.forward * force, ForceMode.Impulse);
			pickup = null;
			throwState = 0;
			currentThrow = 0f;
		}
	}
}
