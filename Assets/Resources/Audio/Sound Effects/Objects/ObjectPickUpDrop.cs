using UnityEngine;

public class ObjectPickUpDrop : MonoBehaviour
{
    public enum ObjectType { Light, Heavy }
    public ObjectType objectType;
    public string objectName;

    public AudioClip[] lightPickUpSounds;
    public AudioClip[] lightDropSounds;
    public AudioClip[] heavyPickUpSounds;
    public AudioClip[] heavyDropSounds;

    public AudioSource audioSource;

    private Collider triggerCollider;
    private Collider physicalCollider;

    private void Awake()
    {
        lightPickUpSounds = new AudioClip[]
        {
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightPickUp1"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightPickUp2"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightPickUp3"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightPickUp4"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightPickUp5")
        };

        lightDropSounds = new AudioClip[]
        {
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightDrop1"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightDrop2"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightDrop3"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightDrop4"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/LightDrop5")
        };

        heavyPickUpSounds = new AudioClip[]
        {
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyPickUp1"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyPickUp2"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyPickUp3"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyPickUp4"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyPickUp5")
        };

        heavyDropSounds = new AudioClip[]
        {
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyDrop1"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyDrop2"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyDrop3"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyDrop4"),
            Resources.Load<AudioClip>("Audio/Sound Effects/Objects/HeavyDrop5")
        };

        // Assign colliders
        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            if (collider.isTrigger)
                triggerCollider = collider;
            else
                physicalCollider = collider;
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            if (objectType == ObjectType.Light)
                PlayRandomSound(lightPickUpSounds);
            else if (objectType == ObjectType.Heavy)
                PlayRandomSound(heavyPickUpSounds);
            if (triggerCollider != null) triggerCollider.enabled = false;
            if (physicalCollider != null) physicalCollider.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            if (triggerCollider != null) triggerCollider.enabled = false;
            if (physicalCollider != null) physicalCollider.enabled = true;

            Debug.Log(objectName + " released from hands, collider updated.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // makes sure the object stays as a physical collider after landing on the floor to prevent it falling through the floor
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if (objectType == ObjectType.Light)
                PlayRandomSound(lightDropSounds);
            else if (objectType == ObjectType.Heavy)
                PlayRandomSound(heavyDropSounds);

            if (triggerCollider != null) triggerCollider.enabled = false;  
            if (physicalCollider != null) physicalCollider.enabled = true;  
        }
    }

    private void PlayRandomSound(AudioClip[] soundClips)
    {
        if (soundClips.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, soundClips.Length);
            if (soundClips[randomIndex] != null)
                audioSource.PlayOneShot(soundClips[randomIndex]);
            else
                Debug.LogWarning("AudioClip missing at index " + randomIndex);
        }
        else
        {
            Debug.LogWarning("Missing sound clips or audio source.");
        }
    }
}
