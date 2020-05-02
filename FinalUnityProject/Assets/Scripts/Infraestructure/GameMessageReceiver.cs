using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageReceiver : DispatchatableComponent
{
    public override void Dispatch(Message m)
    {
        GameMessage gameMsg = ((GameMessage)m);

        GameManager.instance.ActivateCharacter();
    }
}
