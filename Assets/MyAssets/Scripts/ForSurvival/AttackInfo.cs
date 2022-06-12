using UnityEngine;

namespace Survival
{
    /// <summary>�U�����</summary>
    public class AttackInfo : MonoBehaviour
    {
        /// <summary>�U���ҏ��</summary>
        StatusBase _Status = default;

        [SerializeField, Tooltip("�_���[�W���q�ɐG��Ă���ԗ^���鑱����_���[�W")]
        float _PowerOnStay = 0.05f;

        [SerializeField, Tooltip("�_���[�W���q�ɐG�ꂽ����̂ݗ^����_���[�W")]
        float _PowerOnEnter = 0f;

        /// <summary>�_���[�W���q�ɐG��Ă���ԗ^���鑱����_���[�W</summary>
        public float PowerOnStay { get => _PowerOnStay; }
        /// <summary>�_���[�W���q�ɐG�ꂽ����̂ݗ^����_���[�W</summary>
        public float PowerOnEnter { get => _PowerOnEnter; }
        /// <summary>�U���ҏ��</summary>
        public StatusBase Status { get => _Status; }


        public void DataSet(StatusBase status, float powerOnStay, float powerOnEnter)
        {
            _Status = status;
            _PowerOnStay = powerOnStay;
            _PowerOnEnter = powerOnEnter;
        }
    }
}
