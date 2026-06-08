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
    [FoldoutGroup("Settings"), PreviewField(150)]
    public GameObject itemPrefab;
    [FoldoutGroup("Settings"), PreviewField(150)]
    public Sprite itemIcon;
    [FoldoutGroup("Settings"), TextArea(2,10)]
    [SerializeField] private string itemDescription;    

    public string ItemName => itemName;
    public int Id => id;   
    public string ItemDescription => itemDescription;
}
