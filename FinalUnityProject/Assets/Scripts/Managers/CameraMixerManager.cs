using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMixer))]
public class CameraMixerManager : MonoBehaviour
{
    [SerializeField]
    Camera[] Cameras;

    private CameraMixer mMixer;

    void Start()
    {
        mMixer = GetComponent<CameraMixer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            mMixer.BlendCamera(Cameras[0], 10.0f, Interpolators.BounceInOut);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mMixer.BlendCamera(Cameras[1], 5.0f, Interpolators.CircularIn);
        }
        if (Input.GetKey(KeyCode.D))
        {
            mMixer.BlendCamera(Cameras[2], 3.0f, Interpolators.ExpoIn);
        }
    }
}
