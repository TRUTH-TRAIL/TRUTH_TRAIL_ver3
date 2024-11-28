using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace TT
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializedDictionary("Sound Name", "Sound Clip")]
        public SerializedDictionary<string, AudioClip> AudioClipContainer = new ();
        
        [SerializeField] private AudioSource audioSource;

        public void PlaySound(string soundName, bool isLooping)
        {
            AudioClip playClip = AudioClipContainer[soundName];

            if (!audioSource.isPlaying)
            {
                audioSource.loop = isLooping;
                audioSource.clip = playClip;
                audioSource.Play();
            }
        }

        public void FindAISound()
        {
            PlaySound("FindAI", true);
        }
        
        public void StopSound()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
