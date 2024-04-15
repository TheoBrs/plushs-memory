using UnityEngine;

public class ToolBox
{
    public static Transform GetChildWithTag(Transform transform, string tag)
    {
        if (transform.childCount == 0)
        {
            return null;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag(tag))
            {
                return child;
            }
        }
        return null;
    }
}

public static class IsWin
{
    public static bool IsWinBool { get; set; }
}