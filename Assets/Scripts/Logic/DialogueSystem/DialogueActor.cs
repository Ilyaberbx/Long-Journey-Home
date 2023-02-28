using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.DialogueSystem
{
    public class DialogueActor : MonoBehaviour, IDialogueActor
    {
        public event Action<char> OnSentenceTyping;
        public event Action OnSentenceCleared;
        
        [SerializeField] private float _typingOneWordDuration;
        [SerializeField] private float _nextSentenceInterval;
        
        private Queue<string> _sentences = new Queue<string>();
        private Coroutine _typingCoroutine;

        public void StartDialogue(Dialogue dialogue)
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
                StopCoroutine(_typingCoroutine);
                _typingCoroutine = null;
            }
            _sentences.Clear();
        }

        private void DisplayNextSentence()
        {
            OnSentenceCleared?.Invoke();
            
            if (IsQueueEnd())
            {
                Debug.Log("End");
                return;
            }

            string sentenceToDisplay = _sentences.Dequeue();
            StopAllCoroutines();
            _typingCoroutine = StartCoroutine(TypeSentence(sentenceToDisplay));
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

        private void EnqueueSentence(Dialogue dialogue)
        {
            foreach (var sentence in dialogue.Sentences)
                _sentences.Enqueue(sentence);
        }
    }
}