using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// AJOUTER CETTE BIBLIOTHEQUE POUR POUVOIR NAVIGUER ENTRE LES SCENES :
using UnityEngine.SceneManagement;


public class gotosceneDetection : MonoBehaviour
{
   
	public GameObject player;
	public GameObject maporte;
	public string mascene;
	public float distance;

   // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Go ! ");
    }

    // Update is called once per frame
    void Update()
    {
		
		
		// On mesure la distance entre les Deux objets
		float dist = Vector3.Distance(player.transform.position, maporte.transform.position);
		Debug.Log("=================Dist==== " + dist);

		// Si la distance est inférieure à 3, on quitte la scene
		if(dist<distance){  
				LoadLevel(mascene);
		}
    }
	
	



	
	
	// Chargement de la scene en fonction d'une variable chaine de caractère : lvl
	public void LoadLevel(string lvl){ 
		SceneManager.LoadScene(lvl);	
	}
	

	
}
