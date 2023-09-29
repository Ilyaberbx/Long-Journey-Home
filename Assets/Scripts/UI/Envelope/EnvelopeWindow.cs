using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Envelope
{
    public class EnvelopeWindow : WindowBase
    {
        [SerializeField] private Button _nextPageButton;
        [SerializeField] private Button _previousPageButton;
        [SerializeField] private TextMeshProUGUI _envelopeText;
        [SerializeField] private Button _closeButton;
        private int _currentPage;
        private int _totalPages;
        
        protected override void SubscribeUpdates()
        {
            _closeButton.onClick.AddListener(Close);
            _nextPageButton.onClick.AddListener(NextPage);
            _previousPageButton.onClick.AddListener(PreviousPage);
        }
        private void DisableButtons()
        {
            ToggleNextPageButton(false);
            TogglePreviousPageButton(false);
        }

        private bool IsOnePagedEnvelope() 
            => _totalPages <= 0;

        private void PreviousPage()
        {
            _currentPage--;
            _envelopeText.pageToDisplay--;

            if (_currentPage <= 0) 
                TogglePreviousPageButton(false);
            
            ToggleNextPageButton(false);
        }
        private void NextPage()
        {
            _currentPage++;
            _envelopeText.pageToDisplay++;

            if (_currentPage >= _totalPages) 
                ToggleNextPageButton(false);
            
            TogglePreviousPageButton(false);
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            _closeButton.onClick.RemoveListener(Close);
        }

        private void TogglePreviousPageButton(bool value) 
            => _previousPageButton.gameObject.SetActive(value);

        private void ToggleNextPageButton(bool value) 
            => _nextPageButton.gameObject.SetActive(value);

        public void UpdateContent(string text)
        {
            _envelopeText.text = text;
            
            _totalPages = _envelopeText.textInfo.pageInfo.Length;

            if (IsOnePagedEnvelope()) 
                DisableButtons();
        }
    }
}