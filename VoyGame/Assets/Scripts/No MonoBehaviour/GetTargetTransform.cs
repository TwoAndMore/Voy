using UnityEngine;

public static class GetTargetTransform
{
    public static Vector3 GetTransform(string itemName)
    {
        return itemName switch
        {
            "Lamp" => new Vector3(),
            "Gun" => new Vector3(),
            "Compass" => new Vector3(0.4f,1.52f,0.46f),
            "Mirror" => new Vector3(),
            _ => new Vector3(0f,0f,0f)
        };
    }
    
    public static Vector3 GetRotation(string itemName)
    {
        return itemName switch
        {
            "Lamp" => new Vector3(),
            "Gun" => new Vector3(),
            "Compass" => new Vector3(56f,222f,0f),
            "Mirror" => new Vector3(),
            _ => new Vector3(0f, 0f, 0f)
        };
    }
}
