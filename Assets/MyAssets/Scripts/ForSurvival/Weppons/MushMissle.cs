using UnityEngine;

namespace Survival
{
    /// <summary>���̂���p �L�m�R�~�T�C��</summary>
    public class MushMissle : PlayerWepponBase
    {
        [SerializeField, Tooltip("�U����")]
        WepponInfomation _Power = default;

        [SerializeField, Tooltip("���ˊԊu")]
        WepponInfomation _Interval = default;

        [SerializeField, Tooltip("��s���x")]
        WepponInfomation _Speed = default;

        [SerializeField, Tooltip("�傫��")]
        WepponInfomation _Size = default;

        [SerializeField, Tooltip("�U����������G�̃��C���[")]
        LayerMask _EnemyLayer = default;

        [SerializeField, Tooltip("�L�m�R�~�T�C���v���n�u")]
        GameObject _PrefMushMissle = null;

        /// <summary>�v���C���[�̃X�e�[�^�X���</summary>
        StatusBase _PlayerStatus = null;

        /// <summary>�L�m�R�~�T�C���I�u�W�F�N�g���Ǘ�</summary>
        ObjectPool<AttackInfoMovable> _PrefMushMisslePool = default;


        /// <summary>���ˊԊu����^�C�}�[</summary>
        float _Timer = 0f;

        // Start is called before the first frame update
        void Start()
        {
            _WepponInfomations.Add(_Power);
            _WepponInfomations.Add(_Interval);
            _WepponInfomations.Add(_Speed);
            _WepponInfomations.Add(_Size);

            _PlayerStatus = GetComponent<StatusBase>();

            _PrefMushMisslePool = new ObjectPool<AttackInfoMovable>(50);
            for (int i = 0; i < _PrefMushMisslePool.Values.Length; i++)
            {
                GameObject obj = Instantiate(_PrefMushMissle);
                obj.SetActive(false);
                _PrefMushMisslePool.Values[i] = obj.GetComponent<AttackInfoMovable>();
            }
        }

        protected override void DoAttack()
        {
            //���ˊԊu�𐧌�
            _Timer += Time.deltaTime;
            float border = 1f / _Interval.NowLevelValue;
            if (_Timer > border)
            {
                _Timer -= border;
                foreach (var val in _PrefMushMisslePool.Values)
                {
                    if (!val.gameObject.activeSelf)
                    {
                        Vector3 dir = transform.forward;
                        RaycastHit hit;
                        if (Physics.SphereCast(transform.position + Vector3.up * 11f, 10f, Vector3.down, out hit, 11f, _EnemyLayer))
                        {
                            dir =�@Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up).normalized;
                        }

                        val.gameObject.SetActive(true);
                        val.DataSet(_PlayerStatus, 0, _Power.NowLevelValue, _Speed.NowLevelValue, dir, 1, 100f);
                        val.transform.position = transform.position + Vector3.up * 0.1f;
                        val.transform.localScale = Vector3.one * _Size.NowLevelValue;
                        break;
                    }
                }
            }
        }
    }
}
