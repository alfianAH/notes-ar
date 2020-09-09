using System;
using UnityEngine;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    [SerializeField] private InputField inputTitle,
        inputBody;

    private string titleText, bodyText;
    
    public void SaveNote()
    {
        if(!string.IsNullOrWhiteSpace(inputTitle.text) && 
           !string.IsNullOrWhiteSpace(inputBody.text))
        {
            titleText = inputTitle.text;
            bodyText = inputBody.text;
            Debug.Log("Title: " + titleText);
            Debug.Log("Body: " + bodyText);
        }
    }
}
