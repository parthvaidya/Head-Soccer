using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : PlayerController
{
    protected override float GetHorizontalInput()
    {
        if (Input.GetKey(KeyCode.A)) return -1f;
        if (Input.GetKey(KeyCode.D)) return 1f;
        return 0f;
    }

    protected override bool GetJumpInput() => Input.GetKeyDown(KeyCode.W);

    protected override bool GetKickInputStraight() => Input.GetKeyDown(KeyCode.LeftShift);

    protected override bool GetKickInputChip() => Input.GetKeyDown(KeyCode.E);

    protected override bool GetKickInputSmash() => Input.GetKeyDown(KeyCode.Q);
}
