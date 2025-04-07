using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Common
{
    public enum SceneName
    {
        MapScene,
        BattleScene,
        DeliveryScene,
        EndScene,
    }
    
    public class SceneController : MonoBehaviour, IService
    {
        public bool CanDestroyed => false;

        private bool _initialized;
        
        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            
            DontDestroyOnLoad(gameObject);
        }

        public void ChangeScene(SceneName sceneName)
        {
            StartCoroutine(LoadSceneAfterCleanup(sceneName));
        }

        private IEnumerator LoadSceneAfterCleanup(SceneName sceneName)
        {
            G.UnregisterAllDestroyed();

            yield return new WaitForEndOfFrame();

            string sn = sceneName.ToString();
            SceneManager.LoadScene(sn);
        }
    }
}
