using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public Entity entity;
    private Vector2 moveInputs = Vector2.zero;
    public InputThings pInputs;
    public SpawnerManager spaManL;
    public SpawnerManager spaManR;
    public MenuManager menuManager;
    public int numDropRolls = 1;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        entity = GetComponent<Entity>();
        pInputs = new InputThings();
        pInputs.Player.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
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

            // #region Calculate Move Speed
            // entity.moveSpeedMult = 1f;
            // // Apply modifiers 
            // #endregion

            // Planar Movement
            entity.Move(moveInputs);


        }
        else
        {
            entity.Move(Vector2.zero);
        }

        #endregion 

    }

    private void Update()
    {
        // Gather Input
        moveInputs = pInputs.Player.Move.ReadValue<Vector2>();

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
        else if (pInputs.Player.Fire.WasReleasedThisFrame())
        {
            spaManL.FireAll(false);
        }

        if (pInputs.Player.AltFire.IsPressed())
        {
            spaManR.FireAll();
        }
        else if (pInputs.Player.AltFire.WasReleasedThisFrame())
        {
            spaManR.FireAll(false);
        }
        #endregion

        // // Upgrades Menu 
        // if (pInputs.Player.UpgradeMenu.WasPressedThisFrame())
        // {
        //     menuManager.ToggleUpgradeMenu();
        // }

        // Dash
        if (pInputs.Player.Dash.WasPressedThisFrame())
        {
            entity.Dash(moveInputs);
        }
    }
}
