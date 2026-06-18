using Unity.Cinemachine;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    public Transform ItemContainer;
    private GameObject currentItem;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void GrabItem(GameObject item)
    {
        GameManager.Instance.flashLight.Interact(transform);
        
        currentItem = item;
        item.transform.SetParent(ItemContainer);

        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;    
    }
}
