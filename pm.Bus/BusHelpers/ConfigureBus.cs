using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace pm.Bus.BusHelpers
{
    public class ConfigureBus
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IUserBus, UserBus>();
            services.AddScoped<IClaimJwtBus, ClaimJwtBus>();
            //services.AddScoped<IAccountManager, AccountManager>();
        }

    }
}
