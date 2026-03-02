using UnityEngine;
using UnityEngine.Audio;

public class MuteScript : MonoBehaviour
{
    public AudioMixer masterMixer;
    public string parameterName = "MusicVol"; 

    void Start()
    {
        masterMixer.SetFloat(parameterName, -80f);
    }
}
