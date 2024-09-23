using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class SoundUI : MonoBehaviour
    {
        public AudioSource musicSource;  // ������ ����ϴ� AudioSource
        //public AudioSource sfxSource;    // ȿ������ ����ϴ� AudioSource
        [Space(10)]
        public Slider musicSlider;
        //public Slider sfxSlider;

        // Start is called before the first frame update
        void Start()
        {
            // PlayerPrefs���� ����� ���� ���� �ε��Ͽ� ����
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
            //sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);

            // �����̴� ���� ����� ������ �ʱ�ȭ
            musicSlider.value = musicSource.volume;
           // sfxSlider.value = sfxSource.volume;
        }

        // ���� ���� ����
        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        // ȿ���� ���� ����
        // public void SetSfxVolume(float volume)
        // {
        //     sfxSource.volume = volume;
        // }

        private void OnEnable()
        {
            // PlayerPrefs���� ���� ���� �ҷ��� �����̴��� ����
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
            //sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        }

        private void OnDisable()
        {
            // ���� AudioSource�� ���� ���� PlayerPrefs�� ����
            PlayerPrefs.SetFloat("musicVolume", musicSource.volume);
            //PlayerPrefs.SetFloat("sfxVolume", sfxSource.volume);
            PlayerPrefs.Save();
        }
    }
}
