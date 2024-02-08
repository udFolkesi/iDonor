﻿using AutoMapper;
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

        [HttpPost]
        public async Task<IActionResult> AddDonationOperation([FromBody]DonationOperationDto operationCreate)
        {
            var operationMap = _mapper.Map<DonationOperation>(operationCreate);

            await dbContext.DonationOperation.AddAsync(operationMap);
            await dbContext.SaveChangesAsync();
            
            return Ok(operationCreate);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOperation(int id, [FromBody] DonationOperationDto updatedOperation)
        {
            var existingOperation = dbContext.DonationOperation.FindAsync(id);

            _mapper.Map(updatedOperation, existingOperation);

            dbContext.Entry(existingOperation).State = EntityState.Modified;
            dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
