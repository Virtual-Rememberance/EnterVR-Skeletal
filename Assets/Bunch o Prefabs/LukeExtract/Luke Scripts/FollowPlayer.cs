using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField]private Vector3 offset = new Vector3(0, 3, 0);

    private void Update()
    {
        gameObject.transform.position = player.transform.position + offset;
    }
}
