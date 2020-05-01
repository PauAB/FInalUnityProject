using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    public delegate void ExecuteFunction();

    private Dictionary<KeyCode, Command> mInputs;
    private Dictionary<string, Command> mKeyInputs;

    protected virtual void Awake()
    {
        Assert.IsTrue(FindObjectsOfType<InputManager>().Length<2,
            GetDebugStrLine(31) + "GameManager has more than 1 copy. It's a singleton Class and there can only be one GameManager for each game");
        Assert.raiseExceptions = true;

        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        mInputs = new Dictionary<KeyCode, Command>();
        mKeyInputs = new Dictionary<string, Command>();
    }

    public static void SetInputs(KeyCode code, Command func)
    {
        if (instance != null) instance.mInputs[code] = func;
    }

    public static void SetInputs(string code, Command func)
    {
        if (instance != null) instance.mKeyInputs[code] = func;
    }

    private void Update()
    {
        foreach (KeyCode k in mInputs.Keys)
        {
            if (Input.GetKeyDown(k))
                mInputs[k].Execute();
        }

        foreach (string k in mKeyInputs.Keys)
        {
            if (Input.GetButton(k))
                mKeyInputs[k].Execute();
            if (Input.GetButtonUp(k))
                mKeyInputs[k].Up();
        }
    }

    private string GetDebugStrLine(int numLine)
    {
        string str = "GameManager.cs ---- line " + numLine + " :";
        return str;
    }
}
