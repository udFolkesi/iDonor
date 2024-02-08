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
            return dbContext.User.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetClient(int userId)
        {
            try
            {
                IQueryable<Client> query = dbContext.Client
                    .Include(c => c.User)
                    .Include(c => c.Blood)
                    .Include(c => c.DonationsAsDonor)
                    .Include(c => c.DonationsAsPatient);


                var client = await query.FirstOrDefaultAsync(c => c.ClientID == userId);

                if (client == null)
                {
                    return NotFound(); // 404 Not Found if client is not found
                }

                return Ok(client);
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
    }
}
