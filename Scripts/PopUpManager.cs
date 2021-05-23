using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject popUpBox;
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text popUpMessage;

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpMessage.text = text;
        animator.SetTrigger("Pop");
    }

    public void SetText(string text)
    {
        popUpMessage.text = text;
    }

    public void Close()
    {
        animator.SetTrigger("Close");
    }
}
