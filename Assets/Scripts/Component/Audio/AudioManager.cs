using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Add(AudioClipName.Slash,
            Resources.Load<AudioClip>("attack"));
        audioClips.Add(AudioClipName.Heal,
            Resources.Load<AudioClip>("heal"));
        audioClips.Add(AudioClipName.Frezze,
            Resources.Load<AudioClip>("frezz"));
        audioClips.Add(AudioClipName.EnemyDead,
            Resources.Load<AudioClip>("death"));
        audioClips.Add(AudioClipName.EndGame,
            Resources.Load<AudioClip>("endgame"));
        audioClips.Add(AudioClipName.Dash,
            Resources.Load<AudioClip>("dash"));
        audioClips.Add(AudioClipName.LvUp,
            Resources.Load<AudioClip>("lvup"));
        audioClips.Add(AudioClipName.EnemyAttack,
            Resources.Load<AudioClip>("enemyattack"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
