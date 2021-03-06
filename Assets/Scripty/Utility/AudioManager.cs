﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripty
{
    public class AudioManager
    {
        private AudioSource _audioSourceEfekt;
        private AudioSource _audioSourceHudba;
        private AudioSource _audioSourceZvuky;
        private AudioClip _defaultEfekt;
        private AudioClip _defaultHudba;
        public bool IsPlaying;

        private List<AudioClip> _hudba = new List<AudioClip>();

        public AudioManager(AudioSource[] audioSource)
        {
            _audioSourceEfekt = audioSource[0];
            _audioSourceHudba = audioSource[1];
            _audioSourceZvuky = audioSource[2];

            string[] soundtrack = new[] {
                // "Audio/Music/1",
                // "Audio/Music/3",
                "Audio/Music/4",
            };

            _hudba = soundtrack.Select(Resources.Load<AudioClip>).ToList();
            _defaultEfekt = _audioSourceEfekt.clip;
            _defaultHudba = _audioSourceHudba.clip;
        }

        public void ZmenEfektNaDefault()
        {
            _audioSourceEfekt.clip = _defaultEfekt;
        }

        public void ZmenHudbuNaDefault()
        {
            ZastavHudbu();
            _audioSourceHudba.clip = _defaultHudba;
            PustHudbu();
        }


        public void ZmenEfekt(AudioClip clip)
        {
            _audioSourceEfekt.clip = clip;
        }


        public void ZahrajZvuk(AudioClip clip)
        {
            _audioSourceZvuky.PlayOneShot(clip);
        }

        public void PustHudbu()
        {
            _audioSourceHudba.Play();
            IsPlaying = true;
        }

        public void PustEfekt()
        {
            _audioSourceEfekt.Play();
        }

        public void ZmenHudbu()
        {
            ZastavHudbu();
            var delka = _hudba.Count;
            _audioSourceHudba.clip = _hudba[UnityEngine.Random.Range(0, delka)];
            PustHudbu();
        }

        public void ZastavHudbu()
        {
            _audioSourceHudba.Stop();
            IsPlaying = false;
        }

        public void ZastavEfekt()
        {
            _audioSourceEfekt.Stop();
            IsPlaying = false;
        }

        public void ZtlumVse(float okolik)
        {
            _audioSourceEfekt.volume = okolik;
            _audioSourceHudba.volume = okolik;

        }

        public void VypniVse()
        {
            _audioSourceEfekt.volume = 0f;
            _audioSourceHudba.volume = 0f;
            _audioSourceZvuky.volume = 0f;
            IsPlaying = false;
        }
        public void ZapniVse()
        {
            _audioSourceEfekt.volume = 1f;
            _audioSourceHudba.volume = 1f;
            _audioSourceZvuky.volume = 1f;
            IsPlaying = true;
        }
    }
}