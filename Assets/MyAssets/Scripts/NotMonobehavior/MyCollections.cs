using System;

namespace MyCollections
{
    namespace Generic
    {
        /// <summary>���O�̓��I�z��</summary>
        /// <typeparam name="T">���l�߂���^</typeparam>
        public class List<T>
        {
            /// <summary>��</summary>
            class Container
            {
                /// <summary>����</summary>
                public T _Value;

                public Container _Back;

                public Container _Front;
            }
        }
    }
}

