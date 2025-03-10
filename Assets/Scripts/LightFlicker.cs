using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Start is called before the first frame updatepublic Material flickMat;
    public Material Glow;
    public float numberWang;


    void Start()
    {
    Glow = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        numberWang = Random.Range(0f,1f);
        //Debug.Log (numberWang);


        if(numberWang <=0.3f)
        Glow.SetColor("_EmissionColor", Color.blue);
        else
        {
            Glow.SetColor("_EmissionColor", Color.black);
        }
    }
}
