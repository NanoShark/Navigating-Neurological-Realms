using UnityEngine;

public class SubmarineAudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OVRCameraRig")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "OVRCameraRig")
        {
            audioSource.Stop();
        }
    }
}
