using Sirenix.OdinInspector;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "BaseItemsData", menuName = "EchoesOfMutations/BaseItemsData")]
[InlineEditor]
public class BaseItemsData : ScriptableObject
{
    [FoldoutGroup("Settings")]
    [SerializeField] private int id;
    [FoldoutGroup("Settings")]
    [SerializeField] private string itemName;
    [FoldoutGroup("Settings")]
    [SerializeField] private bool isStackable = false;
    [FoldoutGroup("Settings")]
    [SerializeField]private int maxStack = 1;
    [FoldoutGroup("Settings"), PreviewField(150)]
    public Sprite ItemIcon;
    [FoldoutGroup("Settings"), TextArea(2,10)]
    [SerializeField] private string itemDescription;
    [SerializeField] private ItemsTypes itemType;


    public int Id => id;
    public string ItemName => itemName;   
    public bool IsStackable => isStackable;
    public int MaxStack => maxStack;
    public string ItemDescription => itemDescription;
    public ItemsTypes ItemType => itemType;
}
