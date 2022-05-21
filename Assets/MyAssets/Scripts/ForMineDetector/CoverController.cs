using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineDetector
{
    public class CoverController : MonoBehaviour
    {
        /// <summary>蓋オブジェクトのレンダーラー</summary>
        Renderer _Renderer = default;

        /// <summary>元のマテリアル</summary>
        Material _Original = default;

        [SerializeField, Tooltip("マウスカーソルを合わせたときに使うマテリアル")]
        Material _OveredMouse = default;

        [SerializeField, Tooltip("True : マウスカーソルが合っている")]
        bool _IsOveredMouse = false;

        [SerializeField, Tooltip("True : 公開済み")]
        bool _IsOpenned = false;

        [SerializeField, Tooltip("True : 旗を建てている")]
        bool _IsBuiltFlag = false;

        /// <summary>True : 公開済み</summary>
        public bool IsOpenned { get => _IsOpenned; }



        // Start is called before the first frame update
        void Start()
        {
            _Renderer = GetComponent<Renderer>();
            _Original = _Renderer.materials[0];
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsOveredMouse)
            {
                _Renderer.material = _OveredMouse;
                _IsOveredMouse = false;
            }
            else
            {
                _Renderer.material = _Original;
            }
        }

        /// <summary>カーソルを合わせている</summary>
        public void OveredMouseCursor()
        {
            _IsOveredMouse = true;
        }

        /// <summary>蓋を外す</summary>
        public void Open()
        {
            //旗が立っていれば外さない
            if (_IsBuiltFlag) return;

            _IsOpenned = true;
            gameObject.SetActive(!_IsOpenned);
        }

        /// <summary>旗を建てているときは降ろし、旗を降ろしているときは建てる</summary>
        public void SwitchFlag()
        {
            _IsBuiltFlag = !_IsBuiltFlag;
        }
    }
}
