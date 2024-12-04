using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroController : PlayerController
{
    


    protected override float GetHorizontalInput()
    {
        // Reverse horizontal input to invert Bro's controls
        if (Input.GetKey(KeyCode.LeftArrow)) return -1f;  // Move right
        if (Input.GetKey(KeyCode.RightArrow)) return 1f; // Move left
        return 0f;
    }

    protected override void PlayMovementAnimation(float horizontal)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Flip Bro's sprite based on inverted controls
        Vector3 scale = transform.localScale;
        if (horizontal > 0) // Moving right
        {
            scale.x = -Mathf.Abs(scale.x); // Face left (default)
        }
        else if (horizontal < 0) // Moving left
        {
            scale.x = Mathf.Abs(scale.x); // Face right
        }
        transform.localScale = scale;

        // Handle jumping animation
        playerAnimator.SetBool("Jump", !isGrounded);
    }

    protected override bool GetJumpInput() => Input.GetKeyDown(KeyCode.UpArrow);

    protected override bool GetKickInputStraight() => Input.GetKeyDown(KeyCode.Space);

    protected override bool GetKickInputChip() => Input.GetKeyDown(KeyCode.B);

    protected override bool GetKickInputSmash() => Input.GetKeyDown(KeyCode.M);

}
