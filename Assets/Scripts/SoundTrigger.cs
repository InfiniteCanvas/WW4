/*
 * Copyright (c) Simon Josiek
 */

using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource[] outdoorSounds;

    private bool isIndoor;

    private void Start()
    {
        isIndoor = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        isIndoor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isIndoor = false;
    }

    private void Update()
    {
        if (isIndoor)
        {
            foreach (AudioSource a in outdoorSounds)
            {
                a.volume = 0.25f;
            }
        }
        else
        {
            foreach (AudioSource a in outdoorSounds)
            {
                a.volume = 1;
            }
        }
    }
}