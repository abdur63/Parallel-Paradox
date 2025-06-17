using UnityEngine;

public class CircleMotion : MonoBehaviour
{
    public float rotateSpeed = 1f; // Speed of rotation
    public float moveSpeed = 1f; // Speed of movement
    public float radius = 2f; // Radius of the circle
    public Transform centerPoint;
    private Vector3 center; // Center of the circle
    private float angle; // Angle of rotation

    void Start()
    {
        center = centerPoint.transform.position; // Set center of the circle to the current position of the cube
    }

    void Update()
    {
        angle += rotateSpeed * Time.deltaTime; // Update angle of rotation
        //Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius; // Calculate offset from the center based on angle and radius
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius; // Calculate offset from the center based on angle and radius
        transform.position = center + offset; // Move the cube to the new position
        //transform.position += offset; // Move the cube to the new position

        //transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); // Rotate the cube around its y-axis
        transform.Rotate(Vector3.right * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * moveSpeed* Time.deltaTime);
    }
}
