### Configuration Options

There is only one configuration option. The "FakeHttps.ForceHttps" setting will
force the module to mark all incoming connections as secure.

```xml
<appSettings>
  <add key="FakeHttps.ForceHttps" value="true" />
</appSettings>
```