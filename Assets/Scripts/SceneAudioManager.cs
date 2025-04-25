/* 
HERE IS THE [2nd]&[4th] PART TO CREATE INDICATION SOUND

1st sound: play after 5 sec enter in principle scene ORCHID
2nd sound: play as long as we are teleported to the second scene NEWS
3rd sound: play after 60 sec enter in second scene NEWS
4th sound: Once we were teleported back to the scene ORCHID
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    void Awake()
    {
        // Make this object persist between scenes
        DontDestroyOnLoad(this.gameObject);

        // Subscribe to scene loaded events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ORCHID_les_infos_option_2" && !SceneTransferData.hasPlayedSecondAudioSource)
        {
            PlayAudioInScene("Audio_src_2nd");
            SceneTransferData.hasPlayedSecondAudioSource = true;
        }
        else if (scene.name == "ORCHID" && !SceneTransferData.firstloadSceneORCHID && !SceneTransferData.hasPlayedForthAudioSource)
        {
            PlayAudioInScene("Audio_src_4th"); // <-- Use your actual GameObject name in SceneA
            SceneTransferData.hasPlayedForthAudioSource = true;
        }
    }

    void PlayAudioInScene(string audioObjectName)
    {
        GameObject audioObj = GameObject.Find(audioObjectName);

        if (audioObj != null)
        {
            AudioSource audio = audioObj.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
            else
            {
                Debug.LogWarning($"AudioSource not found on {audioObjectName}.");
            }
        }
        else
        {
            Debug.LogWarning($"{audioObjectName} not found in the scene.");
        }
    }

    void OnDestroy()
    {
        // Clean up the event when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}