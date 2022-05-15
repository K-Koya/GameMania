using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] int _Count = 5;
    [SerializeField] int _Selected = 0;

    Image[] _Cells = default;

    private void Start()
    {
        _Cells = new Image[_Count];

        for (var i = 0; i < _Count; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            _Cells[i] = obj.AddComponent<Image>();
            if (i == _Selected) { _Cells[i].color = Color.red; }
            else { _Cells[i].color = Color.white; }
        }
    }

    private void Update()
    {
        if (_Count < 1) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            _Selected = --_Selected < 0 ? _Count - 1 : _Selected;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            _Selected = ++_Selected < _Count ? _Selected : 0;
        }
        if (Input.GetKeyDown(KeyCode.Space)) //spaceキーを押した
        {
            RemoveCell();
        }

        DrawCell();
    }

    void RemoveCell()
    {
        Destroy(_Cells[_Selected]);

        for (var i = _Selected; i < _Count - 1; i++)
            _Cells[i] = _Cells[i + 1];

        _Cells[--_Count] = null;

        if (_Selected >= _Count) _Selected = _Count - 1;
    }

    void DrawCell()
    {
        for (var i = 0; i < _Count; i++)
        {
            if (i == _Selected) { _Cells[i].color = Color.red; }
            else { _Cells[i].color = Color.white; }
        }
    }
}
