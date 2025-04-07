using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class LoseGameController : MonoBehaviour, IService
    {
        private LoseGameView _loseGameView;
        
        public bool CanDestroyed => true;
        public void Initialize()
        {
            var asset = AssetLoader.LoadSync<LoseGameView>("Common/LoseGameView");

            _loseGameView = Instantiate(asset);
            _loseGameView.RestartButton.onClick.AddListener(ResetSaves);
            _loseGameView.gameObject.SetActive(false);
        }

        public void EndGame()
        {
            _loseGameView.gameObject.SetActive(true);
        }
#if UNITY_EDITOR
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ResetSaves();
            }
        }
#endif

        private void ResetSaves()
        {
            G.Get<DialogueController>().DisableDialogue();
            var playerSo = AssetLoader.LoadSync<PlayerSO>("Player/BattlePlayer");
            var mapStorage = AssetLoader.LoadSync<MapStorage>("Map/Storage");
            playerSo.ResetAllData();
            mapStorage.ResetAllData();

            G.UnregisterAllDestroyed();
            
            G.Get<SceneController>().ChangeScene(SceneName.MapScene);
        }
    }
}
