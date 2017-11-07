using UnityEngine;

namespace WW4.Database
{
    public interface SoundDatabaseHandler
    {
        string GetAudioClipUrl();
        void IdentifyAudioclip(WWW clipUrl, string name);
    }
}