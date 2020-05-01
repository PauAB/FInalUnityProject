using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMixer : MonoBehaviour
{
    [System.Serializable]
    public class MixedCamera
    {
        public Camera Cam;
        [SerializeField]
        public float BlendTime;
        [SerializeField]
        public float ElapsedTime;
        [SerializeField]
        [Range(0, 1)]
        public float Weight;
        [SerializeField]
        public float EffectiveWeight;
        [SerializeField]
        public bool Abandoned;

        public Interpolators.InterpolaltorFunc InterpolatorFunc;
    }

    private void LerpCameras(GameObject target, GameObject source, float ratio)
    {
        if (target == null || source == null) return;

        Transform trSource = source.transform;
        Transform trTarget = target.transform;
        Camera camSource = source.GetComponent<Camera>();
        Camera camTarget = target.GetComponent<Camera>();

        if (!camSource || !camTarget) return;

        Vector3 newPosition = Vector3.Lerp(trTarget.position, trSource.position, ratio);
        Vector3 newForward = Vector3.Lerp(trTarget.forward, trSource.forward, ratio);

        trTarget.position = newPosition;
        trTarget.forward = newForward;
    }

    [SerializeField]
    List<MixedCamera> MixedCameras;

    [SerializeField]
    Camera OutputCamera;
    [SerializeField]
    Camera DefaultCamera;

    void Update()
    {
        float remainingWeight = 1.0f;
        bool hasFallback = false;

        for (int i = 0; i < MixedCameras.Count; i++)
        {
            if (MixedCameras[i].ElapsedTime < MixedCameras[i].BlendTime)
            {
                MixedCameras[i].ElapsedTime += Mathf.Min(Time.deltaTime, MixedCameras[i].BlendTime - MixedCameras[i].ElapsedTime);
                MixedCameras[i].Weight = MixedCameras[i].ElapsedTime / MixedCameras[i].BlendTime;
            }

            MixedCameras[i].EffectiveWeight = Mathf.Min(remainingWeight, MixedCameras[i].Weight);

            remainingWeight -= MixedCameras[i].EffectiveWeight;

            MixedCameras[i].Abandoned = MixedCameras[i].Weight >= 1.0f & hasFallback;

            hasFallback = MixedCameras[i].EffectiveWeight >= 1.0f;
        }

        MixedCameras.RemoveAll(x => x.EffectiveWeight >= 1.0f);

        foreach (MixedCamera mc in MixedCameras)
        {
            float interpolatedWeight = mc.InterpolatorFunc != null ? mc.InterpolatorFunc(0.0f, 1.0f, mc.Weight) : mc.Weight;
            LerpCameras(OutputCamera.gameObject, mc.Cam.gameObject, interpolatedWeight);
        }

        Camera.SetupCurrent(OutputCamera);
    }

    public void BlendCamera(Camera cam, float blendTime, Interpolators.InterpolaltorFunc interpolatorFunc)
    {
        MixedCamera mc = new MixedCamera();
        mc.BlendTime = blendTime;
        mc.Cam = cam;
        mc.ElapsedTime = 0.0f;
        mc.Weight = blendTime <= 0.0f ? 1.0f : 0.0f;
        mc.InterpolatorFunc = interpolatorFunc;

        MixedCameras.Add(mc);
    }
}
