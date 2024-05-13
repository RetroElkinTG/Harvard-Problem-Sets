using System.Collections.Generic;
using UnityEngine;

// Inventory values
[CreateAssetMenu]
public class InventoryValues : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Inventory Variables")]
    public ItemValues currentItem;
    public List<ItemValues> items = new List<ItemValues>();
    [HideInInspector]
    public int runtimeKeyCount;
    public int defaultKeyCount;

    // Set inventory values for scriptable objects; called when program is deserialized
    public void OnAfterDeserialize()
    {
        runtimeKeyCount = defaultKeyCount;
    }

    // Required for above method; called when program is serialized
    public void OnBeforeSerialize()
    {

    }

    // Add item to inventory
    public void AddItem(ItemValues itemToAdd)
    {
        if (itemToAdd.isKey)
        {
            runtimeKeyCount++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            { 
                items.Add(itemToAdd);
            }
        }
    }
}