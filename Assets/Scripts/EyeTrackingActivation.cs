using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
// integration Cube's EyeTracking DONE + Pushaway 3D particles DONE

public class EyeTrackingActivation : MonoBehaviour
{
    public float fixationTime = 1f; // Temps avant activation
    public float preHintTime = 1f; // Temps avant hint activation
    // public float fixationTimeChangeScene = 4f;
    private float gazeTimer = 0f;
    private bool isPlaying = false;

    public List<Transform> gazeTargets; // List of objects to look at (Cubes)
    private List<Animator> animators;

    public int distOfRay = 10;
    private RaycastHit _hit;

    //TODO: paring the obj and Anim
    // private Dictionary<GameObject, Animator> objectAnimators = new Dictionary<GameObject, Animator>();

    void Start()
    {
        animators = new List<Animator>();
        foreach (var gTar in gazeTargets)
        {
            if (gTar != null)
            {
                animators.Add(gTar.GetComponent<Animator>());
            }
        }
    }

    void Update()
    {
        Debug.Log("****************************SceneTransferData (scenePrinciple)= " + SceneTransferData.shouldSpawnAvatar);
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

        if (isLookingAtAnyTarget)
        {
            gazeTimer += Time.deltaTime;
            // Debug.Log("Regarde l'objet: " + gazeTimer); // ✅ Vérifie dans la console si ça s'incrémente

            int index = gazeTargets.IndexOf(currentTarget);

            if (gazeTimer >= preHintTime)
            {
                // Get the Renderer and Material of the current target
                Renderer targetRenderer = currentTarget.GetComponent<Renderer>();
                if (targetRenderer != null)
                {
                    Material material = targetRenderer.material;

                    // Use Mathf.PingPong to oscillate between -0.8 and 1.2 over a 2 second duration
                    float emissionIntensity = Mathf.PingPong(Time.time, 5.0f) * 3.0f; // Oscillates between 0 and 3.0

                    // Get the current emission color
                    Color emissionColor = material.GetColor("_EmissionColor");

                    // Apply the calculated intensity to the emission color (keep the original color but adjust intensity)
                    emissionColor *= emissionIntensity;

                    // Set the new emission color back to the material
                    material.SetColor("_EmissionColor", emissionColor);
                }
            }

            if (gazeTimer >= fixationTime)
            {
                AudioSource audioSource = currentTarget.GetComponent<AudioSource>();
                if (!audioSource.isPlaying) // If the VideoPlayer exists and isn't already playing
                {
                    // audioSource.PlayOneShot(myAudioClip); // Start playing the video
                    audioSource.Play();
                    Debug.Log("Audio de GLYPHE started on target: " + currentTarget.name);
                }
                
                //Debug.Log("=================Anim Success=================");
                // int index = gazeTargets.IndexOf(currentTarget);
                ActivateAnimation(animators[index], index);
                gazeTimer = 0f; // Reset gazeTimer
                isPlaying = true;
                StartCoroutine(ReturnToIdle(animators[index], 5f));

                
                // if(IsLookingAtTarget(currentTarget))
                // {
                //     SceneManager.LoadScene("ORCHID_les_infos_option_2");
                //     // StartCoroutine(WaitForAnimationAndLoad(animators[index], "G_activation", "ORCHID_les_infos_option_2"));
                //     Debug.Log("=========================Is still looking at the game object or not:" + IsLookingAtTarget(currentTarget));
                // }
                
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

    private void ActivateAnimation(Animator anim, int index)
    {
        if (anim != null)
        {
            anim.Play("G_activation");
            StartCoroutine(WaitForAnimationAndLoad(index, animators[index], "G_activation", "ORCHID_les_infos_option_2"));
            // Debug.Log("=========================Is still looking at the game object or not:" + IsLookingAtTarget(currentTarget));
        }
        else
        {
            //Debug.LogWarning("Aucun Animator trouvé sur l'objet !");
        }
    }

    private IEnumerator WaitForAnimationAndLoad(int index, Animator animator, string animationName, string sceneName)
    {
        // Wait until the animation state is actually playing
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(animationName));

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        if(IsLookingAtTarget(gazeTargets[index])){
            SceneManager.LoadScene(sceneName);
            
        }
    }

    private IEnumerator ReturnToIdle(Animator anim, float delayDuration)
    {
        // yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(delayDuration);
        anim.Play("Idle");
        isPlaying = false;
    }
}
