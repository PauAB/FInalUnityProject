using UnityEngine;

public class Damageable : DispatchatableComponent
{
    [SerializeField]
    float Life = 100f;

    DamageMessage mDmgMessage;

    public override void Dispatch(Message m)
    {
        mDmgMessage = ((DamageMessage)m);
        Life -= mDmgMessage.Damage;

        mDmgMessage.Sender.gameObject.SetActive(false);
    }
}
