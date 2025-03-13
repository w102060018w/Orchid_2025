using UnityEngine;
using System.Collections;
// integration Cube's EyeTracking DONE + Pushaway 3D particles DONE

public class EyeTrackingActivation : MonoBehaviour
{
    public float fixationTime = 5f; // Temps avant activation
    private float gazeTimer = 0f;
    private bool isPlaying = false;

    public Transform gazeTarget; // L'objet à regarder (Cube)

    private Animator anim;

    public int distOfRay = 10;
    private RaycastHit _hit;

    //TODO: paring the obj and Anim
    // private Dictionary<GameObject, Animator> objectAnimators = new Dictionary<GameObject, Animator>();

    void Start()
    {
        if (gazeTarget != null)
            anim = gazeTarget.GetComponent<Animator>();
    }

    void Update()
    {
        if (IsLookingAtTarget())
        {
            gazeTimer += Time.deltaTime;
            Debug.Log("Regarde l'objet: " + gazeTimer); // ✅ Vérifie dans la console si ça s'incrémente

            if (gazeTimer >= fixationTime)
            {
                Debug.Log("=================Anim Success=================");
                ActivateAnimation();
                gazeTimer = 0f; // Reset gazeTimer
                isPlaying = true;
                StartCoroutine(ReturnToIdle());

                Debug.Log("******************Teleport Success******************");
                _hit.transform.gameObject.GetComponent<Teleport>().TeleportPlayer();
            }
        }
        else
        {
            gazeTimer = 0f;
        }
    }

    private bool IsLookingAtTarget()
    {
        //TODO/ replace it with 'raycast detection' (in order to work on multiple objects EyeTracking)
        Vector3 direction = gazeTarget.position - Camera.main.transform.position;
        direction.Normalize();
        float dot = Vector3.Dot(Camera.main.transform.forward, direction);

        Debug.Log("Angle de regard: " + dot); // ✅ Vérifie dans la console si le regard est bien détecté

        return dot > 0.95f; // L'objet est bien dans l'axe de vision
    }

    private void ActivateAnimation()
    {
        if (anim != null)
        {
            Debug.Log("Animation activée !");
            anim.SetTrigger("Activate");
        }
        else
        {
            Debug.LogWarning("Aucun Animator trouvé sur l'objet !");
        }
    }

    private IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.Play("Idle");
        isPlaying = false;
    }
}
