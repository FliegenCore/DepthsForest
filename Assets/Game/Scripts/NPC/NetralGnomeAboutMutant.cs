using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class NetralGnomeAboutMutant : NPC, IOnBattleStart
    {
        public void BattleStart()
        {
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SetAnswers(() =>
                    {
                        G.Get<DialogueController>().SendMessage(() =>
                        {
                            G.Get<DialogueController>().SendMessage(() =>
                            {
                                G.Get<DialogueController>().DisableDialogue();
                                StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
                            }, "His legs are very long, attack them");
                        }, "Oh, I see you're not a monster.");
                    
                    }, "Gnome, give me some advice.", 
                    
                    "I'm just a postman.", () =>
                    {
                        G.Get<DialogueController>().SendMessage(() =>
                        {
                            G.Get<DialogueController>().SendMessage(() =>
                            {
                                G.Get<DialogueController>().DisableDialogue();
                                StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
                            }, "His legs are very long, attack them");
                        }, "They say there is a mutant living in these forests!");
                    });
                
            }, "What are you doing here?!");
        }
    }
}