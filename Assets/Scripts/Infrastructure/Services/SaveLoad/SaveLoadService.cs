﻿using ProjectSolitude.Data;
using ProjectSolitude.Extensions;
using ProjectSolitude.Infrastructure.PersistentProgress;
using ProjectSolitude.Interfaces;
using UnityEngine;

namespace ProjectSolitude.Infrastructure.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private const string ProgressKey = "Progress";

        public SaveLoadService(IGameFactory gameFactory,IPersistentProgressService persistentProgressService)
        {
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressWriter progressWriter in _gameFactory.ProgressWriter)
                progressWriter.UpdateProgress(_persistentProgressService.PlayerProgress);
            
            PlayerPrefs.SetString(ProgressKey,_persistentProgressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress()
            => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        
    }
}