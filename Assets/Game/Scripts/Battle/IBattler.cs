
namespace Game.Common
{
    public interface IBattler
    {
        void SetOpponent(IBattler battler);
    }

    public interface IOnBattleStart
    {
        void BattleStart();
    }
    
    public interface IOnDamaged
    {
        void Damaged(int damage);
    }

    public interface IOnDeath
    {
        bool IsDeath { get; }

        void Death();
    }

    public interface IOnStartMyTurn
    {
        void StartMyTurn();
    }

    public interface IOnLostMyTurn
    {
        void LostMyTurn();
    }

    public interface IOnAttack
    {
        void Attack();
    }

    public interface IOnHandLoose
    {
        void HandLoose();
    }
}
