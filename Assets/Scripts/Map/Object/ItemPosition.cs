using UnityEngine;

public class ItemPosition : MonoBehaviour
{
    public string _positionKey = "";

    private void OnTriggerEnter(Collider other)
    {
        InteractionableItem item = other.gameObject.transform.GetComponent<InteractionableItem>();
        if(item != null)
        {
            item.ItemPositionKey = _positionKey;
        }
    }
}
