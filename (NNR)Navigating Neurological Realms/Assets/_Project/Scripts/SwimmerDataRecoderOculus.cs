using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class SwimmerDataRecoderOculus : MonoBehaviour
{
    public Swimmer swimmer;
    public float recordInterval = 0.1f; // Record data every 0.1 seconds
    public string savePath; // Path where the CSV will be saved

    private float timer = 0f;
    private List<SwimmerData> dataList = new List<SwimmerData>();
    private bool isInitialized = false;

    private void Start()
    {
        // Set the save path to the persistent data path on the Oculus Quest 2
        savePath = Path.Combine(Application.persistentDataPath, "SwimmerData");

        // Check if all necessary components are assigned
        if (swimmer == null)
        {
            Debug.LogError("Swimmer component is not assigned to SwimmerDataRecorder.");
            return;
        }

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
        if (swimmer == null) return;

        Rigidbody rb = swimmer.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the Swimmer object.");
            return;
        }

        SwimmerData data = new SwimmerData
        {
            timestamp = Time.time,
            position = swimmer.transform.position,
            rotation = swimmer.transform.rotation,
            velocity = rb.velocity
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

        string fileName = "SwimmerData_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
        string filePath = Path.Combine(savePath, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Timestamp,PositionX,PositionY,PositionZ,RotationX,RotationY,RotationZ,RotationW,VelocityX,VelocityY,VelocityZ");

                foreach (SwimmerData data in dataList)
                {
                    writer.WriteLine($"{data.timestamp},{data.position.x},{data.position.y},{data.position.z}," +
                                     $"{data.rotation.x},{data.rotation.y},{data.rotation.z},{data.rotation.w}," +
                                     $"{data.velocity.x},{data.velocity.y},{data.velocity.z}");
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
public class SwimmerData
{
    public float timestamp;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
}
