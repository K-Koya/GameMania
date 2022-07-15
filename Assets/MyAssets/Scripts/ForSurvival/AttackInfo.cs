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

        [SerializeField, Tooltip("���ʉ���炷�ꍇ�A���̎�ނ��`")]
        SEManager.Kind _SoundKind = SEManager.Kind.None;

        /// <summary>�_���[�W���q�ɐG��Ă���ԗ^���鑱����_���[�W</summary>
        public float PowerOnStay { get => _PowerOnStay; }
        /// <summary>�_���[�W���q�ɐG�ꂽ����̂ݗ^����_���[�W</summary>
        public float PowerOnEnter { get => _PowerOnEnter; }
        /// <summary>�U���ҏ��</summary>
        public StatusBase Status { get => _Status; }


        /// <summary>����Ƃ��ďo�͂���I�u�W�F�N�g</summary>
        /// <param name="status"></param>
        /// <param name="powerOnStay"></param>
        /// <param name="powerOnEnter"></param>
        public void DataSet(StatusBase status, float powerOnStay, float powerOnEnter)
        {
            _Status = status;
            _PowerOnStay = powerOnStay;
            _PowerOnEnter = powerOnEnter;
        }

        /// <summary>��ɃA�j���[�V�����C�x���g�Ƃ��ČĂяo�����߂̌��ʉ��������\�b�h</summary>
        public void EmitSound()
        {
            if(_SoundKind != SEManager.Kind.None) SEManager.Emit((int)_SoundKind, transform);
        }
    }
}
