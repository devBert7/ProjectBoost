using UnityEngine;

public class Rocket : MonoBehaviour {
	[SerializeField] float rcsThrust = 250f;
	[SerializeField] float mainThrust = 150f;

	Rigidbody rigidBody;
	AudioSource audioSource;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update() {
		Thrust();
		Rotate();
	}

	void Thrust() {
		float thrustThisFrame = mainThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.Space)) {
			rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
			if (!audioSource.isPlaying) {
					audioSource.Play();
			}
		} else {
			audioSource.Stop();
		}
	}

	void Rotate() {
		rigidBody.freezeRotation = true;
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.forward * rotationThisFrame);
		} else if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}

		rigidBody.freezeRotation = false;
	}
}
