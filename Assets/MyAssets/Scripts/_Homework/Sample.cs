using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] int _Row = 5;
    [SerializeField] int _Column = 5;

    [SerializeField] int _SelectedC = 0;
    [SerializeField] int _SelectedR = 0;

    Image[,] _Cells = default;

    private void Start()
    {
        _Cells = new Image[_Row, _Column];

        //for (var i = 0; i < _Count; i++)
        //{
        //    var obj = new GameObject($"Cell{i}");
        //    obj.transform.parent = transform;

        //    _Cells[i] = obj.AddComponent<Image>();
        //    if (i == _Selected) { _Cells[i].color = Color.red; }
        //    else { _Cells[i].color = Color.white; }
        //}

        for (var r = 0; r < 5; r++)
        {
            for (var c = 0; c < 5; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                _Cells[r,c] = obj.AddComponent<Image>();
                if (r == _SelectedR && c == _SelectedC) { _Cells[r, c].color = Color.red; }
                else { _Cells[r, c].color = Color.white; }
            }
        }
    }

    private void Update()
    {
        int vertical = (Input.GetKeyDown(KeyCode.UpArrow) ? -1 : 0) + (Input.GetKeyDown(KeyCode.DownArrow) ? 1 : 0);
        int horizontal = (Input.GetKeyDown(KeyCode.LeftArrow) ? -1 : 0) + (Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0);

        if(vertical != 0)
        {
            do
            {
                _SelectedR = Repeat(_SelectedR + vertical, _Row);
            } while (!_Cells[_SelectedR, _SelectedC]);

            DrawCell();
        }
        if(horizontal != 0)
        {
            do
            {
                _SelectedC = Repeat(_SelectedC + horizontal, _Column);
            } while (!_Cells[_SelectedR, _SelectedC]);

            DrawCell();
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) //spaceÉLÅ[ÇâüÇµÇΩ
        {
            Destroy(_Cells[_SelectedR, _SelectedC]);
        }
    }

    void DrawCell()
    {
        for (var r = 0; r < _Row; r++)
        {
            for(var c = 0; c < _Column; c++)
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
}
