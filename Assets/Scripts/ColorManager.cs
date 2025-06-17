using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Material redMat;
    public Material blueMat;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }


    private void onTriggerEnter(Collider other)
    {
        if(other.tag == "RedCube" )
        {
            meshRenderer.material = redMat;
        }
        if(other.tag == "BlueCube" )
        {
            meshRenderer.material = blueMat;
        }
    }
}
