
public class Ability : Command, IEntity
{
    protected FirstPersonCharacter mCharacter;
    protected float mCooldownTime;
    protected float mAbilityTimer;

    public virtual void EAwake()
    {
        
    }

    public void InitAbility(FirstPersonCharacter character)
    {
        mCharacter = character;
    }

    public virtual void EUpdate(float delta)
    {
        
    }

    public void UseAbility() { }    
}
