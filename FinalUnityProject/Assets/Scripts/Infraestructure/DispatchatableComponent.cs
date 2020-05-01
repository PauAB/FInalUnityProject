using UnityEngine;

public abstract class DispatchatableComponent : MonoBehaviour
{
    public abstract void Dispatch(Message m);
}
