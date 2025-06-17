using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCanMove : MonoBehaviour
{
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool canMoveUp;

    public bool checkMoveUp()
    {
        return canMoveUp;
    }

    public bool checkMoveDown()
    {
        return canMoveDown;
    }

    public bool checkMoveRight()
    {
        return canMoveRight;
    }

    public bool checkMoveLeft()
    {
        return canMoveLeft;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftBorder"))
        {
            canMoveLeft = false;
        }
        if (other.CompareTag("RightBorder"))
        {
            canMoveRight = false;
        }
        if (other.CompareTag("TopBorder"))
        {
            canMoveUp = false;
        }
        if (other.CompareTag("BottomBorder"))
        {
            canMoveDown = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("LeftBorder"))
        {
            canMoveLeft = true;
        }
        if (other.CompareTag("RightBorder"))
        {
            canMoveRight = true;
        }
        if (other.CompareTag("TopBorder"))
        {
            canMoveUp = true;
        }
        if (other.CompareTag("BottomBorder"))
        {
            canMoveDown = true;
        }
    }

 
}
