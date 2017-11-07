using System;
using UnityEngine;
using WW4.Database;

namespace WW4.TestScripts
{
    [Serializable]
    public class SoundDatabase : MonoBehaviour, SoundDatabaseHandler
    {
        public string[] Url;

        public static SoundDatabaseHandler SoundDatabaseHandler { private set; get; }

        public string GetAudioClipUrl()
        {
            return Url[UnityEngine.Random.Range(0,Url.Length)];
        }

        public void IdentifyAudioclip(WWW clipUrl, string name)
        {
            throw new NotImplementedException();
        }

        private void Awake()
        {
            SoundDatabaseHandler = this;
        }
    }
}