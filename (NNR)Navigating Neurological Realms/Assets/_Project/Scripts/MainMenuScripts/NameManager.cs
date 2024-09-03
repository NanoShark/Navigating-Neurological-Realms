using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    public static string PlayerName {  get; private set; }

    public void OnNameEntered() 
    {
        PlayerName = nameInputField.text;
    }
}
