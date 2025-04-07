using Game.Common;

namespace Game.World
{
    public class OldestGnome : NPC, IOnBattleStart
    {
        private Player _player;

        public void BattleStart()
        {
            _player = FindObjectOfType<Player>();

            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SetAnswers(() =>
                {
                    G.Get<DialogueController>().SendMessage(() =>
                    {
                        G.Get<DialogueController>().SendMessage(() =>
                        {
                            _player.EnableHealth();
                            _player.LevelUp(10, 100, 10, 2);
                            G.Get<MessageBoxController>().ShowMessage(_player.transform.position, 
                                $"<color=#{C.YELLOW}>LevelUP!</color>");
                            G.Get<DialogueController>().SendMessage(() =>
                            {
                                EndDialogue();
                            }, "Good luck, human!");
                            
                        }, "Take this knife.");
                    }, "But remember the forest is very dangerous.");
                }, "Your brother said you will help me.");
                
            }, "Who are you?");
        }
        
        private void EndDialogue()
        {
            G.Get<DialogueController>().DisableDialogue();
            StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
        }
    }
}
