using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Entity entity;
    private Vector2 moveInputs = Vector2.zero;
    public InputThings pInputs;

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
    }

    private void FixedUpdate()
    {
        // Gather Input
        moveInputs = pInputs.Player.Move.ReadValue<Vector2>();

    }
}
