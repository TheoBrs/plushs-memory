using UnityEngine;

public class ToolBox : MonoBehaviour
{
    public static Transform GetChildWithTag(Transform transform, string tag, bool recurcive = false)
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
            else if (recurcive)
            {
                var temp = GetChildWithTag(child.transform, tag);
                if (temp)
                    return temp;
            }
        }
        return null;
    }
}

public static class IsWin
{
    public static bool IsWinBool { get; set; }
}