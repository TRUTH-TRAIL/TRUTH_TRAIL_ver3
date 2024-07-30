namespace TT
{
    public interface ICurse
    {
        string Description { get; }
        bool CanActivate();
        void Activate();
    }
}
