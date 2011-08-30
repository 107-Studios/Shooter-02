using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace shooter02.Managers
{
    struct AudioClip
    {
        public SoundEffect soundEffect;
        public SoundEffectInstance soundEffectInstance;
    }

    struct BGM
    {
        
    }

    class AudioManager
    {
        private static readonly AudioManager instance = new AudioManager();
        public static AudioManager Instance
        {
            get
            {
                return instance;
            }
        }

        // data members
        private List<AudioClip> m_vAudioClips;

        // constructor
        public AudioManager()
        {
            m_vAudioClips = new List<AudioClip>();
        }

        // methods
        public int LoadSoundEffect(string szFileName)
        {
            // check if audioClip is already in DB
            int result = AudioClipExists(szFileName);
            if (result >= 0)
                return result;

            // since audioClip isn't in DB, add it
            AudioClip newClip = new AudioClip();
            newClip.soundEffect = StateManager.Instance.ContentManagerInstance.Load<SoundEffect>(szFileName);
            newClip.soundEffectInstance = newClip.soundEffect.CreateInstance();

            m_vAudioClips.Add(newClip);

            // return the new addition's ID
            return m_vAudioClips.Count - 1;
        }

        public SoundState SoundEffect_State(int nIndex)
        {
            if (nIndex < 0 || nIndex > m_vAudioClips.Count - 1)
                return SoundState.Stopped;

            return m_vAudioClips[nIndex].soundEffectInstance.State;
        }

        public void SoundEffect_Play(int nIndex)
        {
            m_vAudioClips[nIndex].soundEffectInstance.Play();
        }

        // helper functions
        private bool ValidIndex(int nIndex, int arrayCount)
        {
            if (nIndex < 0 || nIndex > arrayCount)
                return false;
            else
                return true;
        }

        private int AudioClipExists(string szFileName)
        {
            // iterate through each audio clip in DB
            for (int i = 0; i < m_vAudioClips.Count; i++)
            {
                if (m_vAudioClips[i].soundEffect.Name == szFileName)
                    return i;
            }

            // audioClip was not found in DB
            return -1;
        }

    }
}
