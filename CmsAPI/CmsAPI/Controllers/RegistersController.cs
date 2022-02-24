using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly CMSContext _context;

        public RegistersController(CMSContext context)
        {
            _context = context;
        }

        // GET: api/UserSetups
        [HttpGet]
        //Get all the list of users
        public async Task<ActionResult<IEnumerable<Registration>>> GetUserSetup()
        {
            return await _context.Registration.ToListAsync();
        }
        //Get user record by Email ID
        
        [Route("[action]/{Email}")]
        [HttpGet]
        public Registration GetUserSetupbyEmail(string Email)
        {
            Registration userSetup = (from i in _context.Registration.ToList()
                             where i.Email == Email
                             select i).FirstOrDefault();

            if (userSetup == null)
            {
                return userSetup;
            }

            return userSetup;
        }

        // GET: api/UserSetups/
        [Route("[action]/{Username}")]
        [HttpGet]
        //Get a userrecord by username
        public async Task<ActionResult<Registration>> GetUserSetup(string Username)
        {
            var userSetup = await _context.Registration.FindAsync(Username);

            if (userSetup == null)
            {
                return NotFound();
            }

            return userSetup;
        }
        
        // PUT: api/UserSetups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Username}")]
        //Edit the user details by id
        public async Task<IActionResult> PutUserSetup(string Username, Registration userSetup)
        {
            foreach(var i in _context.Registration.ToList())
            {
                if(i.Username == Username)
                {
                    
                    i.Firstname = userSetup.Firstname;
                    i.Lastname = userSetup.Lastname;
                    i.Email = userSetup.Email;
                    i.Password = userSetup.Password;
                    i.ConfirmPassword = userSetup.ConfirmPassword;
                    i.MobileNo = userSetup.MobileNo;
                    i.SecurityCode = userSetup.SecurityCode;
                    i.Status = userSetup.Status;
                    i.CreationDate = userSetup.CreationDate;
                    _context.SaveChanges();
                    return Ok(200);

                }

            }
            return BadRequest();
          
        }

        // POST: api/UserSetups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //Add a user
        public async Task<ActionResult<Registration>> PostUserSetup(Registration userSetup)
        {
            _context.Registration.Add(userSetup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserSetupExists(userSetup.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserSetup", new { Username = userSetup.Username }, userSetup);
        }

        // DELETE: api/UserSetups/5
        [HttpDelete("{Username}")]
        //Delete a user by id
        public async Task<IActionResult> DeleteUserSetup(string Username)
        {
            var userSetup = await _context.Registration.FindAsync(Username);
            if (userSetup == null)
            {
                return NotFound();
            }

            _context.Registration.Remove(userSetup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSetupExists(string Username)
        {
            return _context.Registration.Any(e => e.Password == Username);
        }
    }
}
