using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMechanics : MonoBehaviour
{
    public Transform ItemContainer;
    private GameObject currentItem;
   




    void Update()
    {

    }

    public void PickUp(GameObject item)
    {
        GameManager.Instance.flashLight.Interact(transform);

        currentItem = item;
        item.transform.SetParent(ItemContainer);

        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    
}