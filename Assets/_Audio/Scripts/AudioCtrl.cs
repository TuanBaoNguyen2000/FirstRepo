using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AudioCtrl : MyMonoBehaviour
{
    [SerializeField] protected List<AudioSource> audios;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioSources();
    }

    protected virtual void LoadAudioSources()
    {
        if (this.audios.Count > 0) return;
        foreach (Transform audio in transform)
        {           
            this.audios.Add(audio.GetComponent<AudioSource>());
        }
        Debug.Log(transform.name + ": LoadPoints", gameObject);
    }

    public virtual AudioSource GetAudio(string audioName)
    {
        foreach (AudioSource audio in this.audios)
        {
            if (audio.name == audioName) return audio;
        }
        return null;
    }

}
