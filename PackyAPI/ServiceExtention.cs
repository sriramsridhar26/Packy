using Microsoft.AspNetCore.Identity;
using PackyAPI.Data;

namespace PackyAPI
{
    public static class ServiceExtention
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builders = services.AddIdentityCore<ApiUser>(q=>q.User.RequireUniqueEmail=true);
            builders = new IdentityBuilder(builders.UserType, typeof(IdentityRole), services);
            builders.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }
    }
}
