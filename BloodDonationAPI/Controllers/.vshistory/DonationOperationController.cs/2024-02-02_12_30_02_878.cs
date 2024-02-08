using AutoMapper;
using BloodDonationAPI.Data;
using BloodDonationAPI.Dto;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationOperationController : Controller
    {
        private readonly iDonorDbContext dbContext;
        private readonly IMapper _mapper;

        public DonationOperationController(iDonorDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<User> GetOperations()
        {
            //return View();
            return dbContext.User.Include(u => u.Client).ToList();
        }

        [HttpGet("{operationId}")]
        public async Task<DonationOperation> GetOperation(int operationId)
        {
            return await dbContext.DonationOperation.FindAsync(operationId);
        }

        [HttpPost]
        public async Task<IActionResult> AddDonationOperation([FromQuery] int bankId, [FromQuery] int donorId, [FromQuery] int? patientId, [FromBody]DonationOperationDto operationCreate)
        {
            var operationMap = _mapper.Map<DonationOperation>(operationCreate);
            operationMap.BloodBankID = bankId;
            operationMap.DonorID = donorId;
            operationMap.PatientID = patientId;

            await dbContext.DonationOperation.AddAsync(operationMap);
            await dbContext.SaveChangesAsync();
            
            return Ok(operationCreate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperation(int id, [FromBody] DonationOperationDto updatedOperation)
        {
            var existingOperation = await dbContext.DonationOperation.FindAsync(id);

            _mapper.Map(updatedOperation, existingOperation);

            dbContext.Entry(existingOperation).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
