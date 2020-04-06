# LegacyEmailService

A simple service to send emails. This is a wrapper for the deprecated/obsolete System.Net.Mail.SmptClient. This package is meant to help support legacy systems/environments. For new development please use MailKit or other third party packages.

## Dependancies

Microsoft.Extensions.Options

## Installing

Install from Nuget
```
Install-Package Ng.LegacyEmailService
```

## Usage

Console application

```csharp
using Ng.Services;
...
static async Task Main(string[] args)
{
    var settings = new EmailSettings
    {
        Host = "smtp.mymailserver.net",
    };
    var emailService = new LegacyEmailService(settings);

    var from = new MailAddress("me@mydomain.net");
    var to = new string[] { "someone@mydomain.net" };
    var subject = "My subject";
    var body = "My body";

    await emailService.SendEmail(from, to, subject, body);
}
```

ASP.NET Core

Register service with dependency injection in Startup.cs
```csharp
using Ng.Services;
...
public void ConfigureServices(IServiceCollection services)
{
    services.AddLegacyEmailService(options => {
        options.Host = "smtp.mymailserver.net";
    });

    or
    
    services.AddScoped<ILegacyEmailService, LegacyEmailService>();
    services.Configure<EmailSettings>(options => {
        options.Host = "smtp.mymailserver.net";
    });
}
```

Inject ILegacyEmailService into a Controller or wherever you like
```csharp
using Ng.Services;
...
public class MyController
{
    private readonly ILegacyEmailService emailService;

    public MyController(ILegacyEmailService emailService)
    {
        this.emailService = emailService;
    }

    public async Task MyMethod()
    {
        var from = new MailAddress("me@mydomain.net");
        var to = new string[] { "someone@mydomain.net" };
        var subject = "My subject";
        var body = "My body";

        await emailService.SendEmail(from, to, subject, body);
    }
}
```

## License

This project is licensed under the MIT License.

## Contributions

Contributions are welcome.
