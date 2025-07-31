using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossController : MonoBehaviour
{
    private bool isFacingRight;
    private Transform healthBarHUD;

    void Update()
    {
        FacingRight();
    }

    void FacingRight()
    {
        if (isFacingRight)
        {
            healthBarHUD.localEulerAngles = Vector2.up * 180;
            transform.eulerAngles = Vector2.up * 180;
            isFacingRight = false;
        }
        else
        {
            healthBarHUD.localEulerAngles = Vector2.zero;
            transform.eulerAngles = Vector2.zero;
            isFacingRight = true;
        }
    }
    
}
