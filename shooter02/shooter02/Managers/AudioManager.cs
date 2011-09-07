using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace shooter02.Managers
{
    struct AudioClip
    {
        public SoundEffect soundEffect;
        public SoundEffectInstance soundEffectInstance;
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
        /// <summary>
        /// Loads in a SoundEffect to the manager
        /// </summary>
        /// <param name="szFileName">File path to the SoundEffect to be loaded.</param>
        /// <returns>The ID of the newly added SoundEffect</returns>
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

        /// <summary>
        /// Will return the state in which the SoundEffect is in.
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be checked</param>
        /// <returns>Returns the state of the SoundEffect (playing, paused, or stopped)</returns>
        public SoundState SoundEffect_State(int nIndex)
        {
            if (nIndex < 0 || nIndex > m_vAudioClips.Count - 1)
                return SoundState.Stopped;

            return m_vAudioClips[nIndex].soundEffectInstance.State;
        }

        /// <summary>
        /// Will play the SoundEffect
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be played</param>
        public void SoundEffect_Play(int nIndex)
        {
            m_vAudioClips[nIndex].soundEffectInstance.Play();
        }

        /// <summary>
        /// Will pause the SoundEffect, giving the ability to resume playback from which it was left off.
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be paused</param>
        public void SoundEffect_Pause(int nIndex)
        {
            m_vAudioClips[nIndex].soundEffectInstance.Pause();
        }

        /// <summary>
        /// Stops the SoundEffect from playing without remembering where it left off.
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be stopped</param>
        public void SoundEffect_Stop(int nIndex)
        {
            m_vAudioClips[nIndex].soundEffectInstance.Stop();
        }

        /// <summary>
        /// Will Resume Playback of the SoundEffect(nIndex) from where it was paused
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be Resumed</param>
        public void SoundEffect_Resume(int nIndex)
        {
            m_vAudioClips[nIndex].soundEffectInstance.Resume();
        }

        /// <summary>
        /// Sets the specified SoundEffect(nIndex) to Loop or not.
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be modified</param>
        /// <param name="bLoop">Whether the SoundEffect will be Looped</param>
        public void SoundEffect_Loop(int nIndex, bool bLoop)
        {
            // NOTE: Call this function before calling Play() on nIndex
            // to begin the looping
            m_vAudioClips[nIndex].soundEffectInstance.IsLooped = bLoop;
        }

        /// <summary>
        /// Sets the Panning for the specified SoundEffect
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be modified</param>
        /// <param name="fPan">Value determining which side to pan to ranging from -1.0f to 1.0f</param>
        public void SoundEffect_Pan(int nIndex, float fPan)
        {
            // Make sure fPan is a value between -1.0f (left) and 1.0f(right)
            float fClamped = MathHelper.Clamp(fPan, -1.0f, 1.0f);

            m_vAudioClips[nIndex].soundEffectInstance.Pan = fClamped;
        }

        /// <summary>
        /// Sets the volume of the specified SoundEffect.
        /// </summary>
        /// <param name="nIndex">The ID for the SoundEffect that will be modified</param>
        /// <param name="fVolume">The volume to set the SoundEffect.</param>
        public void SoundEffect_Volume(int nIndex, float fVolume)
        {
            // Make sure fVolume is in between the valid ranges.
            float fClamped = MathHelper.Clamp(fVolume, 0.0f, 1.0f);

            m_vAudioClips[nIndex].soundEffectInstance.Volume = fClamped;
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
