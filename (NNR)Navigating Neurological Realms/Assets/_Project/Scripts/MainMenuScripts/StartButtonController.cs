using UnityEngine.UI;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private InputField nameInputField;

    public static string playerName;  // Static variable to hold the player's name

    private void Start()
    {
        startButton.interactable = false;
        nameInputField.onValueChanged.AddListener(OnNameInputChanged);
    }

    private void OnNameInputChanged(string name)
    {
        playerName = name;  // Assign the entered name to the static variable
        startButton.interactable = !string.IsNullOrEmpty(playerName);
    }
}
