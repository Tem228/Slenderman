using UnityEngine.Audio;

public class AudioSourceMusicMixerGroupSetter : AudioSourceMixerGroupSetterBase
{
    protected override AudioMixerGroup GetMixerGroup()
    {
        return Settings.Instance.AudioMixer.FindMatchingGroups("Music")[0];
    }
}
