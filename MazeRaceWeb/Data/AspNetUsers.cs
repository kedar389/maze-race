using Microsoft.AspNetCore.Identity;

namespace MazeRaceWeb.Data;

public class AspNetUsers:IdentityUser
{
    public String Name { get; set; }
}