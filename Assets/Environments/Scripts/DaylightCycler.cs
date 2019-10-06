using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycler : MonoBehaviour
{

    [Header("Settings")]

    [SerializeField, Range(-180f, 0f), Tooltip("Z axis is north/south, X axis is east/west\n0 rises due south, -90 due east, -180 due north")] private float sunriseDegree;
    [SerializeField] private float timeInMinutes;
    [SerializeField, Tooltip("One day on Earth is 1,440 minutes long")] private float dayLengthInMinutes = 1440f;
    [SerializeField] private float daySpeedMultiplier = 1f;

    [Header("Links")]

    [SerializeField] private Light sun;



    private void Update()
    {
        float dt = Time.deltaTime;

        timeInMinutes += (dt / 60f) * daySpeedMultiplier;
        while (timeInMinutes > dayLengthInMinutes)
            timeInMinutes -= dayLengthInMinutes;

        SetSunEuler();
    }

    private void SetSunEuler()
    {
        float angle = Mathf.PI * 2f * (timeInMinutes / dayLengthInMinutes);
        float radius = sunriseDegree;
        if (radius < -90f)
        {
            radius += 180f;
            radius = -radius;
        }
        
        Vector3 euler = new Vector3();
        euler.x = -radius * Mathf.Sin(angle);
        euler.y = radius * Mathf.Cos(angle);
        if (sunriseDegree < -90f)
            euler.y = -180f - euler.y;
        
        sun.transform.eulerAngles = euler;

        //float angle = timeInMinutes / dayLengthInMinutes;

    }



    private void OnValidate()
    {
        timeInMinutes = Mathf.Clamp(timeInMinutes, 0f, dayLengthInMinutes);
        dayLengthInMinutes = Mathf.Clamp(dayLengthInMinutes, 0.01f, float.MaxValue);
        SetSunEuler();
    }

    public float GetSunriseDegree() { return sunriseDegree; }
    public void SetSunriseDegree(float sunriseDegree) { this.sunriseDegree = Mathf.Clamp(sunriseDegree, -90f, 90f); }

    public float GetTime() { return timeInMinutes; }
    public void SetTime(float timeInMinutes) { this.timeInMinutes = Mathf.Clamp(timeInMinutes, 0f, dayLengthInMinutes); }

    public float GetDayLength() { return dayLengthInMinutes; }
    public void SetDayLength(float dayLengthInMinutes) { this.dayLengthInMinutes = Mathf.Clamp(dayLengthInMinutes, 0f, float.MaxValue); }

    public float GetDaySpeedMultiplier() { return daySpeedMultiplier; }
    public void SetDaySpeedMultiplier(float daySpeedMultiplier) { this.daySpeedMultiplier = daySpeedMultiplier; }

}
