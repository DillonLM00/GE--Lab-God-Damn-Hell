using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sound events that get called in the animation event system
public class DemonSpearAnimationSounds : MonoBehaviour
{
    public void DemonSpearRunSound()
    {
        AkSoundEngine.PostEvent("DemonSpearRun", gameObject);
    }

    public void DemonSpearSwingSound()
    {
        AkSoundEngine.PostEvent("SpearSwing", gameObject);
    }

    public void DemonSpearDeathSound()
    {
        AkSoundEngine.PostEvent("DemonSpearDeath", gameObject);
    }

    public void DemonSpearGotHitSound()
    {
        AkSoundEngine.PostEvent("DemonSpearGotHit", gameObject);
    }

    public void DemonSpearWalkSound()
    {
        AkSoundEngine.PostEvent("DemonSpearWalk", gameObject);
    }
}
