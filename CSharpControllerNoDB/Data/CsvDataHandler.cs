using CSharpControllerNoDB.Models;

namespace CSharpControllerNoDB.Data
{
    public class CsvDataHandler
    {
        public static HashSet<Student> ReadStudentsFromCsv()
        {
            var students = new HashSet<Student>(); // custom comparer for hashset

            using (var reader = new StreamReader("/dane.csv"))
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
                    // TODO: check for duplicates(done using the implemented Equals and GetHashCode methods in Student)
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

        public static void UpdateStudent(Student student)
        {

        }
    }
}
