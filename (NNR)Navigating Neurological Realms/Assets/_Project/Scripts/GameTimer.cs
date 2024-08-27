using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement; // Required to manage scenes

public class GameTimer : MonoBehaviour
{
    private bool timerIsActive; // Tracks whether the timer is active
    private float currentTimeRemaining; // The current time left on the timer
    [SerializeField] private float initialTimeInSeconds = 300f; // Initial time in seconds (e.g., 300 seconds for 5 minutes)
    [SerializeField] private TMP_Text timerDisplayText; // Reference to the TextMeshPro component for displaying the time
    [SerializeField] private GameObject endGameCanvas; // Reference to the Canvas that will be shown when the timer ends
    [SerializeField] private string sceneToLoad; // Name of the scene to load when the A button is pressed

    // Start is called before the first frame update
    void Start()
    {
        currentTimeRemaining = initialTimeInSeconds; // Initialize the timer with the start time in seconds
        UpdateTimerDisplay(); // Update the text to show the initial time
        endGameCanvas.SetActive(false); // Ensure the end game canvas is initially hidden
    }

    // Update is called once per frame
    void Update()
    {
        // Check if both primary hand triggers are pressed
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f &&
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5f)
        {
            StartTimer(); // Start the timer
        }

        // Update the timer if active
        if (timerIsActive)
        {
            currentTimeRemaining -= Time.deltaTime; // Decrement the timer by the time passed since the last frame
            if (currentTimeRemaining <= 0)
            {
                timerIsActive = false; // Stop the timer when time runs out
                currentTimeRemaining = 0; // Ensure the timer doesn't go negative
                EndGame(); // Stop the game and show the canvas
            }
            UpdateTimerDisplay(); // Update the text to show the current time
        }

        // Check if the A button is pressed
        if (OVRInput.GetDown(OVRInput.Button.One) && currentTimeRemaining <= 0)
        {
            LoadNextScene(); // Load the next scene
        }
    }

    private void UpdateTimerDisplay()
    {
        // Convert the current time to a TimeSpan to format it as minutes and seconds
        TimeSpan time = TimeSpan.FromSeconds(currentTimeRemaining);
        // Update the text with the formatted time (MM:SS)
        timerDisplayText.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
    }

    public void StartTimer()
    {
        if (!timerIsActive) // Start the timer only if it's not already active
        {
            timerIsActive = true;
        }
    }

    public void StopTimer()
    {
        timerIsActive = false; // Deactivate the timer
    }

    public void ResetTimer()
    {
        currentTimeRemaining = initialTimeInSeconds; // Reset the timer to the initial start time in seconds
        UpdateTimerDisplay(); // Update the text to show the reset time
    }

    private void EndGame()
    {
        Time.timeScale = 0; // Stop the game
        endGameCanvas.SetActive(true); // Show the end game canvas
    }

    private void LoadNextScene()
    {
        // Ensure you have set the scene name in the Unity Inspector
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
        }
        else
        {
            Debug.LogError("Scene name to load is not set!");
        }
    }
}
