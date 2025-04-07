using Assets;
using Game.Common;

namespace Game.World
{
    public class LadyMailAccepter : MailAccepter, IOnAccept, IOnStartAccept
    {
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
            }, "Be careful goodbye.");
        }
        

        public void OnStartAccept()
        {               
            G.Get<DeliveryController>().DisableWindow();
            
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().DisableDialogue();   
                G.Get<DeliveryController>().EnableWindow();
            }, "Hello, you got there quickly, unlike others!");
        }

        private void TakeMail()
        {
            MapStorage mapStorage = AssetLoader.LoadSync<MapStorage>("Map/Storage");

            mapStorage.CurrentDepth -= 10;
        }
    }
}
