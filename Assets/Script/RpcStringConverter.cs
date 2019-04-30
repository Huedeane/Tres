using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_ObjectType { Player, Card }
public static class RpcStringConverter <T>
{
    public static string ConvertString(List<T> genericParameter)
    {
        string convertedString = "";
        foreach (T t in genericParameter)
        {
            convertedString += t.ToString() + ',';
        }
        convertedString.TrimEnd(',');
        return convertedString;
    }
}
