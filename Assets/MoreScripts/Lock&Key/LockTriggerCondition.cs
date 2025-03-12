using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LockTriggerCondition : MonoBehaviour
{
    public InteractionLayerMask keyLayerMask;
    public InteractionLayerMask notKeyLayerMask;
    public string Tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
          other.GetComponent<XRGrabInteractable>().interactionLayers = keyLayerMask;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            other.GetComponent<XRGrabInteractable>().interactionLayers = notKeyLayerMask;
        }
    }
}