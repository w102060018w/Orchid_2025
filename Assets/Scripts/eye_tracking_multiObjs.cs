using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// integration Cube's EyeTracking DONE + Pushaway 3D particles DONE

public class EyeTrackingActivation : MonoBehaviour
{
    public float fixationTime = 5f; // Temps avant activation
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
        bool isLookingAtAnyTarget = false;
        Transform currentTarget = null;

        for (int i = 0; i < gazeTargets.Count; i++)
        {
            if (IsLookingAtTarget(gazeTargets[i]))
            {
                isLookingAtAnyTarget = true;
                currentTarget = gazeTargets[i];
                Debug.Log("=================Current gaze target: " + i + "=================");
                break;
            }
        }

        if (isLookingAtTarget)
        {
            gazeTimer += Time.deltaTime;
            Debug.Log("Regarde l'objet: " + gazeTimer); // ✅ Vérifie dans la console si ça s'incrémente

            if (gazeTimer >= fixationTime)
            {
                Debug.Log("=================Anim Success=================");
                int index = gazeTargets.IndexOf(currentTarget);
                ActivateAnimation(animators[index]);
                gazeTimer = 0f; // Reset gazeTimer
                isPlaying = true;
                StartCoroutine(ReturnToIdle(animators[index]));
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

        Debug.Log("Angle de regard: " + dot); // ✅ Vérifie dans la console si le regard est bien détecté

        return dot > 0.95f; // L'objet est bien dans l'axe de vision
    }

    private void ActivateAnimation(Animator anim)
    {
        if (anim != null)
        {
            Debug.Log("Animation activée !");
            //anim.SetTrigger("Activate");
            anim.Play("activation");
        }
        else
        {
            Debug.LogWarning("Aucun Animator trouvé sur l'objet !");
        }
    }

    private IEnumerator ReturnToIdle(Animator anim)
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.Play("Idle");
        isPlaying = false;
    }
}
