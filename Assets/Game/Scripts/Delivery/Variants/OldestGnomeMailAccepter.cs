using Assets;
using Game.Common;

namespace Game.World
{
    public class OldestGnomeMailAccepter : MailAccepter, IOnStartAccept, IOnAccept
    {
        private Player _player;
        
        public void OnStartAccept()
        {
            _player = FindObjectOfType<Player>();
            G.Get<DeliveryController>().DisableWindow();
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SetAnswers(() =>
                {
                    G.Get<DialogueController>().SendMessage(() =>
                    {
                        _player.EnableHealth();
                        _player.LevelUp(15, 120, 10, 2);
                        G.Get<MessageBoxController>().ShowMessage(_player.transform.position, 
                            $"<color=#{C.YELLOW}>LevelUP!</color>");
                        
                        G.Get<DialogueController>().SendMessage(() =>
                        {
                            G.Get<DialogueController>().DisableDialogue();   
                            G.Get<DeliveryController>().EnableWindow();
                            //EndDialogue();
                        }, "And since you're delivering packages, do you have any for me?");
                        
                    }, "It's very dangerous further on, take an axe.");
                }, "I'm just a delivery guy.");
                
            }, "Human?! How did you get so deep into the forest?");
        }
        
        private void EndDialogue()
        {
            G.Get<DialogueController>().DisableDialogue();
            StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
        }


        public void OnAccept()
        {
            G.Get<DeliveryController>().DisableWindow();
            
            TakeMail();
            
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<FadeController>().FadeIn(() =>
                {
                    G.Get<DialogueController>().DisableDialogue();   
                    
                    G.Get<SceneController>().ChangeScene(SceneName.MapScene);
                    G.Get<FadeController>().FadeOut();
                });
            }, "Thanks.");
        }
        
        private void TakeMail()
        {
            MapStorage mapStorage = AssetLoader.LoadSync<MapStorage>("Map/Storage");

            mapStorage.CurrentDepth -= 10;
        }
    }
}