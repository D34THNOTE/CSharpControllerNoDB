using CSharpControllerNoDB.Data;
using CSharpControllerNoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CSharpControllerNoDB.Controllers
{
    [ApiController]
    // [controller] is replaced with the name of this class minus the "Controller" word, so if it was called CEyyghgfhfController, then the URL would be api/CEyyghgfhf
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            HashSet<Student> students = CsvDataHandler.ReadStudentsFromCsv();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id)
        {
            // Checking if the incoming ID is matching the required pattern
            var validationError = ValidateIdFormat(id);
            if (validationError != null)
            {
                return validationError;
            }

            HashSet<Student> students = CsvDataHandler.ReadStudentsFromCsv();

            // Again, returns null if no match was found
            Student student = students.FirstOrDefault(s => s.IndexNumber == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> Post(Student studentData)
        {
            // Built-in class, ModelState will be replaced with "student" after successful binding incoming data to the Student object(model binding)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStudent = new Student
            {
                Name = studentData.Name,
                Surname = studentData.Surname,
                IndexNumber = studentData.IndexNumber,
                DateOfBirth = studentData.DateOfBirth,
                Studies = studentData.Studies,
                Mode = studentData.Mode,
                Email = studentData.Email,
                FathersName = studentData.FathersName,
                MothersName = studentData.MothersName
            };

            bool success = CsvDataHandler.WriteStudentToCsv(newStudent);

            if (!success)
            {
                return BadRequest("A student with given index number already exists");
            }

            return Ok(newStudent);
        }

        [HttpPut("{id}")]
        public ActionResult<Student> Update(string id, [FromBody] Student updatedStudent)
        {
            var validationError = ValidateIdFormat(id);
            if (validationError != null)
            {
                return validationError;
            }

            // UpdateStudent returns a false if no match was found and true after overwriting the .csv file
            bool success = CsvDataHandler.UpdateStudent(updatedStudent);
            if (!success)
            {
                return BadRequest("There was no match for the provided student, update failed");
            }

            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var validationError = ValidateIdFormat(id);
            if (validationError != null)
            {
                return validationError;
            }

            var deleted = CsvDataHandler.DeleteStudent(id);

            if(deleted)
            {
                return Ok($"Student with ID {id} successfully deleted");
            } 
            else
            {
                return NotFound($"Student with ID {id} not found");
            }
        }

        private ActionResult ValidateIdFormat(string id)
        {
            bool isValidIndexNumber = Regex.IsMatch(id, "^s\\d+$");
            if (!isValidIndexNumber)
            {
                return BadRequest("Invalid ID format. The accepted format looks like: 's1234' ");
            }

            return null;
        }

        public StudentsController()
        {

        }
    }
}
