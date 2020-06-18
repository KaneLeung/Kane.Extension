# Kane.Extension

> 常用的 C#扩展方法和帮助类 [C# extension methods]

![dotnet-version](https://img.shields.io/badge/.net-%3E%3D4.0-blue.svg?style=flat-square&cacheSeconds=604800)
[![nuget-version](https://img.shields.io/nuget/v/Kane.Extension.svg?style=flat-square&cacheSeconds=604800)](https://www.nuget.org/packages/Kane.Extension)
[![Nuget](https://img.shields.io/nuget/dt/Kane.Extension?style=flat-square&cacheSeconds=604800)](https://www.nuget.org/packages/Kane.Extension)
![stars](https://img.shields.io/github/stars/KaneLeung/Kane.Extension?style=flat-square&cacheSeconds=604800)
![last commit](https://img.shields.io/github/last-commit/KaneLeung/Kane.Extension?style=flat-square&cacheSeconds=604800)
[![Release](https://img.shields.io/github/release/KaneLeung/Kane.Extension.svg?style=flat-square&cacheSeconds=604800)](https://github.com/KaneLeung/Kane.Extension/releases/latest)
[![Issues](https://img.shields.io/github/issues/KaneLeung/Kane.Extension.svg?style=flat-square)](https://github.com/KaneLeung/Kane.Extension/issues)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/KaneLeung/Kane.Extension/blob/master/LICENSE)

## 如何使用

> 添加引用或在 Nuget 直接搜索【Kane.Extension】;

`Install-Package Kane.Extension`

## 功能示例代码

### 1.Json 序列化和反序列化

#### 使用【System.Text.Json】,适合 NetCore3.1 以上

```csharp
using Kane.Extension.Json;
public class TestClass
{
    public int Int { get; set; }
    public string String { get; set; }
    public DateTime Time { get; set; }
    public int? NullableInt { get; set; }
    public DateTime NullableTime { get; set; }
    public int[] IntArray { get; set; }
    public List<string> StringList { get; set; }
}
var testClass = new TestClass{ ID = 123, Name="Hello World"};
string customOption = testClass.ToJson();//使用预设的【JsonSerializerOptions】
//【customOption】{"Int":123,"String":"Hello World","Time":"2020-06-18 00:00:00","NullableTime":"0001-01-01 00:00:00"}
string ingoreOption = testClass.ToJson(true);//忽略预设选项
//【ingoreOption】{"Int":123,"String":"Hello World","Time":"2020-06-18T00:00:00","NullableInt":null,"NullableTime":"0001-01-01T00:00:00","IntArray":null,"StringList":null}
TestClass entity = jsonString.ToObject<TestClass>();//使用预设反序列化
//也可以自定义全局【JsonSerializerOptions】配置选项
Kane.Extension.Json.Json.GlobalOption = new System.Text.Json.JsonSerializerOptions();
```

#### 使用【Newtonsoft.Json】,适合 Net4.0 以及 NetCore2.1 以上

```csharp
using Kane.Extension.JsonNet;
string customOption = testClass.ToJson();//使用预设的【JsonSerializerSettings】
//【customOption】{"Int":123,"String":"Hello World","Time":"2020-06-18 00:00:00","NullableTime":"0001-01-01 00:00:00"}
string ingoreOption = testClass.ToJson(true);//忽略预设选项
//【ingoreOption】{"Int":123,"String":"Hello World","Time":"2020-06-18T00:00:00","NullableInt":null,"NullableTime":"0001-01-01T00:00:00","IntArray":null,"StringList":null}
TestClass entity = jsonString.ToObject<TestClass>();//使用预设反序列化
//也可以自定义全局【JsonSerializerSettings】配置选项
Kane.Extension.JsonNet.JsonNet.GlobalSetting = new Newtonsoft.Json.JsonSerializerSettings();
```
