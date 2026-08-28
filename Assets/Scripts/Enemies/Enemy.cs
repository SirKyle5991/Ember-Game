using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {

        [FormerlySerializedAs("_myHealth")] public Health Health;
        public abstract bool ShouldRespawn();

        protected virtual void Awake()
        {
            Health = GetComponent<Health>();
        }

        private void Start()
        {
            GameManager.Instance.RegisterEnemy(this);
        }
    }
}