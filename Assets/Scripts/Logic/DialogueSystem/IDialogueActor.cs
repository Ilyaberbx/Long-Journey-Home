using System;

namespace Logic.DialogueSystem
{
    public interface IDialogueActor
    {
        void StartDialogue(Dialogue dialogue);
        event Action<char> OnSentenceTyping;
        event Action OnSentenceCleared;
    }
}