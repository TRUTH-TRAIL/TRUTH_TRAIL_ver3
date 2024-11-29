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
        [SerializeField] float fadeVolumeTime = 3f;

        public static MainGameSoundManager Instance { get; private set; }

        private string playerState;

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
        }

        private void Start()
        {
            //PlayBGM();
            StartCoroutine(PlayerFootStepSound());
        }

        /// BGM
        public void PlayBGM()
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.volume = 0;
            StartCoroutine(FadeInVolume(bgmSource));
        }

        public void StopBGM()
        {
            bgmSource.volume = 1;
            StartCoroutine(FadeOutVolume(bgmSource));
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
        public void AiSoundPlay(string clipName)
        {
            AudioClip clipToPlay = aiClipList.Find(clip => clip.name == clipName);
            aiSfxSource.PlayOneShot(clipToPlay);
        }

        public void AiFindSoundPlay()
        {
            aiSfxSource.PlayOneShot(aiClipList[2]);
        }

        public void AiSoundStop()
        {
            aiSfxSource.Stop();
        }

        /// Player 발소리 
        public void PlayerState(string playerState)
        {
            this.playerState = playerState;
        }

        IEnumerator PlayerFootStepSound()
        {
            while (true)
            {
                switch (playerState)
                {
                    case "walking":
                        if (Input.GetKey(KeyCode.W))
                        {
                            playerSfxSource.PlayOneShot(playerStepClipList[0]);
                            yield return new WaitForSeconds(0.6f);
                        }
                        else
                            yield return null;
                        break;
                    case "slowWalking":
                        if (Input.GetKey(KeyCode.W))
                        {
                            playerSfxSource.PlayOneShot(playerStepClipList[0]);
                            yield return new WaitForSeconds(1f);
                        }
                        else
                            yield return null;
                        break;
                    case "running":
                        if (Input.GetKey(KeyCode.W))
                        {
                            playerSfxSource.PlayOneShot(playerStepClipList[0]);
                            yield return new WaitForSeconds(0.3f);
                        }
                        else
                            yield return null;
                        break;
                    default:
                        yield return null;
                        break;
                }
            }
        }

        public void PlayerRunStop()
        {
            playerSfxSource.Stop();
        }


        /// 볼륨 페이드인
        private IEnumerator FadeInVolume(AudioSource audio)
        {
            float targetVolume = 1f;
            float currentTime = 0f;

            audio.Play();

            while (currentTime < fadeVolumeTime)
            {
                currentTime += Time.deltaTime;
                audio.volume = Mathf.Lerp(0f, targetVolume, currentTime / fadeVolumeTime);
                yield return null;
            }

            audio.volume = targetVolume;   
        }

        /// 볼륨 페이드아웃
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
