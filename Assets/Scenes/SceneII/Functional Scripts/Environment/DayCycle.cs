using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public enum Season { Spring, Summer, Fall, Winter }
    public enum Weather { Clear, Cloudy, Rainy, Snowy }

    public float dayLengthInSeconds = 300f;
    public float summerDayLengthMultiplier = 1.2f;
    public float winterDayLengthMultiplier = 0.8f;
    public float nightLengthMultiplier = 1.5f;

    public Light sun;
    public Gradient lightColorGradient;
    public AnimationCurve sunIntensityCurve;
    public float sunInitialIntensity = 1f;

    public Weather currentWeather = Weather.Clear;

    private float currentCycleTime = 0f;
    private float currentSeasonDayLength = 50f;

    void Start()
    {
        if (sun == null)
        {
            Debug.LogError("Sun light is not assigned.");
            enabled = false;
            return;
        }

        sun.intensity = sunInitialIntensity;
        UpdateSeason(Season.Spring);
    }

    void Update()
    {
        UpdateDayNightCycle();
    }

    void UpdateDayNightCycle()
    {
        currentCycleTime += Time.deltaTime;

        float currentTimeOfDay = currentCycleTime / currentSeasonDayLength;
        currentTimeOfDay %= 1f;

        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 0, 0);
        sun.intensity = sunIntensityCurve.Evaluate(currentTimeOfDay) * sunInitialIntensity;
        sun.color = lightColorGradient.Evaluate(currentTimeOfDay);

        UpdateWeather(currentTimeOfDay);
    }

    void UpdateSeason(Season currentSeason)
    {
        switch (currentSeason)
        {
            case Season.Spring:
                currentSeasonDayLength = dayLengthInSeconds;
                break;
            case Season.Summer:
                currentSeasonDayLength = dayLengthInSeconds * summerDayLengthMultiplier;
                break;
            case Season.Fall:
                currentSeasonDayLength = dayLengthInSeconds;
                currentWeather = Weather.Cloudy; // Fall is cloudy
                break;
            case Season.Winter:
                currentSeasonDayLength = dayLengthInSeconds * winterDayLengthMultiplier;
                currentWeather = Weather.Snowy; // Winter is snowy
                break;
            default:
                currentSeasonDayLength = dayLengthInSeconds;
                break;
        }
    }

    void UpdateWeather(float currentTimeOfDay)
    {
        switch (currentWeather)
        {
            case Weather.Clear:
                // Handle clear weather effects
                break;
            case Weather.Cloudy:
                // Handle cloudy weather effects
                break;
            case Weather.Rainy:
                // Handle rainy weather effects
                break;
            case Weather.Snowy:
                // Handle snowy weather effects
                break;
            default:
                // Default to clear weather effects
                break;
        }
    }
}
