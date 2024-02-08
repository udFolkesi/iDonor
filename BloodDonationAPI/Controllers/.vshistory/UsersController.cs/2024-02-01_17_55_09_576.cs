using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly iDonorDbContext dbContext;

        public UsersController(iDonorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: UsersController
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            //return View();
            return dbContext.User.Include(u => u.Client).ToList();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            try
            {
                IQueryable<User> query = dbContext.User
                    .Include(c => c.Client)
                    .Include(u => u.Client.Blood)
                    .Include(u => u.Client.DonationsAsDonor)
                        .ThenInclude(d => d.BloodBank)
                    .Include(u => u.Client.DonationsAsPatient)
                        .ThenInclude(d => d.BloodBank);

                var user = await query.FirstOrDefaultAsync(c => c.UserID == userId);

                if (user == null)
                {
                    return NotFound(); // 404 Not Found if client is not found
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user2)
        {
            var user = await dbContext.User.Where(u => u.Email == user2.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                ModelState.AddModelError("", "User with such email already exists");
                return StatusCode(422, ModelState);
            }

/*            var userCreate = new User()
            {
                UserID = user2.UserID,
                Username = user2.Username,
                Name = user2.Name,
                Surname = user2.Surname,
                Patronymic = user2.Patronymic,
                Role = user2.Role,
                Email = user2.Email,
                Password = user2.Password,
            };*/

            await dbContext.User.AddAsync(user2);
            await dbContext.SaveChangesAsync();

            return Ok(user2);
        }

        [HttpPut]
        [Route("{userID:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int userID, [FromBody]User updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userID != updatedUser.UserID)
                return BadRequest(ModelState);

            dbContext.Entry(updatedUser).State = EntityState.Modified;
            //await dbContext.User.UpdateAsync(updatedUser);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!dbContext.User.Any(e => e.UserID == userID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task DeleteUser(int userId)
        {
            var userToDelete = await dbContext.User.FindAsync(userId);
            dbContext.User.Remove(userToDelete);
            await dbContext.SaveChangesAsync();
            //return Ok();
        }
    }
}
