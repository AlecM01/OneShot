using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class pOnePistol : MonoBehaviour
{

    #region Settings
    public float speed = 20f;
    public float followSpedMulti = 20f;
    public float weponOffset = 150f;
    public float maxWeponDist = 0.7f;
    public float maxShots = 1;

    private float shotsP1 = 0;

    public float fixedWeponOffset = 0.7f;

    public KeyCode p1Fire = KeyCode.Mouse1;

    public Rigidbody2D projectile;
    public Rigidbody2D playerOne;

    private Vector2 mousePos;
    private Vector2 weponDist;

    bool firePressed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        shotsP1 = maxShots;
    }

    // Update is called once per frame
    void Update()
    {
        #region Cloner
        if (Input.GetKeyDown(p1Fire) & shotsP1 >= 1)
        {
            Rigidbody2D clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody2D;
            clone.velocity = transform.TransformDirection(Vector2.right * speed);

            shotsP1 -= 1;
        }
        #endregion

        #region Share info
        SendMessage("shotsLeftOne", shotsP1);
        #endregion
    }

    private void FixedUpdate()
    {

        #region gun movement
        float weponDist = Vector2.Distance(playerOne.position, transform.position);

        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        weponOffset = fixedWeponOffset / weponDist;

        transform.position = Vector2.Lerp(transform.position, mousePos, Time.fixedDeltaTime * weponOffset);
        transform.position = Vector2.Lerp(transform.position, playerOne.position, Time.fixedDeltaTime * followSpedMulti);

        transform.LookAt(mousePos);
        transform.Rotate(0, 90, 180);
        #endregion


    }

    void reloadPistol(float reload)
    {
        if (reload >= 1)
        {
            shotsP1 = maxShots;
        }
    }

}
