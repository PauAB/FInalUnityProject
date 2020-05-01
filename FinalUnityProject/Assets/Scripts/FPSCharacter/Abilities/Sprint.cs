using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : Ability
{
    [SerializeField]
    float SprintVelocity = 0f;
    [SerializeField]
    float BaseVelocity;

    public override void Up()
    {
        GameManager.Log("End Sprint");
        mCharacter.Move.Speed = BaseVelocity;
    }    

    public override void EAwake()
    {
        mCooldownTime = 0f;
        BaseVelocity = mCharacter.Move.Speed;
        SprintVelocity = BaseVelocity * 2.0f;
    }

    public override void Execute()
    {
        GameManager.Log("Start Sprint");
        mCharacter.Move.Speed = SprintVelocity;
    }
}