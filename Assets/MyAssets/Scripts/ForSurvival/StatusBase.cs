using UnityEngine;

namespace Survival
{
    abstract public class StatusBase : MonoBehaviour
    {
        [SerializeField, Tooltip("�̗�")]
        protected float _Life = 100f;

        /// <summary>�̗�</summary>
        public float Life { get => _Life; }
    }
}
