using UnityEngine;
using System.Collections;
// integration Cube's EyeTracking DONE + Pushaway 3D particles DONE

public class EyeTrackingActivation : MonoBehaviour
{
    public float fixationTime = 5f; // Temps avant activation
    private float gazeTimer = 0f;
    private bool isPlaying = false;

    public Transform gazeTarget_1; // L'objet à regarder (Cube)
    public Transform gazeTarget_2; // L'objet à regarder (Cube)

    private Animator anim_1;
    private Animator anim_2;

    public int distOfRay = 10;
    private RaycastHit _hit;

    //TODO: paring the obj and Anim
    // private Dictionary<GameObject, Animator> objectAnimators = new Dictionary<GameObject, Animator>();

    void Start()
    {
        if (gazeTarget_1 != null && gazeTarget_2 != null)
        {
            anim_1 = gazeTarget_1.GetComponent<Animator>();
            anim_2 = gazeTarget_2.GetComponent<Animator>();
        }
    }

    void Update()
    {
        bool isLookingAtTarget1 = IsLookingAtTarget(gazeTarget_1);
        bool isLookingAtTarget2 = IsLookingAtTarget(gazeTarget_2);

        if (isLookingAtTarget1 || isLookingAtTarget2)
        {
            gazeTimer += Time.deltaTime;
            Debug.Log("Regarde l'objet: " + gazeTimer); // ✅ Vérifie dans la console si ça s'incrémente

            if (gazeTimer >= fixationTime)
            {
                Debug.Log("=================Anim Success=================");
                if (isLookingAtTarget1)
                {
                    ActivateAnimation(anim_1);
                    gazeTimer = 0f; // Reset gazeTimer
                    isPlaying = true;
                    StartCoroutine(ReturnToIdle(anim_1));

                }
                else if (isLookingAtTarget2)
                {
                    ActivateAnimation(anim_2);
                    gazeTimer = 0f; // Reset gazeTimer
                    isPlaying = true;
                    StartCoroutine(ReturnToIdle(anim_2));
                }
                // Debug.Log("******************Teleport Success******************");
                // _hit.transform.gameObject.GetComponent<Teleport>().TeleportPlayer();
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
