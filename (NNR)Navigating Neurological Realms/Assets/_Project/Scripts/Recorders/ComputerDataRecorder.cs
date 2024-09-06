using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ComputerDataRecorder : MonoBehaviour
{
    public float dataRecordInterval = 0.1f; // Record data every 0.1 seconds
    public string computerSavePath = "C:\\Users\\micka\\Downloads\\data"; // Update with your desired path
    public static string playerName; // Default player name, can be set from another script

    private float recordTimer = 0f;
    private List<ControllerDataRecord> controllerDataList = new List<ControllerDataRecord>();
    private bool isRecorderInitialized = false;

    private float handCloseThreshold = 0.1f; // Set the threshold for how close hands should be (10 CM)
    private int handCloseCount = 0; // Counter for how many times hands are close

    private void Start()
    {
        // Check if computerSavePath is set, otherwise use a default path
        if (string.IsNullOrEmpty(computerSavePath))
        {
            computerSavePath = Path.Combine(Application.dataPath, "ControllerData");
        }

        isRecorderInitialized = true;
    }

    private void Update()
    {
        if (!isRecorderInitialized) return;

        // Use Oculus-specific input system to check if triggers are pressed
        bool isLeftTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool isRightTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (isLeftTriggerPressed && isRightTriggerPressed)
        {
            recordTimer += Time.deltaTime;

            if (recordTimer >= dataRecordInterval)
            {
                CaptureControllerData();
                recordTimer = 0f;
            }
        }
        else
        {
            recordTimer = 0f;
        }
    }

    private void CaptureControllerData()
    {
        // Record the positions, rotations, and velocities of the left and right controllers
        Vector3 leftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Quaternion leftRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        Vector3 leftVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);

        Vector3 rightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Quaternion rightRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        Vector3 rightVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

        // Check if hands are close together
        float handDistance = Vector3.Distance(leftPosition, rightPosition);
        if (handDistance <= handCloseThreshold)
        {
            handCloseCount++;
            Debug.Log("Hands are close! Count: " + handCloseCount);
        }

        // Continue recording other data
        ControllerDataRecord dataRecord = new ControllerDataRecord
        {
            timestamp = Time.time,
            leftPosition = leftPosition,
            leftRotation = leftRotation,
            leftVelocity = leftVelocity,
            rightPosition = rightPosition,
            rightRotation = rightRotation,
            rightVelocity = rightVelocity
        };

        controllerDataList.Add(dataRecord);
    }

    public void SaveDataToComputer()
    {
        if (controllerDataList.Count == 0)
        {
            Debug.Log("No data to save.");
            return;
        }

        playerName = StartButtonController.playerName;

        string formattedTime = System.DateTime.Now.ToString("yyyyMMdd_HHmm");
        string fileName = $"{playerName}_{formattedTime}.csv";
        string filePath = Path.Combine(computerSavePath, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Timestamp,LeftPosX,LeftPosY,LeftPosZ,LeftRotX,LeftRotY,LeftRotZ,LeftRotW,LeftVelX,LeftVelY,LeftVelZ," +
                                 "RightPosX,RightPosY,RightPosZ,RightRotX,RightRotY,RightRotZ,RightRotW,RightVelX,RightVelY,RightVelZ");

                foreach (ControllerDataRecord dataRecord in controllerDataList)
                {
                    writer.WriteLine($"{dataRecord.timestamp},{dataRecord.leftPosition.x},{dataRecord.leftPosition.y},{dataRecord.leftPosition.z}," +
                                     $"{dataRecord.leftRotation.x},{dataRecord.leftRotation.y},{dataRecord.leftRotation.z},{dataRecord.leftRotation.w}," +
                                     $"{dataRecord.leftVelocity.x},{dataRecord.leftVelocity.y},{dataRecord.leftVelocity.z}," +
                                     $"{dataRecord.rightPosition.x},{dataRecord.rightPosition.y},{dataRecord.rightPosition.z}," +
                                     $"{dataRecord.rightRotation.x},{dataRecord.rightRotation.y},{dataRecord.rightRotation.z},{dataRecord.rightRotation.w}," +
                                     $"{dataRecord.rightVelocity.x},{dataRecord.rightVelocity.y},{dataRecord.rightVelocity.z}");
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
        if (isRecorderInitialized)
        {
            SaveDataToComputer();
        }
    }
}

[System.Serializable]
public class ControllerDataRecord
{
    public float timestamp;
    public Vector3 leftPosition;
    public Quaternion leftRotation;
    public Vector3 leftVelocity;
    public Vector3 rightPosition;
    public Quaternion rightRotation;
    public Vector3 rightVelocity;
}
