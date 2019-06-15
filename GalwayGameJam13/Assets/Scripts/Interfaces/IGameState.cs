
public interface IGameState
{
    void HandleInput(string key);
    void OnStateEnter();
    void OnStateExit();
}
