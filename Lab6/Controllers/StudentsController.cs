using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Controllers
{

    [ApiController] // enforces attribute routing for controller class
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }


        // GET: all Students
        /// <summary>
        /// Get collection of Students.
        /// </summary>
        /// <returns>A collection of Students</returns>
        /// <response code="200">Returns a collection of Students</response>
        /// <response code="500">Internal error</response>    
        [HttpGet]
        [Route("/[controller]/[action]")] //route = the controller and this GET action
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return Ok(await _context.Students.ToListAsync());
        }


        // GET: single Student
        /// <summary>
        /// Get a Student.
        /// </summary>
        /// <param id="id"></param>
        /// <returns>A Student</returns>
        /// <response code="200">Returns a collection of Students</response>
        /// <response code="400">If the id is malformed</response>      
        /// <response code="404">If the Student is null</response>      
        /// <response code="500">Internal error</response>
        [HttpGet("{id}")] // GET localhost/Students/.....
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetById(Guid id)
        {
            Student student = await _context.Students.FindAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        // POST: Students
        /// <summary>
        /// Creates a Student.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Cars
        ///     {
        ///        "firstName": "firstName",
        ///        "lastName": "lastName",
        ///        "program": "program"
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created Student</returns>
        /// <response code="201">Returns the newly created Student</response>
        /// <response code="400">If the Student is malformed</response>      
        /// <response code="500">Internal error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> CreateAsync([Bind("FirstName,LastName,Program")] StudentBase studentBase)
        {
            Student stu = new Student
            {
                FirstName = studentBase.FirstName,
                LastName = studentBase.LastName,
                Program = studentBase.Program
            };

            _context.Add(stu);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stu.ID }, stu);
        }


        // DELETE: Students/5
        /// <summary>
        /// Deletes a Student.
        /// </summary>
        /// <param id="id"></param>
        /// <response code="202">Student is deleted</response>
        /// <response code="400">If the id is malformed</response>      
        /// <response code="500">Internal error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var stu = await _context.Students.FindAsync(id);
            _context.Students.Remove(stu);
            await _context.SaveChangesAsync();
            return Accepted(); // 202 accepted response
        }


        // PUT: Student/5
        /// <summary>
        /// Updates a Student.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Students
        ///     {
        ///        "firstName": "firstName",
        ///        "lastName": "lastName",
        ///        "program": "program"
        ///     }
        ///
        /// </remarks>
        /// <param id="id"></param>
        /// <returns>An updated Student</returns>
        /// <response code="200">Returns the updated Student</response>
        /// <response code="201">Returns the newly created Student</response>
        /// <response code="400">If the Student or id is malformed</response>
        /// <response code="404">If the Student is null</response>
        /// <response code="500">Internal error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> Update(Guid id, [Bind("FirstName,LastName,Program")] StudentBase studentBase)
        {
            Student stu = new Student
            {
                FirstName = studentBase.FirstName,
                LastName = studentBase.LastName,
                Program = studentBase.Program
            };

            if (!StudentExists(id))
            {
                return NotFound(); // 404 error response
                
               // stu.ID = id;
               // _context.Add(stu);
               // await _context.SaveChangesAsync();
               // return CreatedAtAction(nameof(GetById), new { id = stu.ID }, stu);
            }

            Student dbStudent = await _context.Students.FindAsync(id);
            dbStudent.FirstName = stu.FirstName;
            dbStudent.LastName = stu.LastName;
            dbStudent.Program = stu.Program;

            _context.Update(dbStudent);
            await _context.SaveChangesAsync();

            return Ok(dbStudent); // 200 ok response
        }


        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
