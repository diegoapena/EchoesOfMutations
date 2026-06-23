using UnityEngine;

[CreateAssetMenu(fileName = "BaseMaterialData", menuName = "EchoesOfMutations/BaseMaterialData")]
public class BaseMaterialData : ScriptableObject
{
    [SerializeField]private string materialName;
    public Sprite icon;

    public string MaterialName => materialName;
}
