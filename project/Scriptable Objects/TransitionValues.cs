using UnityEngine;

// Scene transition values
[CreateAssetMenu]
public class TransitionValues : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Player Variables")]
    [HideInInspector]
    public Vector2 runtimePlayerPosition;
    public Vector2 defaultPlayerPosition;

    [Header("Camera Variables")]
    [HideInInspector]
    public Vector2 runtimeCameraMinPosition;
    public Vector2 defaultCameraMinPosition;
    [HideInInspector]
    public Vector2 runtimeCameraMaxPosition;
    public Vector2 defaultCameraMaxPosition;

    // Set scene transition values for scriptable objects; called when program is deserialized
    public void OnAfterDeserialize()
    { 
        runtimePlayerPosition = defaultPlayerPosition;
        runtimeCameraMinPosition = defaultCameraMinPosition;
        runtimeCameraMaxPosition = defaultCameraMaxPosition;
    }

    // Required for above method; called when program is serialized
    public void OnBeforeSerialize ()
    {

    }
}