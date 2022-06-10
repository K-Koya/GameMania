using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Homework
{
    public class Sample : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] int _Row = 5;
        [SerializeField] int _Column = 5;

        [SerializeField] int _SelectedC = 0;
        [SerializeField] int _SelectedR = 0;

        Image[,] _Cells = default;

        [Header("true : ÉNÉäÉAÇµÇΩ")]
        [SerializeField] bool _IsCleared = false;
        [Header("ä|Ç©Ç¡ÇΩéËêî")]
        [SerializeField] int _NumberOfEffort = 0;
        [Header("ä|Ç©Ç¡ÇΩéûä‘")]
        [SerializeField] float _Timer = 0f;

        private void Start()
        {
            var layout = GetComponent<GridLayoutGroup>();
            if (layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount) layout.constraintCount = _Column;
            else if(layout.constraint == GridLayoutGroup.Constraint.FixedRowCount) layout.constraintCount = _Row;

            _Cells = new Image[_Row, _Column];

            for (var r = 0; r < _Row; r++)
            {
                for (var c = 0; c < _Column; c++)
                {
                    var obj = new GameObject($"Cell({r}, {c})");
                    obj.transform.parent = transform;

                    _Cells[r, c] = obj.AddComponent<Image>();
                    _Cells[r, c].color = Color.white;
                }
            }

            int numberOfBlackCell = Random.Range(1, (_Row * _Column) / 2);
            for(int i = 0; i < numberOfBlackCell; i++)
            {
                bool find = false;
                int r = 0;
                int c = 0;
                while (!find)
                {
                    r = Random.Range(0, _Row);
                    c = Random.Range(0, _Column);
                    find = _Cells[r, c].color == Color.white;
                }
                _Cells[r, c].color = Color.black;
            }
        }

        private void Update()
        {
            if (!_IsCleared) _Timer += Time.deltaTime;
        }

        void DrawCell()
        {
            for (var r = 0; r < _Row; r++)
            {
                for (var c = 0; c < _Column; c++)
                {
                    if (!_Cells[r, c]) continue;

                    if (r == _SelectedR && c == _SelectedC) { _Cells[r, c].color = Color.red; }
                    else { _Cells[r, c].color = Color.white; }
                }
            }
        }

        int Repeat(int input, int max)
        {
            if (input < 0) return max + input;
            return input < max ? input : input - max;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var cell = eventData.pointerCurrentRaycast.gameObject;
            if (!cell) return;

            var image = cell.GetComponent<Image>();
            for (var r = 0; r < _Row; r++)
            {
                for (var c = 0; c < _Column; c++)
                {
                    if (_Cells[r, c] == image)
                    {
                        _Cells[r, c].color = _Cells[r, c].color == Color.white ? Color.black : Color.white;
                        if (r - 1 > -1) _Cells[r - 1, c].color = _Cells[r - 1, c].color == Color.white ? Color.black : Color.white;
                        if (r + 1 < _Row) _Cells[r + 1, c].color = _Cells[r + 1, c].color == Color.white ? Color.black : Color.white;
                        if (c - 1 > -1) _Cells[r, c - 1].color = _Cells[r, c - 1].color == Color.white ? Color.black : Color.white;
                        if (c + 1 < _Column) _Cells[r, c + 1].color = _Cells[r, c + 1].color == Color.white ? Color.black : Color.white;
                        goto LoopBreak;
                    }
                }
            }
        LoopBreak:;

            if (_IsCleared)
            {
                _NumberOfEffort = 0;
                _Timer = 0f;
            }

            _IsCleared = true;
            foreach (var c in _Cells)
            {
                if(c.color == Color.white)
                {
                    _IsCleared = false;
                    break;
                }
            }

            _NumberOfEffort++;
        }
    }
}
