using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class MainGameSoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource bgmSource;
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioSource aiSfxSource;
        [SerializeField] AudioSource playerSfxSource;

        [Header ("Clip")]
        [SerializeField] AudioClip bgmClip;
        [SerializeField] List<AudioClip> sfxClipList;
        [SerializeField] List<AudioClip> aiClipList;
        [SerializeField] List<AudioClip> playerStepClipList;
        [SerializeField] float fadeVolumeTime = 5f;

        public static MainGameSoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            PlayBGM();
        }

        /// BGM
        public void PlayBGM()
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.volume = 0.005f;
            bgmSource.Play();
        }

        public void StopBGM()
        {
            bgmSource.Stop();
        }

        /// SFX
        public void PlaySFX(string clipName)
        {
            AudioClip clipToPlay = sfxClipList.Find(clip => clip.name == clipName);
            sfxSource.PlayOneShot(clipToPlay);
        }

        public void StopSFX()
        {
            sfxSource.Stop();
        }

        /// AI
        public void AiNearSoundPlay(string clipName)
        {
            //aiSfxSource
            //aiClipList
        }

        public void AiFindSoundPlay(string clipName)
        {

        }

        public void AiSoundStop()
        {

        }

        /// Player �߼Ҹ�
        public void PlayerStopSound()
        {
            //playerSfxSource
            //playerStepClipList
        }

        public void PlayerRunSound()
        {

        }


        /// ���� ���̵���
        private IEnumerator FadeInVolume(AudioSource audio)
        {
            float targetVolume = 1f;
            float currentTime = 0f;

            while (currentTime < fadeVolumeTime)
            {
                currentTime += Time.deltaTime;
                audio.volume = Mathf.Lerp(0f, targetVolume, currentTime / fadeVolumeTime);
                yield return null;
            }

            audio.volume = targetVolume;
        }

        /// ���� ���̵�ƿ�
        private IEnumerator FadeOutVolume(AudioSource audio)
        {
            float startVolume = audio.volume;
            float currentTime = 0f;

            while (currentTime < fadeVolumeTime)
            {
                currentTime += Time.deltaTime;
                audio.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeVolumeTime);
                yield return null;
            }

            audio.volume = 0f;
            audio.Stop();
        }
    }
}
