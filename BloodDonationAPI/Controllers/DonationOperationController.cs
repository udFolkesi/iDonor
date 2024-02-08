using AutoMapper;
using BloodDonationAPI.Business.Services;
using BloodDonationAPI.Data;
using BloodDonationAPI.Dto;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DonationOperationController : Controller
    {
        private readonly iDonorDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly OperationService _operationService;

        public DonationOperationController(iDonorDbContext dbContext, IMapper mapper, OperationService operationService)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            _operationService = operationService;
        }

        [HttpGet]
        public IEnumerable<DonationOperation> GetOperations()
        {
            //return View();
            return dbContext.DonationOperation
                //.Include(d => d.BloodBank)
                .Include(d => d.Donor)
                    .ThenInclude(d => d.Blood)
                .Include(d => d.Patient)
                    .ThenInclude(d => d.Blood)
                .ToList();
        }

        [HttpGet]
        public IEnumerable<DonationOperation> GetOperationsShort()
        {
            //return View();
            return dbContext.DonationOperation
                //.Include(d => d.BloodBank)
/*                .Include(d => d.Donor)
                .Include(d => d.Patient)*/
                .ToList();
        }

        [HttpGet("{operationId}")]
        public async Task<DonationOperation> GetOperation(int operationId)
        {
            return await dbContext.DonationOperation.FindAsync(operationId);
        }

        [HttpGet("search")]
        public IEnumerable<DonationOperation> SearchOperations(int bloodGroup, string clients)
        {
            return _operationService.Search(bloodGroup, clients);
        }

        [HttpGet("sorting")]
        public IEnumerable<DonationOperation> SortOperations(string order, string clients)
        {
            return _operationService.Sort(order, clients);
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
