using Assets.Scripts.Architecture.CustomEventBus;
using Assets.Scripts.Architecture.CustomEventBus.Signals.InitializeSignals;
using Assets.Scripts.Architecture.Input;
using Assets.Scripts.Content;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using IDisposable = Assets.Scripts.Architecture.CustomEventBus.IDisposable;

namespace Assets.Scripts.Architecture.Bootstrap
{
    public class LevelBootstrap : MonoBehaviour
    {
        [Inject] private InputEventHandler _inputEventHandler;
        [Inject] private PlayerController.Factory _playerFactory;
        private PlayerController _playerController;
        [Inject] private EventBus _eventBus;

        private List<object> _dependencies = new();

        private void Awake()
        {
            InitializeDependencies();
            AddDependencies();
        }

        private void InitializeDependencies()
        {
            _inputEventHandler.Initialize();

            _playerController = _playerFactory.Create();
        }

        private void AddDependencies()
        {
            _dependencies.Add(_inputEventHandler);
            _dependencies.Add(_playerController);
        }

        private void OnDestroy()
        {
            foreach (var dependency in _dependencies)
            {
                if (dependency is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
