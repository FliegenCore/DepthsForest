using Assets;
using Game.Common;

namespace Game.World
{
    public class LastMailAccepter : MailAccepter, IOnAccept, IOnStartAccept
    {
        public void OnAccept()
        {
           
        }
        

        public void OnStartAccept()
        {               
            G.Get<DeliveryController>().DisableWindow();
            
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SendMessage(() =>
                {
                    G.Get<FadeController>().FadeIn(() =>
                    {
                        G.Get<DialogueController>().DisableDialogue();
                        G.Get<SceneController>().ChangeScene(SceneName.EndScene);
                        G.Get<FadeController>().FadeOut();
                    });
                    //end game
                }, "Don't be afraid, it won't hurt, the forest won't let go!");
            }, "You are the first one who got to me!");
        }
    }
}