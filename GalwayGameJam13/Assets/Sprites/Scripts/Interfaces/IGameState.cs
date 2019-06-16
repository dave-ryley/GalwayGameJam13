
public interface IGameState
{
    void HandleKeyDown(string key);
    void HandleKeyHold(string key);
    void HandleKeyUp(string key);
    void OnStateEnter();
    void OnStateExit();
    void OnStateUpdate(float deltaTime);
}
