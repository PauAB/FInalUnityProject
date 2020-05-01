using UnityEngine;

public class FirstPersonCharacter : MonoBehaviour, IEntity
{
    private const int NUM_ABILITIES = 1;

    [SerializeField]
    PlayerData Data;
    [SerializeField]
    public CharacterMove Move;

    private Ability[] mAbilities;
    private Projectile mProjectile;
    private float mReloadTime;
    private bool mReloading;
    private float mCurrentBullets;
    private float mTotalBullets;
    private float mShootCooldown;
    private bool mShooting;
    private float mShootTimer;

    private GameObject mPooledObject;

    public void EAwake()
    {
        SetCharacter();
    }

    public void EUpdate(float delta)
    {
        if (Input.GetMouseButtonDown(0))
        {
            mPooledObject = ObjectPooler.Instance.Trigger("Bullet", transform.position + transform.forward, Quaternion.identity, transform.forward);
        }
    }

    void SetCharacter()
    {
        mAbilities = new Ability[NUM_ABILITIES];
        for (int i = 0; i < NUM_ABILITIES; i++)
        {
            mAbilities[i] = Data.Abilities[i].GetComponent<Ability>();
            mAbilities[i].InitAbility(this);
            mAbilities[i].EAwake();
        }

        mReloadTime = Data.ReloadTime;
        mReloading = false;
        mCurrentBullets = Data.TotalBullets;
        mTotalBullets = Data.TotalBullets;
        mShootCooldown = Data.ShootCooldown;
        mShooting = false;
        mShootTimer = 0.0f;

        InputManager.SetInputs("UseAbility_1", mAbilities[0]);
        InputManager.SetInputs("UseAbility_2", mAbilities[1]);
        InputManager.SetInputs("UseAbility_3", mAbilities[2]);
    }
}
