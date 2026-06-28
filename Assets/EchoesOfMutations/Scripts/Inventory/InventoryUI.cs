using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [FoldoutGroup("References")]
    [SerializeField] private GameObject slotPrefab;
    [FoldoutGroup("References")]
    [SerializeField] private Transform slotsContainer;

    [FoldoutGroup("Settings")]
    [SerializeField] private int maxSlots = 9;

    private List<InventorySlotUI> slots = new();
    private int selectedIndex = -1;
    private void Awake()
    {
        BuildSlots();
    }
    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += HandleInventoryChanged;
        InventoryManager.OnEquippedItem += HandleEquippedItem;       
    }

    

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= HandleInventoryChanged;
        InventoryManager.OnEquippedItem -= HandleEquippedItem;
    }
   
    private void BuildSlots()
    {
        foreach(Transform child in slotsContainer)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();

        for(int i = 0; i< maxSlots ; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsContainer);
            
            InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
            if(slotUI == null)
            {
                Debug.LogError("The slotPrefab is missing the InventorySlotUI component on slot :" + i);
                continue;
            }
            slotUI.ClearSlot();
            slotUI.SetSelected(false);
            slots.Add(slotUI);
        }        
    }
    private void HandleInventoryChanged(CircularLinkedList<IInteractable> inventory)
    {
        foreach(var slot in slots)
        {
            slot.ClearSlot();
        }

        if (inventory == null || inventory.Count == 0) return;

        Node<IInteractable> current = inventory.head;
        for (int i = 0; i < inventory.Count && i < slots.Count; i++) 
        { 
            Sprite icon = ItemIcon(current.Value);
            slots[i].SetItem(icon);
            current = current.Next;
        }
        RefreshSelection();
    }
    private void HandleEquippedItem(int index)
    {
        selectedIndex = index;
        RefreshSelection();
    }
    private void RefreshSelection()
    {
        for (int i = 0; i < slots.Count; i++) 
        {
            slots[i].SetSelected(i==selectedIndex);
        }
    }
    private Sprite ItemIcon(IInteractable interactable)
    {
        if (interactable == null) return null;

        if(interactable is BaseInteractableObj baseObj && baseObj.ItemData != null)
        {
            return baseObj.ItemData.ItemIcon;
        }

        if(interactable is BaseCraftable craftable && craftable.ItemData != null)
        {
            return craftable.ItemData.ItemIcon;
        }
        Debug.LogWarning("Make sure your ItemData ScriptableObject has an ItemIcon (Sprite) field");
        return null;
    }
}
