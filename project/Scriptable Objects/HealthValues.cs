using UnityEngine;

// Health values
[CreateAssetMenu]
public class HealthValues : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Health Variables")]
    [HideInInspector]
    public float runtimeValue;
    public float defaultValue;

    // Set health values for scriptable objects; called when program is deserialized
    public void OnAfterDeserialize()
    {
        runtimeValue = defaultValue;
    }

    // Required for above method; called when program is serialized
    public void OnBeforeSerialize() 
    { 

    }
}