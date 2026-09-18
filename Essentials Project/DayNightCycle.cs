using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("Length of a full day in seconds")]
    public float dayDuration = 120f; // Default: 2 minutes per day

    private float rotationSpeed;

    void Start()
    {
        // Calculate degrees per second (360 degrees = full day)
        rotationSpeed = 360f / dayDuration;
    }

    void Update()
    {
        // Rotate around the X axis to simulate sun movement
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}