using UnityEngine;
using UnityEngine.EventSystems;

public interface ChangeLife : IEventSystemHandler
{
    void Damage(float dmg);
    void Healing(float healing);
}

public class MessageUnity : MonoBehaviour, ChangeLife
{
    [SerializeField]
    GameObject Target;

    

    void Start()
    {
        ExecuteEvents.Execute<ChangeLife>(Target, null, (a, b) => a.Damage(50));
        ExecuteEvents.Execute<ChangeLife>(gameObject, null, DamageMessage);
    }

    public void Damage(float dmg)
    {
        Debug.Log("Damage for " + dmg);
    }

    public void Healing(float healing)
    {
        Debug.Log("Healing for " + healing);
    }

    private void DamageMessage(ChangeLife handler, BaseEventData eventData)
    {
        handler.Damage(50);
    }
}
