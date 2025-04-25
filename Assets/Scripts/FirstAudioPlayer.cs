/* 
HERE IS THE [1st] PART TO CREATE INDICATION SOUND

1st sound: play after 5 sec enter in principle scene ORCHID
2nd sound: play as long as we are teleported to the second scene NEWS
3rd sound: play after 60 sec enter in second scene NEWS
4th sound: Once we were teleported back to the scene ORCHID
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Assign in Inspector

    void Start()
    {
        StartCoroutine(PlayAudioAfterDelay(8f));
    }

    IEnumerator PlayAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null && !SceneTransferData.hasPlayedFirstAudioSource)
        {
            audioSource.Play();
            SceneTransferData.hasPlayedFirstAudioSource = true;
            SceneTransferData.firstloadSceneORCHID = false;
        }
    }
}
