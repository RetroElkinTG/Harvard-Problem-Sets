using UnityEngine;
using UnityEngine.UI;

// Sound Behaviour
public class SoundManager : MonoBehaviour
{
    [Header("Sound Variables")]
    public Slider volumeSlider;
    private string musicVolume = "musicVolume";
    private float maxVolume = 1;

    // Get sound components
    void Start()
    {
        if (!PlayerPrefs.HasKey(musicVolume))
        {
            PlayerPrefs.SetFloat(musicVolume, maxVolume);
            LoadPreferences();
        }
        else
        {
            LoadPreferences();
        }
    }

    // Change volume
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SavePreferences();
    }

    // Save preferences
    private void SavePreferences()
    {
        PlayerPrefs.SetFloat(musicVolume, volumeSlider.value);
    }

    // Load preferences
    private void LoadPreferences()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(musicVolume);
    }
}