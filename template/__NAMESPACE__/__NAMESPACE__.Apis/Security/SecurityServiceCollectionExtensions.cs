using __NAMESPACE__.Domain.Services.Setting;
using __NAMESPACE__.Repository.Abstractions.Security;
using __NAMESPACE__.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static OpenIddict.Server.OpenIddictServerEvents;

namespace __NAMESPACE__.Apis.Security
{
    public static class SecurityServiceCollectionExtensions
    {
        public static IServiceCollection UseSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var basePath = configuration.GetValue<string>("ApiOptions:BasePath");
            var issuerUri = configuration.GetValue<string>("SecurityOptions:Issuer");
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var validIssuer = configuration.GetValue<string>("SecurityOptions:Issuer");
            var validAudience = configuration.GetValue<string>("SecurityOptions:Audience");
            var securityKey = configuration.GetValue<string>("SecurityOptions:SecurityKey");
            var expirationInSeconds = configuration.GetValue<int>("SecurityOptions:ExpirationInSeconds");
            var certificatePassword = configuration.GetValue<string>("SecurityOptions:CertificatePassword");

            #region Identity
            services
                .AddIdentity<Entity.ApplicationUser, Entity.ApplicationRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<CoreDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region IdentityOptions
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                if (configuration.GetValue<bool>("SignInOptions:LockoutEnabled"))
                {
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = configuration.GetValue<int>("SignInOptions:LockoutMaxFailedAccessAttempts");
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(configuration.GetValue<int>("SignInOptions:LockoutDefaultTimeSpanInMinutes"));
                }
            });
            #endregion

            #region OpenIddict
            services
                .AddOpenIddict()
                .AddCore(options =>
                {
                    options
                        .UseEntityFrameworkCore()
                        .UseDbContext<CoreDbContext>();
                })
                .AddServer(options =>
                {
                    options
                        .SetTokenEndpointUris("api/user/login")
                        .SetUserInfoEndpointUris("api/user/info");

                    // To add scopes dynamically from storage
                    options
                        .AddEventHandler<ValidateTokenRequestContext>(builder =>
                        {
                            builder.UseScopedHandler<ValidateScopesHandler>();
                            builder.SetOrder(int.MinValue);
                        });

                    options
                        .AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    options.AddSigningCertificate(
                        new FileStream("Certificates/hanseatica.com.pe.p12", FileMode.Open), certificatePassword
                    );
                    
                    options.AddEncryptionCertificate(
                        new FileStream("Certificates/hanseatica.com.pe.p12", FileMode.Open), certificatePassword
                    );

                    options
                        .UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        .EnableUserInfoEndpointPassthrough();

                    options.SetAccessTokenLifetime(TimeSpan.FromSeconds(expirationInSeconds));
                    options.SetIdentityTokenLifetime(TimeSpan.FromSeconds(expirationInSeconds));
                    options.SetAuthorizationCodeLifetime(TimeSpan.FromSeconds(expirationInSeconds));
                    options.SetRefreshTokenLifetime(TimeSpan.FromDays(1));
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });
            #endregion

            #region UserIdentity
            services.AddScoped<IUserIdentity, UserIdentity>();
            #endregion

            return services;
        }
    }
}
