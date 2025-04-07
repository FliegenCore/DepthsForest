using System.Collections;
using Assets;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class BattleController : MonoBehaviour, IService
    {
        private Player _playerBattler;
        private NPC _enemyBattler;

        private IBattler _currentBattler;
        private SpawnManager _battleManager;
        
        public bool CanDestroyed => true;
        public Player Player => _playerBattler;
        public NPC Enemy => _enemyBattler;
        
        public void Initialize()
        {
            _battleManager = FindObjectOfType<SpawnManager>();
            
            var playerAsset = AssetLoader.LoadSync<Player>("Player/Player");
            _playerBattler = Instantiate(playerAsset, _battleManager.PlayerSpawnPoint.transform);
            
            SpawnEnemy();
            
            _playerBattler.SetOpponent(_enemyBattler);
            _enemyBattler.SetOpponent(_playerBattler);
            
            _playerBattler.DisableHealth();
            _enemyBattler.DisableHealth();
            
            G.Get<BattleUIController>().DisableBattleWindow();

            if (_enemyBattler is IOnBattleStart battleStart)
            {
                battleStart.BattleStart();
            }
            else
            {
                ChangeTurn();
            }
        }
        
        public void ChangeTurn()
        {
            if (_currentBattler == null)
            {
                _playerBattler.EnableHealth();
                _enemyBattler.EnableHealth();
                TakeTurn(_playerBattler);
                return;
            }
            
            if (_currentBattler is Player)
            {
                if(!_enemyBattler.IsDeath)
                    TakeTurn(_enemyBattler);
            }
            else
            {
                if(!_playerBattler.IsDeath)
                    TakeTurn(_playerBattler);
            }
        }

        public IEnumerator EndFight(float time)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("-------End-----");
            G.Get<FadeController>().FadeIn(() =>
            {
                G.Get<SceneController>().ChangeScene(SceneName.MapScene);
                G.Get<FadeController>().FadeOut();
            });
        }

        private void TakeTurn(IBattler battler)
        {
            if (_currentBattler is IOnLostMyTurn lost)
            {
                lost.LostMyTurn();
            }
            
            _currentBattler = battler;

            if (_currentBattler is IOnStartMyTurn start)
            {
                start.StartMyTurn();
            }
        }

        private void SpawnEnemy()
        {
            var enemyStorage = AssetLoader.LoadSync<EnemyStorage>("Enemy/Storage/Storage");
            Debug.Log(enemyStorage.Enemy);
            NPC enemyScriptable = enemyStorage.Enemy;
            
            _enemyBattler = Instantiate(enemyScriptable, _battleManager.EnemySpawnPoint.transform);
        }
    }
}
