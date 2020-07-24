using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playerShootDetermin : MonoBehaviour
{
    #region Settings
    private float p1Remaining = 0;
    private float p2Remaining = 0;

    private float reloadLimit = 1;
    private float reloadStart = 0;

    private float reload = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        reload = reloadStart;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (p1Remaining >= 1 & p2Remaining >= 1)
        {
            reload = 1;
        }
        if (p1Remaining <= 0 | p2Remaining <= 0)
        {
            reload = 0;
        }

        gameObject.SendMessage("reloadPistol", reload);

    }

    #region Check for shots left
    void shotsLeftOne(float shotsP1)
    {
        if (shotsP1 <= 0)
        {
            p1Remaining = 0;
        }
        if (shotsP1 >= 1)
        {
            p1Remaining = 1;
        }
    }
    void shotsLeftTwo(float shotsP2)
    {
        if (shotsP2 <= 0)
        {
            p2Remaining = 0;
        }
        if (shotsP2 >= 1)
        {
            p2Remaining = 1;
        }
    }
    #endregion
}
