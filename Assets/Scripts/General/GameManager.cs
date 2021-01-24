using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Proto Settings
    [SerializeField] private MainPlayer _player;
    [SerializeField] private WorldEnemy _enemy;
    [SerializeField] private BoardSettings _localBoardSettings;
    [SerializeField] private BoardSettings _enemyBoardSettings;
    [SerializeField] private Transform _battlePoint;

    private void Start()
    {
        // Test
        StartFight();
    }

    private void StartFight()
    {
        BattleSettings settings = CreateBattleSettings(_player, _enemy);

        GameEvents.OnStartFight.Invoke(settings);
    }

    #region Helpers

    private BattleSettings CreateBattleSettings(MainPlayer player, WorldEnemy enemy)
    {
        BattleSettings settings = new BattleSettings(player, enemy);
        settings.SetBoardSettings(_localBoardSettings, _enemyBoardSettings);
        settings.battlePoint = _battlePoint.position;

        return settings;
    }

    #endregion
}
