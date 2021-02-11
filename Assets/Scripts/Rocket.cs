using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
	[SerializeField] float rcsThrust = 250f;
	[SerializeField] float mainThrust = 10f;

	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip crash;
	[SerializeField] AudioClip success;

	[SerializeField] ParticleSystem thrustParticles;
	[SerializeField] ParticleSystem crashParticles;
	[SerializeField] ParticleSystem successParticles;

	Rigidbody rigidBody;
	AudioSource audioSource;

	enum State {Alive, Dying, Transcending};
	State state = State.Alive;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update() {
		if (state == State.Alive) {
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (state != State.Alive) {
			return;
		}

		switch (collision.gameObject.tag) {
			case "Friendly":
				print("Ok");
				break;

			case "Finish":
				StartSuccessSequence();
				break;
			
			default:
				StartDeathSequence();
				break;
		}
	}

	void StartSuccessSequence() {
		state = State.Transcending;
		audioSource.Stop();
		audioSource.PlayOneShot(success);
		successParticles.Play();
		Invoke("LoadNextScene", 1f);
	}

	void StartDeathSequence() {
		state = State.Dying;
		audioSource.Stop();
		thrustParticles.Stop();
		audioSource.PlayOneShot(crash);
		crashParticles.Play();
		Invoke("GameOver", 1f);
	}

	void LoadNextScene() {
		SceneManager.LoadScene(1);
	}

	void GameOver() {
		SceneManager.LoadScene(0);
	}

	void RespondToThrustInput() {
		if (Input.GetKey(KeyCode.Space)) {
			ApplyThrust();
		} else {
			audioSource.Stop();
			thrustParticles.Stop();
		}
	}

	void ApplyThrust() {
		rigidBody.AddRelativeForce(Vector3.up * mainThrust);

		if (!audioSource.isPlaying) {
				audioSource.PlayOneShot(mainEngine);
		}

		thrustParticles.Play();
	}

	void RespondToRotateInput() {
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
