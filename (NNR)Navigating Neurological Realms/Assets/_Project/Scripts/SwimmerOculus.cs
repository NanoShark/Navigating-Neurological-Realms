using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Swimmer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1f;
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;

    [Header("References")]
    [SerializeField] Transform trackingReference;

    [Header("Audio")]
    [SerializeField] AudioClip swimmingSound;
    [SerializeField] float volumeScale = 0.5f;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private float _cooldownTimer;
    private bool _isSwimming = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody.useGravity = false;  // Give us the water illusion
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        // Set up the AudioSource
        _audioSource.clip = swimmingSound;
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
        _audioSource.volume = volumeScale;
    }

    private void FixedUpdate()
    {
        _cooldownTimer += Time.fixedDeltaTime;

        bool isSwimmingThisFrame = false;

        // Check if hand triggers are pressed
        bool leftTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        //Debug.Log("Left Trigger Pressed: " + leftTriggerPressed);
        //Debug.Log("Right Trigger Pressed: " + rightTriggerPressed);

        if (_cooldownTimer > minTimeBetweenStrokes && leftTriggerPressed && rightTriggerPressed)
        {
            Vector3 leftHandVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
            Vector3 rightHandVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;

            //Debug.Log("Left Hand Velocity: " + leftHandVelocity);
            //Debug.Log("Right Hand Velocity: " + rightHandVelocity);
            //Debug.Log("Combined Local Velocity: " + localVelocity);

            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldownTimer = 0f;
                isSwimmingThisFrame = true;

                //Debug.Log("Applied Force: " + (worldVelocity * swimForce));
                //Debug.Log("Cooldown Timer Reset");
            }
        }

        // Apply drag force
        if (_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
            //Debug.Log("Applied Drag Force: " + (-_rigidbody.velocity * dragForce));
        }

        //Debug.Log("Rigidbody Velocity: " + _rigidbody.velocity);

        // Handle swimming sound
        if (isSwimmingThisFrame && !_isSwimming)
        {
            _audioSource.Play();
            _isSwimming = true;
            //Debug.Log("Swimming Sound Played");
        }
        else if (!isSwimmingThisFrame && _isSwimming)
        {
            _audioSource.Stop();
            _isSwimming = false;
            //Debug.Log("Swimming Sound Stopped");
        }
    }
}
