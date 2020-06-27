# Kane.Extension

> 常用的 C#扩展方法和帮助类 [C# extension methods]

![dotnet-version](https://img.shields.io/badge/.net-%3E%3D4.0-blue.svg?style=flat-square&cacheSeconds=604800)
![csharp-version](https://img.shields.io/badge/C%23-8.0-gr.svg?style=flat-square&cacheSeconds=604800)
![IDE-version](https://img.shields.io/badge/IDE-vs2019-blue.svg?style=flat-square&cacheSeconds=604800)
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
var testClass = new TestClass{ Int = 123, String = "Hello World"};
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

### 2.Json 帮助类

```csharp
public class Level1
{
    public int Level1ID { get; set; }
    public string Level1Name { get; set; }
    public Level2 Level1Child { get; set; }
    public List<Level3> Level3List { get; set; }
    public Level2[] Level2Array { get; set; }
}
public class Level2
{
    public int Level2ID { get; set; }
    public string Level2Name { get; set; }
    public Level3 Level2Child { get; set; }
}
public class Level3
{
    public int Level3ID { get; set; }
    public string Level3Name { get; set; }
}

var level1 = new Level1()
{
    Level1ID = 111,
    Level1Name = "Level1",
    Level1Child = new Level2()
    {
        Level2ID = 222,
        Level2Name = "Level2",
        Level2Child = new Level3()
        {
            Level3ID = 333,
            Level3Name = "Level3"
        }
    },
    Level3List = new List<Level3>() { new Level3 { Level3ID = 3331, Level3Name = "Level31" }, new Level3 { Level3ID = 3332, Level3Name = "Level32" }, new Level3 { Level3ID = 3333, Level3Name = "Level33" } },
    Level2Array = new Level2[] { new Level2 { Level2ID = 2221, Level2Name = "Level21" }, new Level2 { Level2ID = 2222, Level2Name = "Level22" }, new Level2 { Level2ID = 2223, Level2Name = "Level23" } }
};
//level1序列化为Json字符串
var json = "{\"Level1ID\":111,\"Level1Name\":\"Level1\",\"Level1Child\":{\"Level2ID\":222,\"Level2Name\":\"Level2\",\"Level2Child\":{\"Level3ID\":333,\"Level3Name\":\"Level3\"}},\"Level3List\":[{\"Level3ID\":3331,\"Level3Name\":\"Level31\"},{\"Level3ID\":3332,\"Level3Name\":\"Level32\"},{\"Level3ID\":3333,\"Level3Name\":\"Level33\"}],\"Level2Array\":[{\"Level2ID\":2221,\"Level2Name\":\"Level21\"},{\"Level2ID\":2222,\"Level2Name\":\"Level22\"},{\"Level2ID\":2223,\"Level2Name\":\"Level23\"}]}";

var helper = new JsonHelper(json);
helper.GetValue<int>("Level1ID");//即获取level1.Level1ID的值【111】
helper.GetValue<string>("Level1Name");//即获取level1.Level1Name的值【"Level1"】
helper.GetValue<int>("Level1Child:Level2ID");//即获取level1.Level1Child.Level2ID的值【222】
helper.GetValue<int>("Level1Child:Level2Child:Level3ID");//即获取level1.Level1Child.Level2Child.Level3ID的值【333】
helper.GetValue<Level3>("Level3List:0");//即获取Level3List第一个元素，即【Level3 { Level3ID = 3331, Level3Name = "Level31" }】
helper.GetValue<Level3>("Level3List:2");//即获取Level3List第三个元素，即【Level3 { Level3ID = 3333, Level3Name = "Level33" }】
```

### 3.Xml 序列化和反序列化

```csharp
//反序列化
var testClass = xmlStream.ToObject<TestClass>();//xmlStream为Stream基类
或
var testClass = xmlTextReader.ToObject<TestClass>();//xmlTextReader为TextReader类
或
using TextReader reader = new StringReader(xmlString);//xmlString为字符串
var testClass = reader.ToObject<TestClass>();

//序列化
var testClass = new TestClass{ Int = 123, String = "Hello World"};
var xmlString = testClass.ToXml();//使用默认
//【xmlString】结果
//<?xml version="1.0" encoding="utf-8"?>
//<TestClass xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
//  <Int>123</Int>
//  <String>Hello World</String>
//  <Time>0001-01-01T00:00:00</Time>
//  <NullableInt xsi:nil="true" />
//  <NullableTime>0001-01-01T00:00:00</NullableTime>
//</TestClass>
或
var xmlString2 = testClass.ToXml(true, true);//第一个是否去掉命名空间，第二个是否去掉版本信息
//【xmlString2】结果
//<TestClass>
//  <Int>123</Int>
//  <String>Hello World</String>
//  <Time>0001-01-01T00:00:00</Time>
//  <NullableInt p2:nil="true" xmlns:p2="http://www.w3.org/2001/XMLSchema-instance" />
//  <NullableTime>0001-01-01T00:00:00</NullableTime>
//</TestClass>
或
Dictionary<string, string> namespaces = new Dictionary<string, string>
{
    { "prefix1", "namespace1" },
    { "prefix2", "namespace2" }
};
XmlWriterSettings settings = new XmlWriterSettings
{
    OmitXmlDeclaration = true,//【True】去除xml声明<?xml version="1.0" encoding="utf-8"?>
    Indent = true,//为True时，换行，缩进
    Encoding = Encoding.UTF8//默认为UTF8编码
};
var xmlString3 = testClass.ToXml(namespaces,settings);
//【xmlString3】结果
//<TestClass xmlns:prefix1="namespace1" xmlns:prefix2="namespace2">
//  <Int>123</Int>
//  <String>Hello World</String>
//  <Time>0001-01-01T00:00:00</Time>
//  <NullableInt p4:nil="true" xmlns:p4="http://www.w3.org/2001/XMLSchema-instance" />
//  <NullableTime>0001-01-01T00:00:00</NullableTime>
//</TestClass>
```