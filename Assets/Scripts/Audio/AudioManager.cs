using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviourSingleton<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource voiceSource;

        public void PlayMusic(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }

        public void StopMusic() => musicSource.Stop();
        
        public void PlaySfx(AudioClip clip) => sfxSource.PlayOneShot(clip);

        public void PlayVoice(AudioClip clip)
        {
            voiceSource.clip = clip;
            voiceSource.Play();
        }

        public void StopVoice() => voiceSource.Stop();
    }
}
