public interface IHealth
{
    public void ReduceHealth(float amount);
    public void IncreaseHealth(float amount, bool canOverHeal);
}
