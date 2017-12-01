using UnityEngine;

namespace WW4.Database
{
    public interface SoundDatabaseHandler
    {
        string GetAudioClipUrl();
        bool IdentifyAudioclip(string clipUrl, string name);
    }
}