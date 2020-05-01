using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    static MessageManager instance = null;

    private DispatchatableComponent[] mDispatchatableComponents;
    private Stack<Message> mQ;
    private Dictionary<string, System.Type> mMessageTypes;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        mQ = new Stack<Message>();
        InitMessages();
    }

    private void InitMessages()
    {
        mMessageTypes = new Dictionary<string, Type>();
        mMessageTypes["Damage"] = typeof(DamageMessage);
    }

    public System.Type GetMessageType(string type)
    {
        return mMessageTypes[type];
    }

    private void Start()
    {
        mDispatchatableComponents = FindObjectsOfType<DispatchatableComponent>();
    }

    public static MessageManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        DispatchMessage();
    }

    public void SendMessage(Message m)
    {
        if (m.Receiver.GetComponent(m.SenderComp) == null) return;
        mQ.Push(m);
    }

    public void SendMessageToAll(Message m)
    {
        DispatchatableComponent[] receiverCmpnts = mDispatchatableComponents.Where(c => c.GetType() == m.SenderComp).ToArray();

        Message msg;
        foreach (DispatchatableComponent dc in receiverCmpnts)
        {
            msg = m.CreateCopy();
            msg.Receiver = dc.transform;

            mQ.Push(msg);
        }
    }

    public void DispatchMessage()
    {
        foreach (Message m in mQ)
        {
            ((DispatchatableComponent)m.Receiver.GetComponent(m.SenderComp)).Dispatch(m);
        }
        mQ.Clear();
    }
}
