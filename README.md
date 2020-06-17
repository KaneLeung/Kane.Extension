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
    public int ID { get; set; }
    public string Name { get; set; }
}
var testClass = new TestClass{ ID = 123, Name="Hello World"};
string jsonString = testClass.ToJson();//"{\"ID\":123,\"Name\":\"Hello World\"}"
TestClass entity = jsonString.ToObject<TestClass>();
```

#### 使用【Newtonsoft.Json】,适合 Net4.0 以及 NetCore2.1 以上

```csharp
using Kane.Extension.JsonNet;
public class TestClass
{
    public int ID { get; set; }
    public string Name { get; set; }
}
var testClass = new TestClass{ ID = 123, Name="Hello World"};
string jsonString = testClass.ToJson();//"{\"ID\":123,\"Name\":\"Hello World\"}"
TestClass entity = jsonString.ToObject<TestClass>();
```
