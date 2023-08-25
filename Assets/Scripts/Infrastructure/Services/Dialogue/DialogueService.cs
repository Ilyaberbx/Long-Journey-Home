using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Infrastructure.Services.Dialogue
{
    public class DialogueService : IDialogueService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public DialogueService(ICoroutineRunner coroutineRunner) 
            => _coroutineRunner = coroutineRunner;
        
        public event Action<char> OnSentenceTyping;
        public event Action OnSentenceCleared;

        private readonly float _typingOneWordDuration = 0.1f;
        private readonly float _nextSentenceInterval = 4;
        
        private readonly Queue<string> _sentences = new Queue<string>();
        private Coroutine _typingCoroutine;

        public void StartDialogue(Logic.DialogueSystem.Dialogue dialogue)
        {
            ClearPreviousSentence();
            EnqueueSentence(dialogue);
            DisplayNextSentence();
        }

        private void ClearPreviousSentence()
        {
            OnSentenceCleared?.Invoke();

            if (_typingCoroutine != null)
            {
                _coroutineRunner.StopCoroutine(_typingCoroutine);
                _typingCoroutine = null;
            }
            _sentences.Clear();
        }

        private void DisplayNextSentence()
        {
            OnSentenceCleared?.Invoke();
            
            if (IsQueueEnd())
                return;

            string sentenceToDisplay = _sentences.Dequeue();
            _coroutineRunner.StopAllCoroutines();
            _typingCoroutine = _coroutineRunner.StartCoroutine(TypeSentence(sentenceToDisplay));
        }
        

        private IEnumerator TypeSentence(string sentence)
        {
            foreach (var letter in sentence.ToCharArray())
            {
                OnSentenceTyping?.Invoke(letter);
                yield return new WaitForSeconds(_typingOneWordDuration);
            }

            yield return new WaitForSeconds(_nextSentenceInterval);
            DisplayNextSentence();
        }

        private bool IsQueueEnd()
            => _sentences.Count == 0;

        private void EnqueueSentence(Logic.DialogueSystem.Dialogue dialogue)
        {
            foreach (string sentence in dialogue.Sentences)
                _sentences.Enqueue(sentence);
        }
    }
}