using UnityEngine;

public class BlocksColorManager : MonoBehaviour
{
    public Material mat;
    /*private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            other.GetComponent<MeshRenderer>().material = mat;
        }
    }

}
