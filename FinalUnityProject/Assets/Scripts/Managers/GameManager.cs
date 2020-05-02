using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static void Log(object item)
    {
#if _DEBUG
        UnityEngine.Debug.Log(item);
#endif
    }

    public static GameManager instance = null;
#if _CONSOLE
    public ConsoleManager console;
#endif

    [SerializeField]
    List<IEntity> Entities;

    [SerializeField]
    GameObject Character;

    public GameObject SpawnPos;

    void Awake()
    {
        StackFrame callStack = new StackFrame(1, true);

        if (instance == null) instance = this;
        else Destroy(this);

#if _CONSOSLE
        console = gameObject.AddComponent<ConsoleManager>();
        ConsoleManager.instance = console;
#endif

        Entities = new List<IEntity>();
        var entitiesI = FindObjectsOfType<MonoBehaviour>().OfType<IEntity>();

        foreach (IEntity ent in entitiesI)
        {
            Entities.Add(ent);
        }
    }

    void Start()
    {
        foreach (IEntity ent in Entities)
        {
            ent.EAwake();
        }
    }

    void Update()
    {
        foreach (IEntity ent in Entities)
        {
            if ((ent as MonoBehaviour).enabled)
                ent.EUpdate(Time.deltaTime);
        }
    }

    public void InstantiateCharacter()
    {
        GameObject character = Instantiate(Character);
        character.transform.position = SpawnPos.transform.position;

        FindNewEntities();
    }

    public void ActivateCharacter()
    {
        Character.SetActive(true);

        FindNewEntities();
    }

    private void FindNewEntities()
    {
        var entities = FindObjectsOfType<MonoBehaviour>().OfType<IEntity>();
        foreach (IEntity ent in entities)
        {
            Entities.Add(ent);
        }
    }
}
