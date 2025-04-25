/* 
HERE IS THE [3rd] PART TO CREATE INDICATION SOUND

1st sound: play after 5 sec enter in principle scene ORCHID
2nd sound: play as long as we are teleported to the second scene NEWS
3rd sound: play after 60 sec enter in second scene NEWS
4th sound: Once we were teleported back to the scene ORCHID
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Assign in Inspector

    void Start()
    {
        StartCoroutine(PlayAudioAfterDelay(20f)); //First sound played after 5 seconds enter into the scene "ORCHID"
    }

    IEnumerator PlayAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null && !SceneTransferData.hasPlayedThirdAudioSource)
        {
            audioSource.Play();
            SceneTransferData.hasPlayedThirdAudioSource = true;
        }
    }
}