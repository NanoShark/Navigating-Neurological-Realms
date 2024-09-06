using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class OculusDataRecorder : MonoBehaviour
{
    public float recordInterval = 0.1f; // Record data every 0.1 seconds
    public string savePath; // Path where the CSV will be saved
    public static string playerName; // Default player name, can be set from another script

    private float timer = 0f;
    private List<ControllerData> dataList = new List<ControllerData>();
    private bool isInitialized = false;

    private float handCloseThreshold = 0.1f; // Set the threshold for how close hands should be
    private int handCloseCount = 0; // Counter for how many times hands are close

    private void Start()
    {
        // Set the save path to the persistent data path on the Oculus Quest 2
        savePath = Path.Combine(Application.persistentDataPath, "ControllerData");

        isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized) return;

        // Use Oculus-specific input system to check if triggers are pressed
        bool leftPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (leftPressed && rightPressed)
        {
            timer += Time.deltaTime;

            if (timer >= recordInterval)
            {
                RecordData();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void RecordData()
    {
        // Record the positions, rotations, and velocities of the left and right controllers
        Vector3 leftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 rightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        // Check if hands are close together
        float handDistance = Vector3.Distance(leftPosition, rightPosition);
        if (handDistance <= handCloseThreshold)
        {
            handCloseCount++;
            //Debug.Log("Hands are close! Count: " + handCloseCount);
        }

        // Continue recording other data
        Vector3 leftVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        Quaternion leftRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        Vector3 rightVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        Quaternion rightRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        ControllerData data = new ControllerData
        {
            timestamp = Time.time,
            leftPosition = leftPosition,
            leftRotation = leftRotation,
            leftVelocity = leftVelocity,
            rightPosition = rightPosition,
            rightRotation = rightRotation,
            rightVelocity = rightVelocity
        };

        dataList.Add(data);
    }

    public void SaveDataToFile()
    {
        if (dataList.Count == 0)
        {
            Debug.Log("No data to save.");
            return;
        }

        playerName = StartButtonController.playerName;

        string formattedTime = System.DateTime.Now.ToString("yyyyMMdd_HHmm");
        string fileName = $"{playerName}_{formattedTime}.csv";
        string filePath = Path.Combine(savePath, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Timestamp,LeftPosX,LeftPosY,LeftPosZ,LeftRotX,LeftRotY,LeftRotZ,LeftRotW,LeftVelX,LeftVelY,LeftVelZ," +
                                 "RightPosX,RightPosY,RightPosZ,RightRotX,RightRotY,RightRotZ,RightRotW,RightVelX,RightVelY,RightVelZ");

                foreach (ControllerData data in dataList)
                {
                    writer.WriteLine($"{data.timestamp},{data.leftPosition.x},{data.leftPosition.y},{data.leftPosition.z}," +
                                     $"{data.leftRotation.x},{data.leftRotation.y},{data.leftRotation.z},{data.leftRotation.w}," +
                                     $"{data.leftVelocity.x},{data.leftVelocity.y},{data.leftVelocity.z}," +
                                     $"{data.rightPosition.x},{data.rightPosition.y},{data.rightPosition.z}," +
                                     $"{data.rightRotation.x},{data.rightRotation.y},{data.rightRotation.z},{data.rightRotation.w}," +
                                     $"{data.rightVelocity.x},{data.rightVelocity.y},{data.rightVelocity.z}");
                }
            }

            Debug.Log("Data saved to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving data: {e.Message}");
        }
    }

    private void OnDisable()
    {
        if (isInitialized)
        {
            SaveDataToFile();
        }
    }
}

[System.Serializable]
public class ControllerData
{
    public float timestamp;
    public Vector3 leftPosition;
    public Quaternion leftRotation;
    public Vector3 leftVelocity;
    public Vector3 rightPosition;
    public Quaternion rightRotation;
    public Vector3 rightVelocity;
}
