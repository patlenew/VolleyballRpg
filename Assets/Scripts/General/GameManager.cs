using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Proto Settings
    [SerializeField] private MainPlayer _player;
    [SerializeField] private WorldEnemy _enemy;
    [SerializeField] private BoardSettings _localBoardSettings;
    [SerializeField] private BoardSettings _enemyBoardSettings;

    // TO IMPLEMENT, try to find an available battle point around where the fight was declared on the world
    [SerializeField] private Transform _battlePoint; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Test
            StartFight();
        }
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
