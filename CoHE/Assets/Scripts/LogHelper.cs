using UnityEngine;

public static class LogHelper
{
    public static void Success(object info)
    {
        Debug.Log("SUCCESS: " + info);
    }

    public static void Failure(object info)
    {
        Debug.LogError("FAILURE: " + info);
    }
}