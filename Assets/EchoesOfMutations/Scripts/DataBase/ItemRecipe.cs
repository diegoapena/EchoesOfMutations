using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Ingredient
{
    public BaseMaterialData material;
    public int amount;
}
[CreateAssetMenu(fileName = "ItemRecipe", menuName = "EchoesOfMutations/ItemRecipe")]

public class ItemRecipe : ScriptableObject
{
    [SerializeField] private string itemName;
    public GameObject resultPrefab; //-> any craftable item
    [SerializeField] private List<Ingredient> ingredients;

    public string ItemName => itemName;
    public List<Ingredient> Ingredients => ingredients;
}
