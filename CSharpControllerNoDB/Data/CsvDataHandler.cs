using CSharpControllerNoDB.Models;

namespace CSharpControllerNoDB.Data
{
    public class CsvDataHandler
    {
        public static HashSet<Student> ReadStudentsFromCsv()
        {
            var students = new HashSet<Student>();

            using (var reader = new StreamReader("./dane.csv"))
            using (var logWriter = new StreamWriter("./log.txt", true))
            {
                string line;
                int lineCounter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCounter++;
                    string[] columns = line.Split(',', StringSplitOptions.RemoveEmptyEntries); // added StringSplitOptions to make columns.Length relevant

                    if (columns.Length != 9)
                    {
                        logWriter.WriteLine($"(Line {lineCounter}) - Rejected input: {line}");
                        Console.WriteLine("An incomplete line added to log.txt");
                        continue;
                    }

                    var name = columns[2];
                    var mode = columns[3];
                    var student = new Student
                    {
                        IndexNumber = columns[4],
                        Name = columns[0],
                        Surname = columns[1],
                        DateOfBirth = DateTime.Parse(columns[5]),
                        Studies = columns[2],
                        Mode = columns[3],
                        Email = columns[6],
                        MothersName = columns[7],
                        FathersName = columns[8],
                    };
                    students.Add(student);
                }
            }
            return students;
        }
        public static void WriteStudentToCsv(Student student)
        {
            using (var logWriter = new StreamWriter("./dane.csv", true))
            {
                logWriter.WriteLine(
                    $"{student.Name}," +
                    $"{student.Surname}," +
                    $"{student.Studies}," +
                    $"{student.Mode}," +
                    $"{student.IndexNumber}," +
                    $"{student.DateOfBirth}," +
                    $"{student.Email}," +
                    $"{student.MothersName}," +
                    $"{student.FathersName}"
                    );
            }
        }

        public static bool UpdateStudent(Student student)
        {
            HashSet<Student> studentList = ReadStudentsFromCsv();

            // Using FirstOrDefault because it will return "null" if no record is found
            Student studentToUpdate = studentList.FirstOrDefault(s => s.IndexNumber == student.IndexNumber);

            if (studentToUpdate == null)
            {
                // Handle error - student not found
                return false;
            }

            // No IndexNumber as this is the unique identifier
            studentToUpdate.Name = student.Name;
            studentToUpdate.Surname = student.Surname;
            studentToUpdate.Studies = student.Studies;
            studentToUpdate.Mode = student.Mode;
            studentToUpdate.DateOfBirth = student.DateOfBirth;
            studentToUpdate.Email = student.Email;
            studentToUpdate.FathersName = student.FathersName;
            studentToUpdate.MothersName = student.MothersName;

            // false paratemer to make sure we're overwriting the existing file
            using (var writer = new StreamWriter("./dane.csv", false))
            {
                foreach (var stud in studentList)
                {
                    writer.WriteLine(
                        $"{stud.Name}," +
                        $"{stud.Surname}," +
                        $"{stud.Studies}," +
                        $"{stud.Mode}," +
                        $"{stud.IndexNumber}," +
                        $"{stud.DateOfBirth}," +
                        $"{stud.Email}," +
                        $"{stud.MothersName}," +
                        $"{stud.FathersName}"
                        );
                }
            }

            return true;
        }
    }
}
