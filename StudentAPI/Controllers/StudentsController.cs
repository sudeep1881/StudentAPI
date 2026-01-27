
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;


namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentApIdbContext _context;

        public StudentsController(StudentApIdbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet("getall")]
        public IActionResult GetStudents()
        {
            return Ok(_context.Students.ToList());
        }

        // POST: api/students
        [HttpPost]
        public IActionResult AddStudent([FromBody]Student student)
        {
            if (student == null)
            return BadRequest("Please Enter Values");
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);

        }


        [HttpGet("{id}")]
        public IActionResult ParticularStudent([FromRoute]int id)
        {
            var StudentDetails = _context.Students.Where(s => s.Id == id).FirstOrDefault();

            if (StudentDetails == null)
                return BadRequest("Student not Found");

            return Ok(StudentDetails);

        }


        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute]int id,[FromBody]Student student)
        {

            if(id != student.Id)
            {
                return BadRequest("Id Miss Matching");
            }

            var existingStudent = _context.Students.Find(id);

            if (existingStudent == null)
                return NotFound("Student Not Found");

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Email = student.Email;

            _context.Students.Update(student);
            _context.SaveChanges();

            return Ok(existingStudent);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound("Student Id Not Found");

            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok("Delete Data Successfully");
        }

        [HttpGet("state")]
        public IActionResult StateNameAll()
        {
            return Ok(_context.Students.ToList());
        }

    }
}
