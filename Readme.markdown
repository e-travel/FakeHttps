FakeHttps
=========

What is This?
-------------

This is an HttpModule for ASP.NET applications that fakes the IsSecureConnection in the Request object.

It understands the  X-Forwarded-Proto header sent by an SSL Offloader/Proxy and sets the IsSecureConnection according to it.

It only works for apps running in *Integrated* mode.

Installation
---------------------

In your web.config, find the *system.webServer* section and add the module. Example:

```xml
<system.webServer>
  <modules runAllManagedModulesForAllRequests="true">
   
	<add name="FakeHttpModule" type="ETravel.Web.FakeHttps.HttpModule, ETravel.Web.FakeHttps"/>

    </modules>
</system.webServer>
```


Configuration Options
---------------------

There is only one configuration option. The "FakeHttps.ForceHttps" setting will
force the module to mark all incoming connections as secure.

```xml
<appSettings>
  <add key="FakeHttps.ForceHttps" value="true" />
</appSettings>
```