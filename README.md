## 资产云后端SDK for .NET
![Nuget](https://img.shields.io/nuget/v/AssetCloud.AssetCloudSDK?color=green)

SDK基于 .NET Standard 2.0 开发，支持 .NET Framework 4.6.1+（推荐4.7.2+） / .NET Core 2.0+ / .NET 5。

### 安装

* dotnet CLI
```bash
dotnet add package AssetCloud.AssetCloudSDK
```

* Visual Studio 程序包管理器
```powershell
Install-Package AssetCloud.AssetCloudSDK
```

### 快速使用


```csharp
using AssetCloud.AssetCloudSDK;
// ...

var client = new AssetCloudClient(new AssetCloudConfig 
{
    AppKey = "(Your app key)",
    AppSecret = "(Your app secret)",
    BaseUrl = "http://platform.assetcloud.org.cn/dev-api",
});

var res = await client.GetAsync<object>("/asset-system/person/get/person/by/id",
    new Dictionary<string, object>
    {
        ["userId"] = "1"
    }
);
Console.WriteLine(res.ToString());
```

### 示例： 利用Nacos在 ASP.NET Core应用中使用资产云SDK

1. clone代码并切换到 `AssetCloudExample` 项目
2. 按照示例的内容搭建nacos服务，准备相应的配置文件
3. 修改Nacos地址并启动项目

    * 本地启动:  修改[appsettings.json](./AssetCloudExample/appsettings.json)中的`Nacos:ServerAddresses`节，编译并启动项目，如果是Visual Studio中打开应该会跳转到Swagger页面

    * Kubernetes:  已经准备好了示例的Docker镜像，修改[deploy.yaml](./AssetCloudExample/deploy.yaml)中的容器启动参数即可查看

        ```bash
        kubectl apply -f ./deploy.yaml
        ```