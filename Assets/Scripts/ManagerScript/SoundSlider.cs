//�X���C�_�[��AudioSource��volume��ύX����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;//BGM�X���C�_�[
    [SerializeField] private Slider seSlider;//SE�X���C�_�[

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