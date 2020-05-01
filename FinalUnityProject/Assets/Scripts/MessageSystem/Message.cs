using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Message : MonoBehaviour
{
    public Transform Receiver;
    public Transform Sender;
    public System.Type SenderComp;
    public string MessageType;

    public Message() { }
    public Message(Message m) { }

    public abstract Message CreateCopy();    
}

public class DamageMessage : Message
{
    public float Damage;

    public DamageMessage() { }
    public DamageMessage(Transform send, Transform receiver, System.Type senderComp, float damage)
    {
        Sender = send;
        Receiver = receiver;
        SenderComp = senderComp;
        Damage = damage;
        MessageType = "Damage";
    }


    public override Message CreateCopy()
    {
        Message m = new DamageMessage(Receiver, Sender, SenderComp, Damage);
        return m;
    }

    public bool SetAndSendMessage(Transform send, Transform receiver, System.Type sendComp, float damage)
    {
        Sender = send;
        Receiver = receiver;
        SenderComp = sendComp;
        Damage = damage;

        if (receiver.GetComponent(sendComp) == null)
        {
            Debug.Log("Null");
            return false;
        }
        MessageManager.GetInstance().SendMessage(this);

        return true;
    }
}