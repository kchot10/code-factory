using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundList
    {
        None = -1,
        Heavy_Clang = 0,
        Conveyorbelt = 1,
        Moter = 2,
        Spray = 3,
        Ring = 4,
        Necklace = 5,
        Input = 6,
        Output = 7,
        Water = 8,
        Swoosh = 9,
        Cartoon_Boing = 10,
        ChainsawLong = 11,
        Whirlpool = 12,
        Other5 = 13,
        Other6 = 14,
        Other7 = 15,
        Other8 = 16,
        Other9 = 17,
        Other10 = 18,
        Other11 = 19,
        Other12 = 20,
    }
    public AudioClip[] soundClips; // 각 사운드에 대한 AudioClip 배열

    public AudioClip GetSoundClip(SoundList sound)
    {
        int index = (int)sound;
        if (index >= 0 && index < soundClips.Length)
        {
            return soundClips[index];
        }
        return null;
    }
}
