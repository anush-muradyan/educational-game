using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Core.Shared;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Managers
{
    public class SoundManager: MonoBehaviour {
        public UnityEvent<bool> OnSoundMute = new UnityEvent<bool>();

        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioSource backgroundAudioSource;

        private static SoundManager _instance;

        [Inject] private IAddressableProvider addressableProvider;

        public static SoundManager Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<SoundManager>();
                }

                return _instance;
            }
        }

        private string musicVolumeKey;
        private string sfxVolumeKey;

        private bool musicMute;
        private bool sfxMute;
        private bool isSoundOn;

        public float SfxVolume => sfxAudioSource.volume;
        public float MusicVolume => backgroundAudioSource.volume;

        public bool IsSoundOn => !musicMute && !sfxMute;

        private Dictionary<SoundName, AudioClip> soundsByName = new Dictionary<SoundName, AudioClip>();

        private void Awake() {
            musicVolumeKey = Constants.MUSIC_KEY + Constants.VOLUME_KEY;
            sfxVolumeKey = Constants.SOUND_KEY + Constants.VOLUME_KEY;

            SetSFXVolume(PlayerPrefs.GetFloat(sfxVolumeKey, 1));
            SetMusicVolume(PlayerPrefs.GetFloat(musicVolumeKey, 1));

            musicMute = PlayerPrefs.GetInt(Constants.MUSIC_KEY, 1) == 0;
            sfxMute = PlayerPrefs.GetInt(Constants.SOUND_KEY, 1) == 0;

            MuteSFX(musicMute);
            Mute(sfxMute);

            preloadAudios();
        }

        public void MuteSFX(bool mute) {
            sfxAudioSource.mute = mute;
            sfxMute = mute;
            PlayerPrefs.SetInt(Constants.SOUND_KEY, !mute ? 1 : 0);
        }

        public void Mute(bool mute) {
            OnSoundMute?.Invoke(mute);
            MuteMusic(mute);
            MuteSFX(mute);
        }

        public void MuteMusic(bool mute) {
            backgroundAudioSource.mute = mute;
            musicMute = mute;
            PlayerPrefs.SetInt(Constants.MUSIC_KEY, !mute ? 1 : 0);
        }

        public void SetMusicVolume(float volume) {
            //volume /= 2;
            backgroundAudioSource.volume = volume;
            PlayerPrefs.SetFloat(musicVolumeKey, volume);
        }

        public void SetSFXVolume(float volume) {
            sfxAudioSource.volume = volume;
            PlayerPrefs.SetFloat(sfxVolumeKey, volume);
        }

        public void PlayMusic(SoundName name) {
            backgroundAudioSource.clip = soundsByName[name];
            backgroundAudioSource.Play();
        }

        public void PlaySFX(SoundName name) {
            sfxAudioSource.clip = soundsByName[name];
            sfxAudioSource.PlayOneShot(sfxAudioSource.clip);
        }

        public void PlayMusicByClip(AudioClip clip) {
            if (backgroundAudioSource.clip == clip) {
                return;
            }
            backgroundAudioSource.clip = clip;
            backgroundAudioSource.Play();
        }

        private void preloadAudios() {
            var soundNames = Enum.GetValues(typeof(SoundName)).Cast<SoundName>();

            foreach (var soundName in soundNames) {
                loadAudioClip(soundName.ToString()).OnSuccess(result =>
                {
                    soundsByName[soundName] = result;
                });
            }
        }


        private AsyncTask<AudioClip> loadAudioClip(string name) {
            var task = new AsyncTask<AudioClip>();
            var loader = Resources.LoadAsync<AudioClip>($"Sound/SFX/{name}");
            loader.completed += operation => {
                if (!operation.isDone) {
                    return;
                }

                task.Success(loader.asset as AudioClip);
            };
            return task;
        }


        private AsyncTask<AudioClip> loadVoiceover(string name) {
            AsyncTask<AudioClip> task = new AsyncTask<AudioClip>();
            addressableProvider.LoadAsset<AudioClip>(name, clip => {
                if (clip != null) {
                    task.Success(clip);
                }
                else {
                    task.Fail(new Exception($"No Audio Clip With Name : {name}"));
                }
            });
            return task;
        }
    }

    public enum SoundName {
        CorrectAnswer, 
        WrongAnswer
    }
    
}