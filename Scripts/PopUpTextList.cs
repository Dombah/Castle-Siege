using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextList : MonoBehaviour
{
    [Tooltip("Order in which the text is put is the order it will be desplayed")]
    [SerializeField] List<string> texts = new List<string>();
    PopUpManager popUp;
    int count = 0;

    private void Awake()
    {
        popUp = FindObjectOfType<PopUpManager>();
    }

    private void Update()
    {
        if (count > texts.Count - 1)
        {
            popUp.Close();
        }
        else
        {
            popUp.SetText(texts[count]);
        }     
    }
    public void IncreaseCount()
    {
        count++;
    }
}
