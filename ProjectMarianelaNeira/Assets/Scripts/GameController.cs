using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GameController : MonoBehaviour
{
    public static string fimeName = "JsonChallenge.json";
    public static string tittle = string.Empty;
    public static List<string> headers = new List<string>();
    public static List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
    static JObject json;

    private void Awake()
    {
       StartCoroutine(GetDataFromFile());
    }
   
    public static IEnumerator GetDataFromFile()
    {
        GetJsonFile();
        GetTitle(json);
        GetHeaders(json);
        GetData(json);
        yield return null;
    }


    #region GetData
    private static void GetJsonFile()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fimeName);
        var result = System.IO.File.ReadAllText(filePath);
        json = JObject.Parse(result);       
    }

    private static void GetTitle(JObject json)
    {
        tittle = json["Title"].ToString();
    }

    private static void GetHeaders(JObject json)
    {
        headers.Clear();
        foreach (var item in json["ColumnHeaders"])
        {
            headers.Add(item.ToString());
        }
    }

    private static void GetData(JObject json)
    {
        foreach (var item in json["Data"])
        {
            JObject json2 = JObject.Parse(item.ToString());
            Dictionary<string, string> value = new Dictionary<string, string>();

            foreach (var i in json2)
            {
                value.Add(i.Key, i.Value.ToString());
            }

            data.Add(value);
        }
    }

    #endregion
}
