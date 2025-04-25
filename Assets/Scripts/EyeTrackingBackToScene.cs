using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
// integration Cube's EyeTracking DONE + Pushaway 3D particles DONE

public class EyeTrackingBackToScene : MonoBehaviour
{
    public float fixationTime = 1f; // Temps avant activation
    // public float fixationTimeChangeScene = 4f;
    private float gazeTimer = 0f;
    private bool isPlaying = false;

    public List<Transform> gazeTargets; // List of objects to look at (Cubes)

    public int distOfRay = 10;

    //TODO: paring the obj and Anim
    // private Dictionary<GameObject, Animator> objectAnimators = new Dictionary<GameObject, Animator>();

    void Start()
    {
    }

    void Update()
    {
        bool isLookingAtAnyTarget = false;
        Transform currentTarget = null;

        for (int i = 0; i < gazeTargets.Count; i++)
        {
            if (IsLookingAtTarget(gazeTargets[i]))
            {
                isLookingAtAnyTarget = true;
                currentTarget = gazeTargets[i];
                // Debug.Log("=================Current gaze target: " + i + "=================");
                break;
            }
        }

        if (isLookingAtAnyTarget) //Looking at the IphoneExit
        {
            gazeTimer += Time.deltaTime;
            // Debug.Log("Regarde l'objet: " + gazeTimer); // ✅ Vérifie dans la console si ça s'incrémente

            int index = gazeTargets.IndexOf(currentTarget);

            if (gazeTimer >= fixationTime)
            {
                Renderer targetRenderer = currentTarget.GetComponent<Renderer>();
                if (targetRenderer != null)
                {
                    Material material = targetRenderer.material;

                    // Use Mathf.PingPong to oscillate between 
                    float emissionIntensity = Mathf.PingPong(Time.time, 1.0f) * 1.0f + 0.5f; // 

                    // Get the current emission color
                    Color emissionColor = material.GetColor("_EmissionColor");

                    // Apply the calculated intensity to the emission color (keep the original color but adjust intensity)
                    emissionColor *= emissionIntensity;

                    // Set the new emission color back to the material
                    material.SetColor("_EmissionColor", emissionColor);
                }

                AudioSource audioSource = currentTarget.GetComponent<AudioSource>();
                if (!audioSource.isPlaying) // If the VideoPlayer exists and isn't already playing
                {
                    // audioSource.PlayOneShot(myAudioClip); // Start playing the video
                    audioSource.Play();
                    Debug.Log("Audio de IphoneExit started on target: " + currentTarget.name);
                }

                VideoPlayer videoPlayer = currentTarget.GetComponent<VideoPlayer>();
                if (videoPlayer != null && !videoPlayer.isPlaying) // If the VideoPlayer exists and isn't already playing
                {
                    videoPlayer.Play(); // Start playing the video
                    Debug.Log("Video started playing on target: " + currentTarget.name);
                }

                // Start a Coroutine to wait for 5 seconds before loading the scene
                StartCoroutine(LoadSceneAfterDelay(5f));
                
                gazeTimer = 0f; // Reset gazeTimer
                isPlaying = true;
            }
        }
        else
        {
            gazeTimer = 0f;
        }
    }

    private bool IsLookingAtTarget(Transform gTarget)
    {
        //TODO/ replace it with 'raycast detection' (in order to work on multiple objects EyeTracking)
        Vector3 direction = gTarget.position - Camera.main.transform.position;
        direction.Normalize();
        float dot = Vector3.Dot(Camera.main.transform.forward, direction);

        //Debug.Log("Angle de regard: " + dot); // ✅ Vérifie dans la console si le regard est bien détecté

        return dot > 0.95f; // L'objet est bien dans l'axe de vision
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Load the new scene
        SceneTransferData.shouldSpawnAvatar = true;
        Debug.Log("****************************SceneTransferData (sceneNews) = " + SceneTransferData.shouldSpawnAvatar);
        SceneManager.LoadScene("ORCHID");
        // SceneManager.UnloadSceneAsync("ORCHID_les_infos_option_2");
    }
}
