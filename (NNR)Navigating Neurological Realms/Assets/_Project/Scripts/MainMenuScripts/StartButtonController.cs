using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButtonController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_InputField nameInputField;

    private void Start()
    {
        // Initially disable the start button
        startButton.interactable = false;

        // Add a listener to the input field to call the method when the text changes
        nameInputField.onValueChanged.AddListener(OnNameInputChanged);
    }

    private void OnNameInputChanged(string playerName)
    {
        // Enable the start button only if the input field is not empty
        startButton.interactable = !string.IsNullOrEmpty(playerName);
    }
}
