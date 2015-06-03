# CustomPortHttps Middleware
## Detect any MovedPermanently (301) response and redirect the request to the custom specified port if it's https scheme.

*Useful in combination of `[RequireHttps]` attribute while developing on localhost.*

When working on localhost in a custom port (1234)

if you use something like `[RequireHttps]` attribute it will be redirected to `https://localhost:1234`

but you probabily have https in another port. (43300)

This middleware is aimed to provide an easy way to redirect to the correct port.

How to use it?

Configure your application to serve on https on any custom port. If you are using IISExpress, there's already a certificate for ports 433xx.
Then add to your Startup.cs class
```
public void ConfigureDevelopment(IApplicationBuilder app)
{
    app.UseCustomPortHttps(44300);
    ...
    Configure(app);
}
```

*note that `ConfigureDevelopment` will only be called if you have an environment variable calles 'Development'*
