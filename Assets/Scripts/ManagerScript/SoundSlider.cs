//スライダーでAudioSourceのvolumeを変更する
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;//BGMスライダー
    [SerializeField] private Slider seSlider;//SEスライダー

    public void BgmVolume()
    {
        float a = bgmSlider.value * 0.8f;
        SoundManager.Instance.SetBgmVolume(a);
        print(a);
    }

    public void SeVolume()
    {
        float b = seSlider.value;
        SoundManager.Instance.SetSeVolume(b);
        print(b);
    }
}