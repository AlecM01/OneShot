using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class pTwoPistol : MonoBehaviour
{

    #region Settings
    public float speed = 20f;
    public float followSpedMulti = 20f;
    public float weponOffset = 150f;
    public float maxWeponDist = 0.7f;
    public float maxShots = 1;

    private float shotsP2 = 0;

    public float fixedWeponOffset = 0.7f;

    public KeyCode p2Fire = KeyCode.Mouse0;

    public Rigidbody2D projectile;
    public Rigidbody2D playerTwo;

    private Vector2 mousePos;
    private Vector2 weponDist;

    bool firePressed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        shotsP2 = maxShots;
    }

    // Update is called once per frame
    void Update()
    {
        #region Cloner
        if (Input.GetKeyDown(p2Fire) & shotsP2 >= 1)
        {
            Rigidbody2D clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody2D;
            clone.velocity = transform.TransformDirection(Vector2.right * speed);

            shotsP2 -= 1;
        }
        #endregion

        #region Share info
        SendMessage("shotsLeftTwo", shotsP2);
        #endregion
    }

    private void FixedUpdate()
    {

        #region gun movement
        float weponDist = Vector2.Distance(playerTwo.position, transform.position);

        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        weponOffset = fixedWeponOffset / weponDist;

        transform.position = Vector2.Lerp(transform.position, mousePos, Time.fixedDeltaTime * weponOffset);
        transform.position = Vector2.Lerp(transform.position, playerTwo.position, Time.fixedDeltaTime * followSpedMulti);

        transform.LookAt(mousePos);
        transform.Rotate(0, 90, 180);
        #endregion


    }

    void reloadPistol(float reload)
    {
        if (reload >= 1)
        {
            shotsP2 = maxShots;
        }
    }

}
