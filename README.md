# Kogane Tablet Checker

デバイスの種類がタブレットかどうかを判別するクラス

## 使用例

```csharp
using Kogane;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        Debug.Log( TabletChecker.IsTabletCurrent );
    }
}
```