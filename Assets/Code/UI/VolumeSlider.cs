using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // used to connect slider value and GameMaster.GM.volume
    public enum VolumeType { masterVolume, musicVolume, sfxVolume}
    public VolumeType myVolumeType;
    [SerializeField] AudioMixer audioMixer;

    // Update slider position whenever I'm activated
    void OnEnable()
    {
        float normalizedVolume = LogToNormalVolume(GetMusicVolume());
        GetComponent<Slider>().value = normalizedVolume; // unity is dumb and needs extra get method
        //GetComponent<Slider>().value = GameMaster.GM.volume;
    }

    // On Value Changed (when slider is moved)
    public void SetVolumeToSliderValue()
    {
        float logVolume = NormalToLogVolume(GetComponent<Slider>().value);
        audioMixer.SetFloat(EnumToString(), logVolume);
        //GameMaster.GM.volume = GetComponent<Slider>().value;
    }

    string EnumToString()
    {
        if (myVolumeType == VolumeType.masterVolume)
            return nameof(VolumeType.masterVolume);
        if (myVolumeType == VolumeType.musicVolume)
            return nameof(VolumeType.musicVolume);
        if (myVolumeType == VolumeType.sfxVolume)
            return nameof(VolumeType.sfxVolume);

        print("No music enum found");
        return null;
    }

    float GetMusicVolume()
    {
        try
        {
            audioMixer.GetFloat(EnumToString(), out float value);
            return value;
        }
        catch
        {
            print("volume setting not found in audioMixer");
            return 0;
        }
    }

    float LogToNormalVolume(float logVolume)
    {
        if (logVolume <= -79)
            return 0; // avoids negative values and asymptote
        return Mathf.Pow(10, logVolume / 20);
    }
    float NormalToLogVolume(float normalVolume)
    {
        if (normalVolume == 0)
            return -80; // instead of -79, log(0) = undefined
        return Mathf.Log10(normalVolume) * 20; // -80 to 0, not up to 20
    }
}
