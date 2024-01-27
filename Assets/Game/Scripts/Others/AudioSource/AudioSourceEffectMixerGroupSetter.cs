using UnityEngine.Audio;

public class AudioSourceEffectMixerGroupSetter : AudioSourceMixerGroupSetterBase
{
    protected override AudioMixerGroup GetMixerGroup()
    {
        return Settings.Instance.AudioMixer.FindMatchingGroups("Effects")[0];
    }
}
