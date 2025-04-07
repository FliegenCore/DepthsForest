using Assets;
using UnityEngine;

namespace Game.World
{
    public class DeliveryController : MonoBehaviour, IService
    {
        private MailAccepter _currentMailAccepter;
        private Background _background;
        private DelivertyView _delivertyView;
        private SpawnManager _spawnManager;
        
        public bool CanDestroyed => true;
    
        public void Initialize()
        {
            _background = FindObjectOfType<Background>();
            _delivertyView = FindObjectOfType<DelivertyView>();
            _spawnManager = FindObjectOfType<SpawnManager>();
            
            _delivertyView.GiftMailButton.onClick.AddListener(GiveMail);
            
            LoadPresset();
            
            StartEvent();
        }

        public void DisableWindow()
        {
            _delivertyView.gameObject.SetActive(false);
        }

        public void EnableWindow()
        {
            _delivertyView.gameObject.SetActive(true);
        }
        
        private void StartEvent()
        {
            if (_currentMailAccepter is IOnStartAccept startAccept)
            {
                startAccept.OnStartAccept();
            }
        }

        private void GiveMail()
        {
            if (_currentMailAccepter is IOnAccept accept)
            {
                accept.OnAccept();
            }
        }
        
        private void LoadPresset()
        {
            var deliveryStorage = AssetLoader.LoadSync<MailAccepterStorage>("NPC/MailAccepter/Storage");
            var playerAsset = AssetLoader.LoadSync<Player>("Player/Player");
            
            Vector3 npcSpawnPoint = Vector3.zero;
            Vector3 playerSpawnPoint = Vector3.zero;
            
            Vector3 playerScale = Vector3.one;
            Vector3 npcScale = deliveryStorage.MailAccepter.transform.localScale;

            if (deliveryStorage.NpcIsRight)
            {
                npcSpawnPoint = _spawnManager.EnemySpawnPoint.transform.position;
                playerSpawnPoint = _spawnManager.PlayerSpawnPoint.transform.position;
            }
            else
            {
                playerScale = new Vector3(-1, 1, 1);
                npcScale = new Vector3(npcScale.x * -1, npcScale.y, npcScale.z);
                npcSpawnPoint = _spawnManager.PlayerSpawnPoint.transform.position;
                playerSpawnPoint = _spawnManager.EnemySpawnPoint.transform.position;
            }
            
            Player player = Instantiate(playerAsset, playerSpawnPoint, Quaternion.identity);
            player.DisableHealth();
            
            _currentMailAccepter = Instantiate(deliveryStorage.MailAccepter,
                npcSpawnPoint, Quaternion.identity);

            player.transform.localScale = playerScale;
            
            _currentMailAccepter.transform.localScale = npcScale;
            
            _background.SetBackground(deliveryStorage.LocationSprite);
        }
    }
}
