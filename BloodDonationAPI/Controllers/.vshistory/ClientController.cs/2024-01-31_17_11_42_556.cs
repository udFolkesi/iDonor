using AutoMapper;
using BloodDonationAPI.Data;
using BloodDonationAPI.Dto;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : Controller
    {
        private readonly iDonorDbContext dbContext;
        private readonly IMapper _mapper;

        public ClientController(iDonorDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<Client>> GetClient(int clientId)
        {
            try
            {
                IQueryable<Client> query = dbContext.Client
                    .Include(c => c.User)
                    .Include(c => c.Blood)
                    .Include(c => c.DonationsAsDonor)
                    .Include(c => c.DonationsAsPatient);


                var client = await query.FirstOrDefaultAsync(c => c.ClientID == clientId);

                if (client == null)
                {
                    return NotFound(); // 404 Not Found if client is not found
                }

/*                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    //MaxDepth = 256 // Adjust the depth as needed
                };

                var jsonResult = JsonSerializer.Serialize(client, jsonOptions);*/

                return Ok(client);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IEnumerable<Client> GetClients()
        {
            return dbContext.Client
                .Include(c => c.Blood)
                .Include(c => c.User)
                .Include(c => c.DonationsAsDonor)
                .Include(c => c.DonationsAsPatient)
                .ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromQuery] int userId, [FromQuery] int bloodId, [FromBody] ClientDto clientCreate)
        {
/*            var bloodEntity = await dbContext.Blood.FindAsync(bloodId);
            if (bloodEntity == null)
            {
                return NotFound("Blood not found");
            }

            var userEntity = await dbContext.User.FindAsync(userId);
            if (userEntity == null)
            {
                return NotFound("User not found");
            }*/
            var clientMap = _mapper.Map<Client>(clientCreate);

            //clientMap.Blood = await dbContext.Blood.FindAsync(bloodId); 
            //clientMap.User = await dbContext.User.FindAsync(userId);

            // ADD CLIENT INSTANCE TO USER'S ONE

            clientMap.BloodID = bloodId;
            clientMap.UserID = userId;

            await dbContext.Client.AddAsync(clientMap);
            await dbContext.SaveChangesAsync();

            return Ok(clientCreate);
        }

        [HttpPut]
        [Route("{clientID:int}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int clientID, [FromBody] ClientDto updatedClient)
        {
            if (updatedClient == null)
                return BadRequest(ModelState);

            if (clientID != updatedClient.ClientID)
                return BadRequest(ModelState);

            try
            {
                var existingClient = await dbContext.Client.FindAsync(clientID);

                if (existingClient == null)
                {
                    return NotFound();
                }

                _mapper.Map(updatedClient, existingClient);

                dbContext.Entry(existingClient).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dbContext.Client.Any(e => e.ClientID == clientID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{clientId}")]
        public async Task DeleteClient(int clientId)
        {
            var clientToDelete = await dbContext.Client.FindAsync(clientId);
            dbContext.Client.Remove(clientToDelete);
            await dbContext.SaveChangesAsync();
            //return Ok();
        }
    }
}
