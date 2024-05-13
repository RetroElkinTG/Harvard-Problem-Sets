using UnityEngine;

// Item values
[CreateAssetMenu]
public class ItemValues : ScriptableObject
{
    [Header("Item Variables")]
    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;
}