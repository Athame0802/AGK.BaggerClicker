using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIndexable
{
    // IIndexable을 상속하는 모든 클래스는 아래 인덱서를 포함시켜야 함
    object this[string index] { get; }
}
