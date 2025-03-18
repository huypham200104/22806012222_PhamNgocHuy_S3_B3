using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _22806012222_PhamNgocHuy_S3_B3.Data;

public class _22806012222_PhamNgocHuy_S3_B3Context : IdentityDbContext<IdentityUser>
{
    public _22806012222_PhamNgocHuy_S3_B3Context(DbContextOptions<_22806012222_PhamNgocHuy_S3_B3Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
