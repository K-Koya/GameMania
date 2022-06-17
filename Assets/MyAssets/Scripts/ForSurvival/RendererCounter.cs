using System.Collections;
using UnityEngine;

public class RendererCounter : MonoBehaviour
{
    [SerializeField] float _Delay = 1f;
    WaitForSeconds _Wait = default;

    [Header("RendererÇÃêî")]
    [SerializeField] uint _Counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        _Wait = new WaitForSeconds(_Delay);
        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        while (true)
        {
            _Counter = (uint)FindObjectsOfType<Renderer>().Length;
            yield return _Wait;
        }
    }
}
