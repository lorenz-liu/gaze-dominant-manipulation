using UnityEngine;

public static class LogHelper
{
    public static void Success(object info)
    {
        Debug.Log("SUCCESS: " + info);
    }

    public static void Failure(object info)
    {
        Debug.Log("Failure: " + info);
    }
    public static void Normal(object info)
    {
        Debug.Log("Normal: " + info);
    }
}