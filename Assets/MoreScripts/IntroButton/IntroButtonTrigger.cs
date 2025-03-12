using UnityEngine;

public class IntroButtonTrigger : MonoBehaviour
{
    public BoxCollider buttonColl;
    public AudioClip introClip;
    public AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
            if (buttonColl != null)
            {
                if (other.CompareTag("Hands"))
                {

                    Debug.Log("Hands has entered me");
                    audioSource.PlayOneShot (introClip);
                    buttonColl.enabled = false;

                }
            }

    }
}
