using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Swimmer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1f;
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;
    [SerializeField] float hapticFrequency = 0.5f;  // Frequency for the haptic feedback
    [SerializeField] float hapticAmplitude = 0.8f;  // Amplitude for the haptic feedback
    [SerializeField] float hapticDuration = 0.1f;   // Duration of the haptic feedback

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

        if (_cooldownTimer > minTimeBetweenStrokes && leftTriggerPressed && rightTriggerPressed)
        {
            Vector3 leftHandVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
            Vector3 rightHandVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;

            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldownTimer = 0f;
                isSwimmingThisFrame = true;

                // Trigger haptic feedback
                OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.LTouch);
                OVRInput.SetControllerVibration(hapticFrequency, hapticAmplitude, OVRInput.Controller.RTouch);

                // Stop haptic feedback after the duration
                Invoke(nameof(StopHaptics), hapticDuration);
            }
        }

        // Apply drag force
        if (_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
        }

        // Handle swimming sound
        if (isSwimmingThisFrame && !_isSwimming)
        {
            _audioSource.Play();
            _isSwimming = true;
        }
        else if (!isSwimmingThisFrame && _isSwimming)
        {
            _audioSource.Stop();
            _isSwimming = false;
        }
    }

    private void StopHaptics()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
