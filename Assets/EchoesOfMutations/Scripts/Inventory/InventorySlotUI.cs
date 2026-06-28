using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [FoldoutGroup("References")]
    [SerializeField] private Image itemIcon;
    [FoldoutGroup("References")]
    [SerializeField] private Image slotBackground;
    [FoldoutGroup("References")]
    [SerializeField] private Image selectHighlight;

    [FoldoutGroup("Colors")]
    [SerializeField] private Color normalColor = new (0.15f, 0.15f, 0.15f, 0.85f);
    [FoldoutGroup("Colors")]
    [SerializeField] private Color selectedColor = new (0.90f, 0.75f, 0.20f, 1f);
    [FoldoutGroup("Colors")]
    [SerializeField] private Color emptyIconColor = new(1f, 1f, 1f, 0f);
    [FoldoutGroup("Colors")]
    [SerializeField] private Color filledIconColor = new(1f, 1f, 1f, 1f);

    [FoldoutGroup("States")]
    [SerializeField] private bool isSelected;
    [FoldoutGroup("States")]
    [SerializeField] private bool hasItem;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetItem(Sprite icon)
    {
        hasItem = icon != null;
        if(itemIcon != null)
        {
            itemIcon.sprite = icon;
            itemIcon.color = hasItem ? filledIconColor : emptyIconColor;
        }
    }

    public void ClearSlot()
    {
        hasItem = false;
        if(itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.color = emptyIconColor;
        }
    }
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if(slotBackground != null)
        {
            slotBackground.color = isSelected ? selectedColor : normalColor;
        }
        if(selectHighlight != null)
        {
            selectHighlight.gameObject.SetActive(isSelected);
        }
    }
}
