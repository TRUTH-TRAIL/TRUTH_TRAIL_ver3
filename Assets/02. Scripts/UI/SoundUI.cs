using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class SoundUI : MonoBehaviour
    {
        public AudioSource musicSource;  // 음악을 재생하는 AudioSource
        //public AudioSource sfxSource;    // 효과음을 재생하는 AudioSource
        [Space(10)]
        public Slider musicSlider;
        //public Slider sfxSlider;

        // Start is called before the first frame update
        void Start()
        {
            // PlayerPrefs에서 저장된 볼륨 값을 로드하여 적용
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
            //sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);

            // 슬라이더 값도 저장된 값으로 초기화
            musicSlider.value = musicSource.volume;
           // sfxSlider.value = sfxSource.volume;
        }

        // 음악 볼륨 조절
        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        // 효과음 볼륨 조절
        // public void SetSfxVolume(float volume)
        // {
        //     sfxSource.volume = volume;
        // }

        private void OnEnable()
        {
            // PlayerPrefs에서 볼륨 값을 불러와 슬라이더에 적용
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
            //sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        }

        private void OnDisable()
        {
            // 현재 AudioSource의 볼륨 값을 PlayerPrefs에 저장
            PlayerPrefs.SetFloat("musicVolume", musicSource.volume);
            //PlayerPrefs.SetFloat("sfxVolume", sfxSource.volume);
            PlayerPrefs.Save();
        }
    }
}
