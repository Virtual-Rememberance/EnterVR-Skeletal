using UnityEngine;

public class Phase1Trigger : MonoBehaviour
{
    public GameObject[] itemsToToggle;
    private bool hasTriggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player")) 
        {
            hasTriggered = true; 

            foreach (GameObject item in itemsToToggle)
            {
                if (item != null)
                {
                    item.SetActive(!item.activeSelf);
                }
            }
        }
    }
}


