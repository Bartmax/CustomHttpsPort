# CustomPortHttps
CustomPortHttpsMiddleware for ASPNET5 beta5.
This will change port for every redirect to https. Useful in combination of [RequireHttps] attribute on localhost.

When working on localhost in a custom port (1234)
if you use something like [RequireHttps] attribute it will be redirected to https://localhost:1234
but you probabily have https in another port. (43300)

This middleware detect every MovedPermanently (301) to https scheme and change the port to the configured one.

Since the intent is to use this only on development, i suggest configuring it like this:

```
public void ConfigureDevelopment(IApplicationBuilder app)
{
    app.UseCustomPortHttps(44300);
    ...
    Configure(app);
}
```
