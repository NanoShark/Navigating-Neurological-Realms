using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SwimmerDataRecoderComputer : MonoBehaviour
{
    public Swimmer swimmerComponent;
    public float dataRecordInterval = 0.1f; // Record data every 0.1 seconds
    public string computerSavePath = "C:\\Users\\micka\\Downloads\\data"; // Update with your desired path

    private float recordTimer = 0f;
    private List<SwimmerDataRecord> swimmerDataList = new List<SwimmerDataRecord>();
    private bool isRecorderInitialized = false;

    private void Start()
    {
        // Check if computerSavePath is set, otherwise use a default path
        if (string.IsNullOrEmpty(computerSavePath))
        {
            computerSavePath = Path.Combine(Application.dataPath, "SwimmerData");
        }

        // Check if all necessary components are assigned
        if (swimmerComponent == null)
        {
            Debug.LogError("Swimmer component is not assigned to ComputerDataRecorder.");
            return;
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
                CaptureSwimmerData();
                recordTimer = 0f;
            }
        }
        else
        {
            recordTimer = 0f;
        }
    }

    private void CaptureSwimmerData()
    {
        if (swimmerComponent == null) return;

        Rigidbody swimmerRigidbody = swimmerComponent.GetComponent<Rigidbody>();
        if (swimmerRigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on the Swimmer object.");
            return;
        }

        SwimmerDataRecord dataRecord = new SwimmerDataRecord
        {
            timestamp = Time.time,
            position = swimmerComponent.transform.position,
            rotation = swimmerComponent.transform.rotation,
            velocity = swimmerRigidbody.velocity
        };

        swimmerDataList.Add(dataRecord);
    }

    public void SaveDataToComputer()
    {
        if (swimmerDataList.Count == 0)
        {
            Debug.Log("No data to save.");
            return;
        }

        string fileName = "SwimmerData_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
        string filePath = Path.Combine(computerSavePath, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Timestamp,PositionX,PositionY,PositionZ,RotationX,RotationY,RotationZ,RotationW,VelocityX,VelocityY,VelocityZ");

                foreach (SwimmerDataRecord dataRecord in swimmerDataList)
                {
                    writer.WriteLine($"{dataRecord.timestamp},{dataRecord.position.x},{dataRecord.position.y},{dataRecord.position.z}," +
                                     $"{dataRecord.rotation.x},{dataRecord.rotation.y},{dataRecord.rotation.z},{dataRecord.rotation.w}," +
                                     $"{dataRecord.velocity.x},{dataRecord.velocity.y},{dataRecord.velocity.z}");
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
public class SwimmerDataRecord
{
    public float timestamp;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
}
