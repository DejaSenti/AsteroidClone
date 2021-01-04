public interface IGameManager
{
    void Initialize(int level);
    void Terminate();
    void TerminateSubordinates();
}