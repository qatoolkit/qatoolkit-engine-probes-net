# QAToolKit Engine Probes library
[![Build .NET Library](https://github.com/qatoolkit/qatoolkit-engine-probes-net/workflows/.NET%20Core/badge.svg?branch=main)](https://github.com/qatoolkit/qatoolkit-engine-probes-net/actions)
[![CodeQL](https://github.com/qatoolkit/qatoolkit-engine-probes-net/workflows/CodeQL%20Analyze/badge.svg)](https://github.com/qatoolkit/qatoolkit-engine-probes-net/security/code-scanning)
[![Sonarcloud Quality gate](https://github.com/qatoolkit/qatoolkit-engine-probes-net/workflows/Sonarqube%20Analyze/badge.svg)](https://sonarcloud.io/dashboard?id=qatoolkit_qatoolkit-engine-probes-net)
[![NuGet package](https://img.shields.io/nuget/v/QAToolKit.Engine.Probes?label=QAToolKit.Engine.Probes)](https://www.nuget.org/packages/QAToolKit.Engine.Probes/)

## Description
`QAToolKit.Engine.Probes` is a .NET Standard 2.1 library that contains network probes to test servers/services with protocols like HTTP, TCP, ICMP (Ping).

When you have a complex software system and would like to check its liveness or read some statuses, you can use the QAToolKit Probes library.

You can quickly write tests or protocols to send `PING`, send `TCP messages`, or hit an `HTTP endpoint`.

Then get the results and process them further depending on your needs.

Supported .NET frameworks and standards: `netstandard2.0`, `netstandard2.1`, `netcoreapp3.1`, `net5.0`

### 1. Ping Probes

A Ping Probe is an ICMP echo request that checks if a particular host is "alive" or that it can return the echo to us.

From Wikipedia: _Ping operates by sending Internet Control Message Protocol (ICMP) echo request packets to the target host and waiting for an ICMP echo reply._  

You can use it to check if a host is up or down, but not much more. It can be used as _a first in line_ test since it's the fastest.

To create a Ping probe, you can easily do it like this:

```csharp
PingProbe pinger = new PingProbe("google.com");
PingResult result = await pinger.Execute();

logger.LogInformation($"Success: {result.Success}");
logger.LogInformation($"ReplyAddress: {result.ReplyAddress}");
logger.LogInformation($"Time: {result.RoundTripTime}");
logger.LogInformation($"TTL: {result.Ttl}");
logger.LogInformation($"BufferLength: {result.BufferLength}");
```

`PingProbe` generates a `PingResult` which contains a lot of useful information.

### 2. TCP Probes

A TCP Probe can do much more than a Ping Probe. If you have an IoT or any other service listening for TCP connections on a specific port, this is a probe for you.
You can easily send an array of messages or commands to a TCP address and get the responses.

To create a TCP Probe, you can easily do it like this:

```csharp
TcpProbe tcpProbe = new TcpProbe("tcpbin.com", 4242, new[] { "HELO\n" });
TcpResult result = await tcpProbe.Execute();

logger.LogInformation($"ResponseData: {result.ResponseData}");
```

We connect to the `tcpbin.com:4242` and send a message HELO + newline character - `"Helo\n"`.
Since this is an ECHO service, we will get back what we sent - `"Helo\n"`. You can read the reply message from the `TcpResult.ResponseData` property.

### 3. HTTP Probes

An HTTP Probe creates a simple HTTP request to your specified URL address.
Only two parameters are necessary to make an HTTP Probe; Url and HttpMethod.

It would be best to use HTTP probes for simple HTTP requests; that's why you can not specify and authenticate or JSON payloads.
Here are some examples:
- test if a website index page returns OK - 200
- test a health check API endpoint returns OK - 200
- test that an Azure storage file is not visible and returns Forbidden - 403

If you want to do comprehensive HTTP service tests, please use [QAToolKit HttpTester Library](https://github.com/qatoolkit/qatoolkit-engine-httptester-net).

To create a HTTP Probe, you can easily do it like this:

```csharp
HttpProbe httpProbe = new HttpProbe(
        new Uri("https://swagger-demo.qatoolkit.io/swagger/index.html"),
        HttpMethod.Get);
HttpResult result = await httpProbe.Execute();

logger.LogInformation($"StatusCode: {result.StatusCode}");
logger.LogInformation($"ResponseBody: {result.ResponseBody}");
```

In the example above, we connect to the https://swagger-demo.qatoolkit.io/swagger/index.html by making a `GET` request. We wait for the response and can extract the `StatusCode` and `ResponseBody` from it.

### 4. Implement your custom probes

If you want to follow the QAToolKit convention, you can create your probe by implementing `IProbe<T>` interface.

Below we create a `MyProbe` that implements `IProbe<string>` with single method Execute().

```
public class MyProbe : IProbe<string>
{
    public Task<string> Execute()
    {
        ....
    }
}
```

## To-do

- **This library is an early alpha version**
- Support for more probes (UDP,...).

## License

MIT License

Copyright (c) 2020 Miha Jakovac

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.