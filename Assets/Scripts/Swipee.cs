using System;
using System.Collections;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

public class Swipee : MonoBehaviour
{
    [SerializeField] private GameObject redCube, blueCube;
    [SerializeField] private float speed;
    [SerializeField] private float tiltAngle = 90.0f;
    private Vector3 targetPosition;
    private Vector3 targetPosition2;
    public static bool canMove = true;
    [SerializeField] private float moveTime = 0.5f;

    [SerializeField] private float moveHorizontalValue;
    [SerializeField] private float moveVerticalValue;

    private Vector2 startPos;

    [SerializeField] private LayerMask targetLayerMask;

    [SerializeField] private Camera gameCamera;

    private Vector3 offset;


    void Start()
    {
        canMove = true;
    }

    void Update()
    {
        //if (GameManager.levelCompleted == false)
        //{
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Save the touch position on touch start
                startPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipe = touch.position - startPos;

                calculateOffset(swipe);

                Vector3 worldStartPos = ScreenToWorldWithConstantY(startPos);
                Vector3 worldEndPos = ScreenToWorldWithConstantY(touch.position);

                worldStartPos += offset;

                // Calculate the direction vector between start and end positions
                Vector3 swipeDirection = (worldEndPos - worldStartPos).normalized;

                // Create a ray from the world start position along the swipe direction
                Ray ray = new Ray(worldStartPos, swipeDirection);

                // Perform a raycast
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayerMask))
                {
                    //AB YAHAN RED AUR BLUE CUBE KI CONDITION AEGI
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.CompareTag("RedCube"))
                    {
                        manageCubesSwipe(swipe, redCube, blueCube);
                    }
                    else if (hit.collider.gameObject.CompareTag("BlueCube"))
                    {
                        manageCubesSwipe(swipe, blueCube, redCube);
                    }
                }
                else
                {
                    //Debug.Log("No collider");
                }
            }
        }
        //}
    }


    IEnumerator MoveCubeUp(GameObject cube1, GameObject cube2)
    {
        canMove = false;
        float startTime = Time.time;

        //for cube1
        Vector3 startPosition = cube1.transform.position;
        Quaternion startRotation = cube1.transform.rotation;
        Quaternion rotationGoal = Quaternion.Euler(tiltAngle, 0, 0);
        Quaternion endRotation = Quaternion.Slerp(startRotation, rotationGoal, 1f);

        //for cube2
        Vector3 startPosition2 = cube2.transform.position;
        Quaternion startRotation2 = cube2.transform.rotation;
        Quaternion rotationGoal2 = Quaternion.Euler(-tiltAngle, 0, 0);
        Quaternion endRotation2 = Quaternion.Slerp(startRotation2, rotationGoal2, 1f);

        while (Time.time < startTime + moveTime)
        {
            float t = (Time.time - startTime) / moveTime;
            cube1.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            cube1.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            cube2.transform.position = Vector3.Lerp(startPosition2, targetPosition2, t);
            cube2.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t);


            yield return null;
        }
        cube1.transform.rotation = startRotation;
        cube2.transform.rotation = startRotation2;

        cube1.transform.position = targetPosition;
        cube2.transform.position = targetPosition2;

        canMove = true;

    }

    IEnumerator MoveCubeDown(GameObject cube1, GameObject cube2)
    {

        canMove = false;
        float startTime = Time.time;

        //for cube1
        Vector3 startPosition = cube1.transform.position;
        Quaternion startRotation = cube1.transform.rotation;
        Quaternion rotationGoal = Quaternion.Euler(-tiltAngle, 0, 0);
        Quaternion endRotation = Quaternion.Slerp(startRotation, rotationGoal, 1f);

        //for cube2
        Vector3 startPosition2 = cube2.transform.position;
        Quaternion startRotation2 = cube2.transform.rotation;
        Quaternion rotationGoal2 = Quaternion.Euler(tiltAngle, 0, 0);
        Quaternion endRotation2 = Quaternion.Slerp(startRotation2, rotationGoal2, 1f);

        while (Time.time < startTime + moveTime)
        {
            float t = (Time.time - startTime) / moveTime;
            cube1.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            cube1.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            cube2.transform.position = Vector3.Lerp(startPosition2, targetPosition2, t);
            cube2.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t);
            yield return null;
        }
        cube1.transform.rotation = startRotation;
        cube2.transform.rotation = startRotation2;

        cube1.transform.position = targetPosition;
        cube2.transform.position = targetPosition2;

        canMove = true;
    }

    IEnumerator MoveCubeLeft(GameObject cube1, GameObject cube2)
    {
        canMove = false;
        float startTime = Time.time;

        //for cube1
        Vector3 startPosition = cube1.transform.position;
        Quaternion startRotation = cube1.transform.rotation;
        Quaternion rotationGoal = Quaternion.Euler(0, 0, tiltAngle);
        Quaternion endRotation = Quaternion.Slerp(startRotation, rotationGoal, 1f);

        //for cube2
        Vector3 startPosition2 = cube2.transform.position;
        Quaternion startRotation2 = cube2.transform.rotation;
        Quaternion rotationGoal2 = Quaternion.Euler(0, 0, -tiltAngle);
        Quaternion endRotation2 = Quaternion.Slerp(startRotation2, rotationGoal2, 1f);

        while (Time.time < startTime + moveTime)
        {
            float t = (Time.time - startTime) / moveTime;
            cube1.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            cube1.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            cube2.transform.position = Vector3.Lerp(startPosition2, targetPosition2, t);
            cube2.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t);
            yield return null;
        }
        cube1.transform.rotation = startRotation;
        cube2.transform.rotation = startRotation2;

        cube1.transform.position = targetPosition;
        cube2.transform.position = targetPosition2;

        canMove = true;
    }

    IEnumerator MoveCubeRight(GameObject cube1, GameObject cube2)
    {
        canMove = false;
        float startTime = Time.time;

        //for cube1
        Vector3 startPosition = cube1.transform.position;
        Quaternion startRotation = cube1.transform.rotation;
        Quaternion rotationGoal = Quaternion.Euler(0, 0, -tiltAngle);
        Quaternion endRotation = Quaternion.Slerp(startRotation, rotationGoal, 1f);

        //for cube2
        Vector3 startPosition2 = cube2.transform.position;
        Quaternion startRotation2 = cube2.transform.rotation;
        Quaternion rotationGoal2 = Quaternion.Euler(0, 0, tiltAngle);
        Quaternion endRotation2 = Quaternion.Slerp(startRotation2, rotationGoal2, 1f);


        while (Time.time < startTime + moveTime)
        {
            float t = (Time.time - startTime) / moveTime;
            cube1.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            cube1.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            cube2.transform.position = Vector3.Lerp(startPosition2, targetPosition2, t);
            cube2.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t);
            yield return null;
        }
        cube1.transform.rotation = startRotation;
        cube2.transform.rotation = startRotation2;

        cube1.transform.position = targetPosition;
        cube2.transform.position = targetPosition2;

        canMove = true;
    }



    Vector3 ScreenToWorldWithConstantY(Vector2 screenPosition)
    {
        // Create a ray from the screen position
        Ray ray = gameCamera.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y, 0f));

        // Calculate the intersection point of the ray with the constant Y plane
        float t = (3f - ray.origin.y) / ray.direction.y;
        Vector3 worldPosition = ray.origin + t * ray.direction;

        return worldPosition;
    }

    void manageCubesSwipe(Vector2 swipe, GameObject cube1, GameObject cube2)
    {
        checkCanMove c = cube1.GetComponent<checkCanMove>();
        //Check the direction of the swipe
        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            // Left or right swipe
            if (swipe.x < 0 && canMove && c.checkMoveLeft())
            {
                // Left swipe
                targetPosition = cube1.transform.position + new Vector3(-moveHorizontalValue, 0, 0);
                targetPosition2 = cube2.transform.position + new Vector3(moveHorizontalValue, 0, 0);
                StartCoroutine(MoveCubeLeft(cube1, cube2));
            }
            else if (swipe.x > 0 && canMove && c.checkMoveRight())
            {
                // Right swipe
                targetPosition = cube1.transform.position + new Vector3(moveHorizontalValue, 0, 0);
                targetPosition2 = cube2.transform.position + new Vector3(-moveHorizontalValue, 0, 0);

                StartCoroutine(MoveCubeRight(cube1, cube2));
            }
        }
        else
        {
            // Up or down swipe
            if (swipe.y < 0 && canMove && c.checkMoveDown())
            {
                // Down swipe
                targetPosition = cube1.transform.position + new Vector3(0, 0, -moveVerticalValue);
                targetPosition2 = cube2.transform.position + new Vector3(0, 0, moveVerticalValue);
                StartCoroutine(MoveCubeDown(cube1, cube2));
            }
            else if (swipe.y > 0 && canMove && c.checkMoveUp())
            {
                // Up swipe
                targetPosition = cube1.transform.position + new Vector3(0, 0, moveVerticalValue);
                targetPosition2 = cube2.transform.position + new Vector3(0, 0, -moveVerticalValue);
                StartCoroutine(MoveCubeUp(cube1, cube2));
            }

        }
    }


    void calculateOffset(Vector2 swipe)
    {
        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            // Left or right swipe
            if (swipe.x < 0)
            {
                // Left swipe
                offset = new Vector3(3f, 0, 0);
            }
            else if (swipe.x > 0)
            {
                // Right swipe
                offset = new Vector3(-3f, 0, 0);
            }
        }
        else
        {
            // Up or down swipe
            if (swipe.y < 0)
            {
                // Down swipe
                offset = new Vector3(0, 0, 3f);
            }
            else if (swipe.y > 0)
            {
                // Up swipe
                offset = new Vector3(0, 0, -3f);
            }

        }
    }

}
