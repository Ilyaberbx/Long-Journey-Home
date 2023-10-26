using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Logic.Spawners;
using UnityEngine;
using Zenject;

namespace Logic.Triggers
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private UniqueId _idGiver;
        private bool _isSaved;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
            => _isSaved = SaveData().Contains(GetId());

        private List<string> SaveData()
            => _progressService.Progress.SaveData.SaveList;

        private void SetSaveData(string id)
            => SaveData().Add(id);

        private string GetId()
            => _idGiver.Id;

        public void Save(bool isVerified)
        {
            if (_isSaved) return;

            SetSaveData(_idGiver.Id);
            _saveLoadService.SavePlayerProgress();

            if (isVerified)
                _saveLoadService.SaveVerifiedProgress();

            _isSaved = true;
        }
    }
}