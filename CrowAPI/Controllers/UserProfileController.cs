using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly DatabaseContext _db;

    public UserProfileController(DatabaseContext db)
    {
        _db = db;
    }

    // GET: api/UserProfile
    [HttpGet]
    public IActionResult GetUserProfiles()
    {
        var users = _db.UserProfiles.ToList();
        return Ok(users);
    }

    // GET: api/UserProfile/{id}
    [HttpGet("{id}")]
    public IActionResult GetUserProfile(int id)
    {
        var user = _db.UserProfiles.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // POST: api/UserProfile
    [HttpPost]
    public IActionResult AddUserProfile([FromBody] UserProfile userProfile)
    {
        _db.UserProfiles.Add(userProfile);
        _db.SaveChanges();
        return CreatedAtAction(nameof(GetUserProfile), new { id = userProfile.Id }, userProfile);
    }

    // PUT: api/UserProfile/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUserProfile(int id, [FromBody] UserProfile updatedUser)
    {
        var user = _db.UserProfiles.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        user.Bio = updatedUser.Bio;
        user.ProfileImagePath = updatedUser.ProfileImagePath;

        _db.SaveChanges();
        return NoContent();
    }

    // DELETE: api/UserProfile/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteUserProfile(int id)
    {
        var user = _db.UserProfiles.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        _db.UserProfiles.Remove(user);
        _db.SaveChanges();
        return NoContent();
    }
}
