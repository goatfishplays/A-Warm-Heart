using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Entity entity;
    private Vector2 moveInputs = Vector2.zero;
    public InputThings pInputs;
    public SpawnerManager spaManL;
    public SpawnerManager spaManR;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();
        pInputs = new InputThings();
        pInputs.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {

        #region Movement
        if (entity.canMove)
        {
            // #region Animator 
            // if (moveInputs != Vector2.zero)
            // {
            //     entity.animator.SetFloat("XVelo", moveInputs.x);
            //     entity.animator.SetFloat("YVelo", moveInputs.y);

            // }
            // #endregion

            #region Calculate Move Speed
            entity.moveSpeedMult = 1f;
            // Apply modifiers
            #endregion

            // Planar Movement
            entity.Move(moveInputs);

        }
        else
        {
            entity.Move(Vector2.zero);
        }

        #endregion 

        #region Aiming
        Vector2 aim = (Vector2)Camera.main.ScreenToWorldPoint(pInputs.Player.Look.ReadValue<Vector2>());
        // print(aim);
        spaManL.AimAll(aim);
        spaManR.AimAll(aim);
        #endregion

        #region Firing
        if (pInputs.Player.Fire.IsPressed())
        {
            spaManL.FireAll();
        }
        if (pInputs.Player.AltFire.IsPressed())
        {
            spaManR.FireAll();
        }
        #endregion
    }

    private void FixedUpdate()
    {
        // Gather Input
        moveInputs = pInputs.Player.Move.ReadValue<Vector2>();

    }
}
