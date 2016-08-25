using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentApp.Tests
{
    public class RepositoryTests
    {

        private Data.IRepository Repository = new Data.Repository();
        [Test]
        public void GetTeacherTest()
        {
            var t = Repository.GetTeacher("toddhilehoffer@yahoo.com");  
            if (t != null && !string.IsNullOrEmpty (t.UserId) )
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void TeachClassesStudentsTest()
        {
            var t = Repository.GetTeacher("toddhilehoffer@yahoo.com");
            var @class = new AssessmentApp.Data.Models.Class
            {
                TeacherId = t.Id,
                ClassName = "Class - " + Guid.NewGuid().ToString(),
                Active = true
            }; 
            Repository.CreateClass(@class); 
            if (!Repository.CheckIfClassAlreadyExists(t.Id, @class.ClassName)) {
                Assert.Fail("CheckIfClassAlreadyExists should return true");
            }


            Repository.UpdateClass(@class);


            var c = Repository.GetClass(@class.Id.Value);
            if (c == null) {
                Assert.Fail("Where is the class");
            }


            var s = new Data.Models.Student
            {
                ClassId = @class.Id,
                StudentDateOfBirth = DateTime.Parse("1/1/2010"),
                StudentName = "Todd Test",
                Active = true
            };
            s = Repository.CreateStudent(s);

            s.StudentName = "Todd Test 2";
            Repository.UpdateStudent(s);

            var students = Repository.GetStudents(@class.TeacherId);
            if (!students.Any())
                Assert.Fail("Where are the students");

            if (!Repository.IsChild(t.Id, s))
                Assert.Fail("Is child check failed");

            Assert.Pass();

        }

        [Test]
        public void GetAssementsTest() {
            if (!Repository.GetActiveAssessments(1).ToList().Any())
                Assert.Fail("Where are Assessment Types");

            Assert.Pass("Good");
        }
    }
}
