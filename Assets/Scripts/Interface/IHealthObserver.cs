namespace Interface
{
    public interface IHealthObserver
    {
        public void OnHealthIncreased(int beforeHp, int currentHp);
        public void OnHealthDecreased(int beforeHp, int currentHp);
    }
}