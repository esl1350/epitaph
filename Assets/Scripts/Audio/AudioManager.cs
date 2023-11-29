using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;

    private void Start()
    {
        InitializeMusic(FMODEvents.instance.music);
    }
    private void Awake() 
    {
        if (instance != null) 
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        else 
        {
            instance = this;
        }
        
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void SetMusic(MusicArea area, string where="None")
    {
        Debug.Log(where);
        Debug.Log(area);
        musicEventInstance.setParameterByName("area", (float) area);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
