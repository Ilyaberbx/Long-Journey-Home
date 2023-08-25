using System;

namespace Infrastructure.Services.Dialogue
{
    public interface IDialogueService
    {
        event Action<char> OnSentenceTyping;
        event Action OnSentenceCleared;
        void StartDialogue(Logic.DialogueSystem.Dialogue dialogue);
    }
}