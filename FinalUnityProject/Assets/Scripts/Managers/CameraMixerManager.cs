using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMixer))]
public class CameraMixerManager : MonoBehaviour
{
    [SerializeField]
    Camera[] Cameras;

    public float TimeBetweenBlend;

    private CameraMixer mMixer;
    private Coroutine mBlendCamerasCo;

    void Start()
    {
        mMixer = GetComponent<CameraMixer>();

        if (mBlendCamerasCo != null) StopCoroutine(mBlendCamerasCo);
        mBlendCamerasCo = StartCoroutine(BlendCamerasCo(TimeBetweenBlend));
    }

    void Update()
    {
        
    }

    private IEnumerator BlendCamerasCo(float waitTime)
    {
        mMixer.BlendCamera(Cameras[1], 10.0f, Interpolators.QuadIn);
        yield return new WaitForSeconds(waitTime);
        
        mMixer.BlendCamera(Cameras[2], 5.0f, Interpolators.QuadIn);
        yield return new WaitForSeconds(waitTime);
        
        mMixer.BlendCamera(Cameras[3], 5.0f, Interpolators.QuadIn);
        yield return new WaitForSeconds(waitTime);

        mMixer.BlendCamera(Cameras[4], 5.0f, Interpolators.QuadIn);
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(2f);

        Message m = new GameMessage(transform, GameManager.instance.transform, typeof(GameMessageReceiver), true);
        MessageManager.GetInstance().SendMessage(m);

        foreach (Camera cam in Cameras)
        {
            cam.gameObject.SetActive(false);
        }
    }
}
