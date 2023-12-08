namespace _Game.UI
{
    public interface IIndicator<T>
    {
        T Value { get; }
        event System.Action<T> ValueChanged;
    }
}