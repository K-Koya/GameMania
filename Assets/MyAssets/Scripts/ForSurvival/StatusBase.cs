using UnityEngine;

namespace Survival
{
    abstract public class StatusBase : MonoBehaviour
    {
        [SerializeField, Tooltip("‘Ì—Í")]
        protected float _Life = 100f;

        /// <summary>‘Ì—Í</summary>
        public float Life { get => _Life; }
    }
}
