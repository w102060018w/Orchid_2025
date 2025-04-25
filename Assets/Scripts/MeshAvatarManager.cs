using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAvatarManager : MonoBehaviour
{
    // public Mesh avatarMeshes;
    // public Material avatarMaterials;
    // [Header("FULL AVATAR PREFABS ONLY (No Meshes, No Materials)")]
    [SerializeField] private Mesh[] avatarMeshes;
    [SerializeField] private Material[] avatarMaterials;

    public GameObject readyToBeReplaced;
    private SkinnedMeshRenderer smr;
    // private GameObject currentAvatar;

    private void Start()
    {
        // if (SceneTransferData.shouldSpawnAvatar)
        // {
        //     SpawnRandomAvatar();
        //     SceneTransferData.shouldSpawnAvatar = false; // Reset the flag (the flag will only be activated while the sceneNEWS end)
        // }
    }

    private void Update()
    {
        if (SceneTransferData.shouldSpawnAvatar)
        {
            SpawnRandomAvatar();
            // ReplaceAvatar();
            SceneTransferData.shouldSpawnAvatar = false; // Reset the flag (the flag will only be activated while the sceneNEWS end)
        }  
    }

    public void SpawnRandomAvatar()
    {
        // int index = Random.Range(0, avatarMeshes.Length);
        Debug.Log("promptsType (prepare to change avatar) = "+SceneTransferData.promptsType);
        ReplaceAvatar(SceneTransferData.promptsType);
    }

    public void ReplaceAvatar(int idx)
    {
        smr = readyToBeReplaced.GetComponent<SkinnedMeshRenderer>();

        if(smr != null){
            smr.sharedMesh = avatarMeshes[idx];
            smr.materials = new Material[] { avatarMaterials[idx], avatarMaterials[idx] };
            // smr.sharedMesh = avatarMeshes;
            // smr.materials[0] = avatarMaterials;
            // smr.materials[1] = avatarMaterials;
        }
    }

    // public void ReplaceAvatar()
    // {
    //     // // Ensure the SkinnedMeshRenderer component is assigned
    //     // GameObject parenttargetObject = GameObject.Find("test_armature_bcp");
    //     // if(parenttargetObject != null){
    //     //     Transform childTrans = parenttargetObject.transform.Find("Armature_mesh");
            
    //     //     if (childTrans != null){
    //     //         GameObject targetObject = childTrans.gameObject;
    //     //         smr = targetObject.GetComponent<SkinnedMeshRenderer>();
    //     //         Debug.Log("Found target !!!!");
    //     //     }else{
    //     //         Debug.Log("NOTTTTTTTTTTTT Found target !!!!");
    //     //     }

    //     //     if(smr != null){
    //     //         smr.sharedMesh = avatarMeshes;
    //     //         smr.materials[0] = avatarMaterials;
    //     //     }
    //     // } 
    //     smr = readyToBeReplaced.GetComponent<SkinnedMeshRenderer>();

    //     if(smr != null){
    //         smr.sharedMesh = avatarMeshes;
    //         smr.materials[0] = avatarMaterials;
    //     }
    // }
}



