using UnityEngine;

public class Damageable : DispatchatableComponent
{
    [SerializeField]
    float Life = 100f;

    public override void Dispatch(Message m)
    {
        DamageMessage mDmgMessage = ((DamageMessage)m);
        Life -= mDmgMessage.Damage;

        if (Life <= 0) gameObject.SetActive(false);
    }
}
