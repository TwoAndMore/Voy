using UnityEngine;

public static class GetTargetTransform
{
    public static Vector3 GetTransform(string itemName)
    {
        return itemName switch
        {
            "Lamp" => new Vector3(0.3f,1.5f,0.5f),
            "Gun" => new Vector3(0.42f,1.38f,0.52f),
            "Compass" => new Vector3(0.4f,1.52f,0.46f),
            "Mirror" => new Vector3(0.38f,1.42f,0.43f),
            _ => new Vector3(0f,0f,0f)
        };
    }
    
    public static Vector3 GetRotation(string itemName)
    {
        return itemName switch
        {
            "Lamp" => new Vector3(-3.25f,78f,-180f),
            "Gun" => new Vector3(95f,270f,0f),
            "Compass" => new Vector3(56f,222f,0f),
            "Mirror" => new Vector3(82f,331f,82f),
            _ => new Vector3(0f, 0f, 0f)
        };
    }
}
