using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class BtnConfig : MonoBehaviour
{
    
    public Slider musicVol,fxVol;
    public RectTransform btnConfig;
    
    public float musicValue;
    public float sfxValue;
    float t = 0;
    public GameObject sourceMusic,sourceSfx;
    void Start()
    {
        
        musicVol.value = PlayerPrefs.GetFloat("musicVol",0.5f);
        sourceMusic = GameObject.Find("Music");
        //AudioListener.volume = musicVol.value;
       // AudioSource.

    }

    // Update is called once per frame
    void Update()
    {

        t += Time.deltaTime;
        float lerpY = Mathf.Lerp(-2000, -1100, t);
       
        btnConfig.localPosition = new Vector3(0,lerpY,0);
        btnConfig.transform.Rotate(0, 0, 0.10f);

        ChangeMusicVolume(musicValue);
        ChangeSfxVolume(sfxValue);

    }

    public void ChangeMusicVolume(float valor) 
    {
        musicValue = musicVol.value;
        PlayerPrefs.SetFloat("musicVol", musicValue);
        sourceMusic.GetComponent<AudioSource>().volume = musicValue;
    }
    
    public void ChangeSfxVolume(float sfXVol) 
    {
        sfxValue = fxVol.value;
        PlayerPrefs.SetFloat("fxVol", sfxValue);
        sourceSfx.GetComponent<AudioSource>().volume = sfxValue;
    
    }

    


}
