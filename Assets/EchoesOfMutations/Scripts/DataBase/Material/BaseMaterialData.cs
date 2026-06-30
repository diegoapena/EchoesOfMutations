using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseMaterialData", menuName = "EchoesOfMutations/BaseMaterialData")]
public class BaseMaterialData : ScriptableObject
{
    [FoldoutGroup("Settings")]
    [SerializeField]private string materialName;
    [FoldoutGroup("Settings"), PreviewField(150)]
    public Sprite icon;

    public string MaterialName => materialName;
}
