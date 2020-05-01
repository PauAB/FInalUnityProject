using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public interface FloorMessage : IEventSystemHandler
{
    void GetFloorInfo(SpatialIndex.FLOOR_STATUS state);
}

public class SpatialIndex : MonoBehaviour
{
    
    public enum FLOOR_STATUS
    {
        DEFAULT = 1<<0,
        WATER = 1<<1,
        GRASS = 1<<2,
        PLAYER = 1<<3,
        LAVA = 1<<4
    }

    [SerializeField]
    float CubeSize = 1.0f;
    [SerializeField]
    int NumCubesX = 10;
    [SerializeField]
    int NumCubesZ = 10;

    private BoxCollider mCollider;
    private FLOOR_STATUS[] mFloor;
    private Vector3 mCenter;

    void Start()
    {
        mCollider = GetComponent<BoxCollider>();

        mCenter = transform.position - (transform.rotation * Vector3.right * NumCubesX / 2 * CubeSize) - (transform.rotation * Vector3.forward * NumCubesZ / 2 * CubeSize);

        NumCubesX = (int)((mCollider.size.x * transform.localScale.x) / CubeSize);
        NumCubesZ = (int)((mCollider.size.z * transform.localScale.z) / CubeSize);
        mFloor = new FLOOR_STATUS[NumCubesX * NumCubesZ];

        for (int i = 0; i < NumCubesX; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                mFloor[NumCubesX * i + j] |= FLOOR_STATUS.WATER;
            }
        }

        SerializeMap();
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor)
        {
            Gizmos.color = Color.yellow;
            Vector3 center = transform.position - (Vector3.right * NumCubesX / 2 * CubeSize) - (Vector3.forward * NumCubesZ / 2 * CubeSize);

            NumCubesX = (int)((GetComponent<BoxCollider>().size.x * transform.localScale.x) / CubeSize);
            NumCubesZ = (int)((GetComponent<BoxCollider>().size.z * transform.localScale.z) / CubeSize);

            for (int i = 0; i < NumCubesX; i++)
            {
                for (int j = 0; j < NumCubesZ; j++)
                {
                    if ((mFloor[i * NumCubesX + j] & FLOOR_STATUS.PLAYER) == FLOOR_STATUS.PLAYER)
                        Gizmos.color = Color.black;
                    else if ((mFloor[i * NumCubesX + j] & FLOOR_STATUS.WATER) == FLOOR_STATUS.WATER)
                        Gizmos.color = Color.blue;
                    else if ((mFloor[i * NumCubesX + j] & FLOOR_STATUS.GRASS) == FLOOR_STATUS.GRASS)
                        Gizmos.color = Color.green;
                    else if ((mFloor[i * NumCubesX + j] & FLOOR_STATUS.LAVA) == FLOOR_STATUS.LAVA)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.yellow;

                    Gizmos.DrawWireCube(transform.rotation * center
                        + transform.rotation * Vector3.right * CubeSize * i
                        + transform.rotation * Vector3.forward * j,
                        Vector3.one * CubeSize)
                        ;
                }
            }
        }
    }

    private void SerializeMap()
    {
        for (int i = 0; i < NumCubesX; i++)
        {
            for (int j = 0; j < NumCubesZ; j++)
            {
                if (Physics.CheckBox(mCenter
                    + transform.rotation * Vector3.right * CubeSize * i
                    + transform.rotation * Vector3.forward * j,
                    Vector3.one * CubeSize / 2.0f,
                    transform.rotation,
                    1 << 4))
                {
                    mFloor[NumCubesX * i + j] = FLOOR_STATUS.WATER;
                }
                else if (Physics.CheckBox(mCenter
                    + transform.rotation * Vector3.right * CubeSize * i
                    + transform.rotation * Vector3.forward * j,
                    Vector3.one * CubeSize / 2.0f,
                    transform.rotation,
                    1 << 9))
                {
                    mFloor[NumCubesX * i + j] = FLOOR_STATUS.LAVA;
                }
                else mFloor[NumCubesX * i + j] = FLOOR_STATUS.GRASS;
            }
        }

        GameObject.FindGameObjectsWithTag("Casters").ToList().ForEach(item => Destroy(item));
    }

    public FLOOR_STATUS GetFloorStatus(float x, float y)
    {
        Vector3 localScale = transform.localScale;
        float row = ((x - mCenter.x) / (mCollider.size.x * localScale.x));
        float col = ((y - mCenter.z) / (mCollider.size.z * localScale.z));
        int mapX = (int)(Mathf.Abs(row) * NumCubesX);
        int mapY = (int)(Mathf.Abs(col) * NumCubesZ);

        mFloor[NumCubesX * mapX + mapY] |= FLOOR_STATUS.PLAYER;

        return mFloor[NumCubesX * mapX + mapY];
    }

    public void GetFloorState(float x, float y, GameObject target)
    {
        Vector3 localScale = transform.localScale;
        float row = ((x - mCenter.x) / (mCollider.size.x * localScale.x));
        float col = ((y - mCenter.z) / (mCollider.size.z * localScale.z));
        int mapX = (int)(Mathf.Abs(row) * NumCubesX);
        int mapY = (int)(Mathf.Abs(col) * NumCubesZ);

        mFloor[NumCubesX * mapX + mapY] |= FLOOR_STATUS.PLAYER;

        ExecuteEvents.Execute<FloorMessage>(target, null, (a, b) => a.GetFloorInfo(mFloor[NumCubesX * mapX + mapY]));
    }
}
