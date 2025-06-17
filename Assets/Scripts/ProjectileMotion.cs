using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
   // public float swapSpeed = 2f;
    public float levitateHeight = 2f;
    public float levitateHeight2 = 2f;

    public float levitateTime;
    public float swapTime;

    private Vector3 cube1OriginalPos;
    private Vector3 cube2OriginalPos;

    // Start is called before the first frame update
    void Start()
    {
        cube1OriginalPos = cube1.transform.position;
        cube2OriginalPos = cube2.transform.position;

    }


    public void SwapCubes()
    {
        StartCoroutine(SwapCubesCoroutine(cube1.transform,cube2.transform,levitateTime,swapTime));
    }

    IEnumerator SwapCubesCoroutine(Transform cube1, Transform cube2, float levitateTime, float swapTime)
    {
        Swipee.canMove = false;
        // Calculate the positions of the cubes before and after the swap
        Vector3 originalPos1 = cube1.position;
        Vector3 originalPos2 = cube2.position;
        Vector3 targetPos1 = originalPos2;
        Vector3 targetPos2 = originalPos1;

        // Levitate the cubes up in their original positions
        float t = 0f;
        while (t < levitateTime)
        {
            t += Time.deltaTime;
            float y = Mathf.Lerp(originalPos1.y, originalPos1.y + levitateHeight, t / levitateTime);
            cube1.position = new Vector3(originalPos1.x, y, originalPos1.z);
            y = Mathf.Lerp(originalPos2.y, originalPos2.y + levitateHeight2, t / levitateTime);
            cube2.position = new Vector3(originalPos2.x, y, originalPos2.z);
            yield return null;
        }

        // Swap the positions of the cubes
        t = 0f;
        while (t < swapTime)
        {
            t += Time.deltaTime;
            float x = Mathf.Lerp(originalPos1.x, targetPos1.x, t / swapTime);
            float z = Mathf.Lerp(originalPos1.z, targetPos1.z, t / swapTime);
            cube1.position = new Vector3(x, cube1.position.y, z);
            x = Mathf.Lerp(originalPos2.x, targetPos2.x, t / swapTime);
            z = Mathf.Lerp(originalPos2.z, targetPos2.z, t / swapTime);
            cube2.position = new Vector3(x, cube2.position.y, z);
            yield return null;
        }

        // Levitate the cubes down in their new positions
        t = 0f;
        while (t < levitateTime)
        {
            t += Time.deltaTime;
            float y = Mathf.Lerp(targetPos1.y + levitateHeight, targetPos1.y, t / levitateTime);
            cube1.position = new Vector3(targetPos1.x, y, targetPos1.z);
            y = Mathf.Lerp(targetPos2.y + levitateHeight2, targetPos2.y, t / levitateTime);
            cube2.position = new Vector3(targetPos2.x, y, targetPos2.z);
            yield return null;
        }

        Swipee.canMove = true;
    }



}
