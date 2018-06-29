using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource channel_a, channel_b = null;
    private AudioSource aSFX = null;
    private Settings settings;

    [Range(0f, 1f)]
    public float bgm_volume = 1.0f;

    private bool cross_fade = false;
    private float vol = 0.0f;
    private string master_channel = "";

    private float _cross_time = 1.0f;

    void Start() {
        aSFX = this.gameObject.GetComponent<AudioSource>();
        settings = GameObject.Find("Main").GetComponent<Settings>();
    }

    void Awake () {
        DontDestroyOnLoad(this);
    }

    void Update() {
        bgm_volume = Settings.volum;

        if (cross_fade) {
            CrossFade();
        } else if ((channel_a != null) || (channel_b != null)) {
            if (master_channel == "A") {
                channel_a.volume = 1f * bgm_volume;
            } else {
                channel_b.volume = 1f * bgm_volume;
            }
        }
    }

    public void PlayFX (AudioClip clip) {

        aSFX.volume = Settings.volumFX;
        aSFX.PlayOneShot(clip);  
    }

    public void Play(uint id, float cross_time = 1.0f) {
        _cross_time = cross_time;
        if (_cross_time < 0.01f) _cross_time = 0.01f;

        if (id < settings.music.Length) {  

            if (channel_a == null) {

                channel_a = this.gameObject.AddComponent<AudioSource>();
                channel_a.playOnAwake = false;
                channel_a.volume = 0f;
                channel_a.clip = settings.music[id];
                channel_a.loop = true;
                channel_a.Play();
                master_channel = "A";
                cross_fade = true;
                vol = 0f;

            } else if (channel_b == null) {

                channel_b = this.gameObject.AddComponent<AudioSource>();
                channel_b.playOnAwake = false;
                channel_b.volume = 0f;
                channel_b.clip = settings.music[id];
                channel_b.loop = true;
                channel_b.Play();
                master_channel = "B";
                cross_fade = true;
                vol = 0f;
            }
        }
    }

    void CrossFade() {

        AudioSource ch_in = null;
        AudioSource ch_out = null;

        if (master_channel == "A") {
            ch_in = channel_a;
            ch_out = channel_b;
        } else {
            ch_in = channel_b;
            ch_out = channel_a;
        }

        vol += Time.deltaTime * (1 / _cross_time);
        if (vol >= 1f) {
            vol = 1f;
            cross_fade = false;

            if (ch_out != null) {
                ch_out.Stop();
                Destroy(ch_out);
            }

        }
        ch_in.volume = vol * bgm_volume;
        if (ch_out != null) ch_out.volume = (1f - vol) * bgm_volume;
    }
}
