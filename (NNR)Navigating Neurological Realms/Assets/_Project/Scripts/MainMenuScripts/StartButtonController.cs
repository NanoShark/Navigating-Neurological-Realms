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


/*
    Copyright (c) 2024 Michael Malka

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/