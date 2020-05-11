using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text Tittle;
    public GameObject Headers;
    public GameObject Data;
    Button Actualizar;

    void Start()
    {
        UpdateData();
    }
    

    public void ActualizarData()
    {
        StartCoroutine(UpdateCoroutine());       
    }

    IEnumerator UpdateCoroutine()
    {
        DeleteData();
        StartCoroutine( GameController.GetDataFromFile());
        UpdateData();
        yield return null;

    }
    private void UpdateData()
    {
        Tittle.text = GameController.tittle;
        SetTextProperties(Tittle, "Header");

        foreach (var item in GameController.headers)
        {
            GameObject newHeaderText = new GameObject(item, typeof(RectTransform));
            var headerText = newHeaderText.AddComponent<Text>();
            headerText.text = item;
            SetTextProperties(headerText, "Header");
            newHeaderText.transform.SetParent(Headers.transform);
        }

        int count = 1;
        foreach (var item in GameController.data)
        {
            GameObject newDataEmpty = new GameObject("Item " + count, typeof(RectTransform));
            newDataEmpty.transform.SetParent(Data.transform);

            RectTransform rectTransform = newDataEmpty.GetComponent<RectTransform>();

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Data.GetComponent<RectTransform>().rect.width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Data.GetComponent<RectTransform>().rect.height);

            newDataEmpty.AddComponent<HorizontalLayoutGroup>();

           
            foreach (var item2 in item)
            {
                GameObject newDataText = new GameObject(item2.Key, typeof(RectTransform));                
                var dataText = newDataText.AddComponent<Text>();
                dataText.text = item2.Value;
                SetTextProperties(dataText, "Data");
                dataText.transform.SetParent(newDataEmpty.transform);               
            }
            count++;

        }
    }

    private void DeleteData()
    {
        Tittle.text = string.Empty;
        foreach (Transform child in Headers.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameController.headers.Clear();

        foreach (Transform child in Data.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameController.data.Clear();
    }

    private void SetTextProperties(Text text, string type)
    {
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;
        switch (type)
        {
            case "Title":
                text.fontStyle = FontStyle.Bold;
                text.fontSize = 30;
                break;
            case "Header":
                text.fontStyle = FontStyle.Bold;
                text.fontSize = 26;
                break;
            case "Data":
                text.fontSize = 22;
                break;
            default:
                break;
        }
    }

}
