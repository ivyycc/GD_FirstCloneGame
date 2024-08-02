using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{

    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;


    private List<StudioEventEmitter> eventEmitters;


    [SerializeField] public List<EventInstance> eventInst;
    //private List<StudioEventEmitter> eventEmitters;

    private EventInstance musicEventInstance;
    public static AudioManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        

        eventInst = new List<EventInstance>();

        // eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        // musicBus = RuntimeManager.GetBus("bus:/Music");
        //sfxBus = RuntimeManager.GetBus("bus:/SFX")
        

        InitializeMusic(FMODEvents.instance.Music);
    }

    public void Start()
    {
        //InitializeMusic(FMODEvents.instance.Music);

    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        //musicBus.setVolume(musicVolume);
        //sfxBus.setVolume(SFXVolume);
    }



    public void PlayOneShot(EventReference sound, Vector3 worldpos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound, worldpos);
    }

    public EventInstance CreateEventInstance(EventReference eR)
    {
        EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(eR);
        eventInst.Add(eventInstance);

        return eventInstance;
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
        Debug.Log("MUSIC STARTED");
    }


    /*public void SetMusicArea(MusicArea area)
    {
        musicEventInstance.setParameterByName("area", (float)area);
    }*/
    /*public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }*/

    public void CleanUp()
    {
        //stop and release any created instances

        foreach (EventInstance eventInstance in eventInst)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
    private void OnDestroy()
    {
        CleanUp();
    }


}
