using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    /// <summary>�}�C���X�C�[�p�[�I�ȓz�̃Q�[���Ղ𐧌䂷��R���|�[�l���g</summary>
    public class CellMap : MonoBehaviour
    {
        [SerializeField, Tooltip("�\������Q�[���Ղ̃}�X�̃v���n�u")]
        GameObject _CellPrefab = default;

        [SerializeField, Tooltip("�Q�[���Ղ�1�ӂ̃}�X�̐��̍ő�l")]
        int _LengthLimit = 10000;
        
        [SerializeField, Tooltip("�Q�[���Ղ̉��c��(Length Limit �𒴂���ꍇ�� Length Limit �Ɋۂ߂���)")]
        Vector2Int _MapSize = new Vector2Int(10, 10);

        [SerializeField, Tooltip("�}�X�̊Ԋu")]
        float _Spacing = 1f;

        /// <summary>�Q�[����</summary>
        CellController[,] _Map = null;


        // Start is called before the first frame update
        void Start()
        {
            Generate();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>�Q�[���Ղ���ׂ�</summary>
        public void Generate()
        {
            int sizeX = _MapSize.x > _LengthLimit ? _LengthLimit : _MapSize.x;
            int sizeY = _MapSize.y > _LengthLimit ? _LengthLimit : _MapSize.y;

            _Map = new CellController[sizeX, sizeY];

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    GameObject cell = Instantiate(_CellPrefab);
                    _Map[x,y] = cell.GetComponent<CellController>();
                    cell.transform.parent = transform;
                    cell.transform.position = new Vector3(x * _Spacing, 0f, -y * _Spacing);
                }
            }
        }

        /// <summary>�Q�[���Ղ̃}�X�̒��g�����</summary>
        void GameStart()
        {

        }
    }
}
