﻿using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> AddUser(User user2)
        {
            var user = new User()
            {
                UserID = user2.UserID,
                Username = user2.Username,
                Name = user2.Name,
                Surname = user2.Surname,
                Patronymic = user2.Patronymic,
                Role = user2.Role,
                Email = user2.Email,
                Password = user2.Password,
            };

            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{UserID}")]
        public IActionResult UpdateCategory(int countryId, [FromBody] CountryDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (countryId != updatedUser.Id)
                return BadRequest(ModelState);

            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(updatedUser);

            if (!_countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /*       // GET: UsersController/Details/5
               public ActionResult Details(int id)
               {
                   return View();
               }

               // GET: UsersController/Create
               public ActionResult Create()
               {
                   return View();
               }

               // POST: UsersController/Create
               [HttpPost]
               [ValidateAntiForgeryToken]
               public ActionResult Create(IFormCollection collection)
               {
                   try
                   {
                       return RedirectToAction(nameof(Index));
                   }
                   catch
                   {
                       return View();
                   }
               }

               // GET: UsersController/Edit/5
               public ActionResult Edit(int id)
               {
                   return View();
               }

               // POST: UsersController/Edit/5
               [HttpPost]
               [ValidateAntiForgeryToken]
               public ActionResult Edit(int id, IFormCollection collection)
               {
                   try
                   {
                       return RedirectToAction(nameof(Index));
                   }
                   catch
                   {
                       return View();
                   }
               }

               // GET: UsersController/Delete/5
               public ActionResult Delete(int id)
               {
                   return View();
               }

               // POST: UsersController/Delete/5
               [HttpPost]
               [ValidateAntiForgeryToken]
               public ActionResult Delete(int id, IFormCollection collection)
               {
                   try
                   {
                       return RedirectToAction(nameof(Index));
                   }
                   catch
                   {
                       return View();
                   }
               }*/
    }
}
