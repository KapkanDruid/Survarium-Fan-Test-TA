using Assets.Scripts.Simples;
using System;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpMoveSpeed;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject _characterObject;

        public float MoveSpeed => _moveSpeed;
        public float RotateSpeed => _rotateSpeed;
        public float JumpTime => _jumpTime;
        public float JumpHeight => _jumpHeight;
        public float JumpMoveSpeed => _jumpMoveSpeed;

        public Transform PlayerTransform => _playerTransform;
        public GameObject CharacterObject => _characterObject;
    }
}
