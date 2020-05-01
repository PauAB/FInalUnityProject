using System.Collections;
using UnityEngine;

[System.Serializable]
public class CharacterMove : MonoBehaviour, IEntity, FloorMessage
{
    public static bool CheckWithFloor(int mask, int maskToCompare)
    {
        return (mask & maskToCompare) == maskToCompare;
    }

    [SerializeField]
    CharacterController Controller;
    [SerializeField]
    public float Speed = 12f;
    [SerializeField]
    bool FlyMode = false;
    [SerializeField]
    SpatialIndex Spatial;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    public float JumpHeight = 3.0f;

    private Vector3 mVelocity;
    private bool mIsGrounded;

    void IEntity.EAwake()
    {

    }

    void IEntity.EUpdate(float delta)
    {
        mIsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (mIsGrounded && mVelocity.y < 0)
        {
            mVelocity.y = -2f;

            Spatial.GetFloorState(transform.position.x, transform.position.z, gameObject);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Controller.Move(move * Speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && mIsGrounded)
            mVelocity.y = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);

        if (!FlyMode)
        {
            mVelocity += Physics.gravity * Time.deltaTime;
            Controller.Move(mVelocity * Time.deltaTime);
        }
    }

    private IEnumerator BurnCo(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void GetFloorInfo(SpatialIndex.FLOOR_STATUS state)
    {
        if (CheckWithFloor((int)state, (int)SpatialIndex.FLOOR_STATUS.WATER))
            Speed = 6.0f;
        else if (CheckWithFloor((int)state, (int)SpatialIndex.FLOOR_STATUS.LAVA))
        {
            Speed = 6.0f;
            Controller.Move(Vector3.up * 50.0f * Time.deltaTime);
        }
        else Speed = 12.0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GroundCheck.position, GroundDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
