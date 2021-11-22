using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication2.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication2.Startup))]
namespace WebApplication2
{

    public partial class Startup

    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            CrateDefaultRolesAndUsers();
        }
        

        public void CrateDefaultRolesAndUsers()
        {


          var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role = new IdentityRole();
            if (!RoleManager.RoleExists("Admins"))
            {
                role.Name = "Admins";
                RoleManager.Create(role);

                ApplicationUser User = new ApplicationUser();
                User.UserName = "Abotale";
                User.Email = "mohamedabotaleb130@gamil.com";
                var Chek = UserManager.Create(User, "Abotaleb@123456#");
                if (Chek.Succeeded)
                {
                    UserManager.AddToRole(User.Id, "Admins");
                }
                   


            }


        }

    }
}
