using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace lab8
{
    class Student
    {
        public int student_id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public double? stipend { get; set; }
        public int kurs { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4}", student_id, surname, name, stipend, kurs);
        }
    }

    class ExamMark
    {
        public int student_id { get; set; }
        public int subj_id { get; set; }
        public int? mark { get; set; }
    }

    class Lecturer
    {
        public int lecturer_id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
    }

    class Subject
    {
        public int subject_id { get; set; }
        public string subj_name { get; set; }
    }

    class SubjLect
    {
        public int subject_id { get; set; }
        public int lecturer_id { get; set; }
    }

    class Program
    {
        NpgsqlConnection conn;
        List<Student> students = new List<Student>();
        List<ExamMark> examMarks = new List<ExamMark>();
        List<Lecturer> lectures = new List<Lecturer>();
        List<Subject> subjects = new List<Subject>();
        List<SubjLect> subj_lects = new List<SubjLect>();

        static void Main(string[] args)
        {
            var program = new Program();
        }

        public Program() {
            conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User=Postgres;Password=" ";Database=university;Encoding=unicode");

            GetStudents("select * from student");
            GetExamMarks("select * from exam_marks");
            GetLectures("select * from lectures");
            GetSubject("select * from subject");
            GetSubjLect("select * from subj_lect");

            First();
            Second();
            Third();
            Fourth();
            Fifth();
        }

        public void GetStudents(string query)
        {
            NpgsqlCommand adapter = new NpgsqlCommand(query, conn);

            conn.Open();

            using (var reader = adapter.ExecuteReader())
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        student_id = reader.GetInt32(0),
                        surname = reader.GetString(1),
                        name = reader.GetString(2),
                        stipend = reader.GetValue(3) == DBNull.Value ? null : (double?)reader.GetValue(3),
                        kurs = reader.GetInt32(4)
                    });
                }
            }

            conn.Close();
        }

        public void GetExamMarks(string query)
        {
            NpgsqlCommand adapter = new NpgsqlCommand(query, conn);

            conn.Open();

            using (var reader = adapter.ExecuteReader())
            {
                while (reader.Read())
                {
                    examMarks.Add(new ExamMark
                    {
                        student_id = reader.GetInt32(1),
                        subj_id = reader.GetInt32(2),
                        mark = reader.GetValue(3) == DBNull.Value ? null : (int?)reader.GetValue(3)
                    });
                }
            }

            conn.Close();
        }

        public void GetLectures(string query)
        {
            NpgsqlCommand adapter = new NpgsqlCommand(query, conn);

            conn.Open();

            using (var reader = adapter.ExecuteReader())
            {
                while (reader.Read())
                {
                    lectures.Add(new Lecturer
                    {
                        lecturer_id = reader.GetInt32(0),
                        surname = reader.GetString(1),
                        name = reader.GetString(2)
                    });
                }
            }

            conn.Close();
        }

        public void GetSubject(string query)
        {
            NpgsqlCommand adapter = new NpgsqlCommand(query, conn);

            conn.Open();

            using (var reader = adapter.ExecuteReader())
            {
                while (reader.Read())
                {
                    subjects.Add(new Subject
                    {
                        subject_id = reader.GetInt32(0),
                        subj_name = reader.GetString(1)
                    });
                }
            }

            conn.Close();
        }

        public void GetSubjLect(string query)
        {
            NpgsqlCommand adapter = new NpgsqlCommand(query, conn);

            conn.Open();

            using (var reader = adapter.ExecuteReader())
            {
                while (reader.Read())
                {
                    subj_lects.Add(new SubjLect
                    {
                        lecturer_id = reader.GetInt32(0),
                        subject_id = reader.GetInt32(1)
                    });
                }
            }

            conn.Close();
        }

        public void First()
        {
            Console.WriteLine("Задание №1");
            foreach (Student st in students.Where(x => x.name.StartsWith("И") || x.surname.StartsWith("И")))
            {
                Console.WriteLine(st);
            }
            Console.WriteLine("\n");
        }

        public void Second()
        {
            Console.WriteLine("Задание №2");
            var _students = students
                .Join(examMarks, s => s.student_id, m => m.student_id, (s, m) => new { s.surname, s.name, m.mark })
                .OrderBy(x => x.surname);

            foreach (var st in _students)
            {
                //Console.WriteLine("{0} {1} {2}", st.surname, st.name, st.mark);
            }
            Console.WriteLine("\n");
        }

        public void Third()
        {
            Console.WriteLine("Задание №3");

            var _subj_lect = subj_lects
                .Join(lectures, x => x.lecturer_id, l => l.lecturer_id, (x, l) => new { l.surname, x.subject_id })
                .Join(subjects, x => x.subject_id, s => s.subject_id, (x, s) => new { x.surname, s.subj_name });

            foreach (var st in _subj_lect)
            {
                Console.WriteLine($"{st.surname} {st.subj_name}");
            }
            Console.WriteLine("\n");
        }

        public void Fourth()
        {
            Console.WriteLine("Задание №4");

            var _students = students
                .Where(s => s.kurs >= 3 && s.stipend == students.Max(st => st.stipend));

            foreach (Student st in _students)
            {
                Console.WriteLine(st);
            }
            Console.WriteLine("\n");
        }

        public void Fifth()
        {
            Console.WriteLine("Задание №5");

            var _avg = examMarks
                .Join(subjects, e => e.subj_id, s => s.subject_id, (e, s) => new { e.mark, s.subj_name })
                .Where(s => s.subj_name == "Информатика")
                .Average(x => x.mark);

            Console.WriteLine(_avg);
            Console.WriteLine("\n");
        }
    }
}
