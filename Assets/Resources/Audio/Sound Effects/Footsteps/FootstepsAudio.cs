using UnityEngine;

public class FootstepsAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] soundClips;
    public float checkInterval = 1f;
    public float movementThreshold = 0.01f;

    private Vector3 lastPosition;
    private float timer;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, lastPosition) > movementThreshold)
        {
            timer += Time.deltaTime;

            if (timer >= checkInterval)
            {
                PlayRandomFootstep();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }

        lastPosition = transform.position;
    }

    void PlayRandomFootstep()
    {
        if (soundClips.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, soundClips.Length);
            audioSource.PlayOneShot(soundClips[randomIndex]);

            // Debug stuff
            Debug.Log("Playing sound: " + soundClips[randomIndex].name);
        }
    }
}
