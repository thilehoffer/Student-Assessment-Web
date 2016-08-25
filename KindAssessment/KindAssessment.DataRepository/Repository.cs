using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace AssessmentApp.Data
{
    public class Repository : IRepository
    {
        private static class StoredProcedures
        {
            internal static readonly string CreateTeacher = "dbo.usp_CreateTeacher";
            internal static readonly string GetTeacher = "dbo.usp_GetTeacher";
            internal static readonly string GetTeacherClass = "dbo.usp_GetTeacherClass";
            internal static readonly string GetTeacherClasses = "dbo.usp_GetTeacherClasses";
            internal static readonly string CreateTeacherClass = "dbo.usp_CreateTeacherClass";
            internal static readonly string UpdateTeacherClass = "dbo.usp_UpdateTeacherClass";
            internal static readonly string CheckIfTeacherClassExists = "dbo.usp_CheckIfTeacherClassExists";
            internal static readonly string GetStudentsInClass = "dbo.usp_GetStudentsInClass";
            internal static readonly string CreateStudent = "dbo.usp_CreateStudent";
            internal static readonly string GetStudent = "dbo.usp_GetStudent";
            internal static readonly string UpdateStudent = "dbo.usp_UpdateStudent";
            internal static readonly string StudentIsTeachers = "dbo.usp_StudentIsTeachers";
            internal static readonly string GetAssessmentTypes = "dbo.usp_GetAssessmentTypes";
            internal static readonly string GetActiveClasses = "dbo.usp_GetActiveClasses";
            internal static readonly string GetActiveStudentList = "dbo.usp_GetActiveStudentList";
            internal static readonly string GetActiveAssessments = "dbo.usp_GetActiveAssessments";
            internal static readonly string CreateStudentAssessment = "dbo.usp_CreateStudentAssessment";
            internal static readonly string GetStudentAssessment = "dbo.usp_GetStudentAssessment";
            internal static readonly string SaveAssessmentData = "dbo.usp_SaveAssessmentData";
            internal static readonly string GetAssessmentData = "dbo.usp_GetAssessmentData";
            internal static readonly string GetRecentAssessments = "dbo.usp_GetRecentAssessments";
        }

        private readonly string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

       

         Models.Teacher IRepository.CreateTeacher(Models.Teacher model)
        {
            var parameters = new[] { new SqlParameter("@UserId", model.UserId) };
            using (var db = new Db(ConnectionString))
            {
                model.Id = db.CallProcWithReturnValue(StoredProcedures.CreateTeacher, parameters);
            }
            return model;
        }
         Models.Teacher IRepository.GetTeacher(string userName)
        {
            var result = new Models.Teacher();
            var parameters = new[] { new SqlParameter("@UserName", userName) };
            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(StoredProcedures.GetTeacher, parameters, reader =>
                {
                    while (reader.Read())
                    {
                        result.Id = reader.GetInt("Id");
                        result.UserId = reader.GetString("UserId");
                    }
                });
            }

            return result;
        }

        List<Models.Class> IRepository.GetTeachersClasses(int teacherId)
        {
            var result = new List<Models.Class>();
            var parameters = new[] { new SqlParameter("@TeacherId", teacherId) };
            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(StoredProcedures.GetTeacherClasses, parameters, reader =>
                {
                    while (reader.Read())
                    {
                        var model = new Models.Class();
                        model.Id = reader.GetInt("Id");
                        model.Active = reader.GetBool("Active");
                        model.ClassName = reader.GetString("ClassName");
                        model.TeacherId = reader.GetInt("TeacherId");
                        result.Add(model);
                    }
                });
                return result;
            }

        }
         Models.Class IRepository.CreateClass(Models.Class model)
        {
            var parameters = new[] {
                new SqlParameter("@TeacherId" , model.TeacherId) ,
                new SqlParameter("@ClassName", model.ClassName ) ,
                new SqlParameter("@Active", true) //New is always active
            };

            using (var db = new Db(ConnectionString))
            {
                model.Id = db.CallProcWithReturnValue(StoredProcedures.CreateTeacherClass, parameters);
            }
            return model;
        }
         Models.Class IRepository.GetClass(int id)
        {
            var result = new Models.Class();
            var parameters = new[] { new SqlParameter("@Id", id) };
            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(StoredProcedures.GetTeacherClass, parameters, reader =>
                {
                    while (reader.Read())
                    {
                        result.Id = reader.GetInt("Id");
                        result.Active = reader.GetBool("Active");
                        result.ClassName = reader.GetString("ClassName");
                        result.TeacherId = reader.GetInt("TeacherId");
                    }
                });
            }
            return result;
        }
         void IRepository.UpdateClass(Models.Class model)
        {
            var parameters = new[] {
                new SqlParameter("@Id", model.Id) ,
                new SqlParameter("@TeacherId" , model.TeacherId) ,
                new SqlParameter("@ClassName", model.ClassName ) ,
                new SqlParameter("@Active", model.Active)
            };
            using (var db = new Db(ConnectionString))
            {
                db.CallProc(StoredProcedures.UpdateTeacherClass, parameters);
            }
        }
         bool IRepository.CheckIfClassAlreadyExists(int teacherId, string className)
        {

            var parameters = new[] {
                new SqlParameter("@TeacherId" , teacherId) ,
                new SqlParameter("@ClassName", className )
            };
            using (var db = new Db(ConnectionString))
            {
                return int.Equals(db.CallProcWithReturnValue(StoredProcedures.CheckIfTeacherClassExists, parameters), 1);
            }
        }

         List<Models.Student> IRepository.GetStudents(int classId)
        {
            var result = new List<Models.Student>();
            var parameters = new[] { new SqlParameter("@ClassId", classId) };
            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(StoredProcedures.GetStudentsInClass, parameters, reader =>
                {
                    while (reader.Read())
                    {
                        var model = new Models.Student();
                        model.Id = reader.GetInt("Id");
                        model.ClassId = reader.GetInt("TeacherClassId");
                        model.ClassName = reader.GetString("ClassName");
                        model.StudentDateOfBirth = reader.GetDateTime("StudentDateOfBirth");
                        model.StudentName = reader.GetString("StudentName");
                        model.Active = reader.GetBool("Active");
                        result.Add(model);
                    }
                });
            }
            return result;
        }
         Models.Student IRepository.CreateStudent(Models.Student model)
        {
            var parameters = new[] {
                new SqlParameter("@TeacherClassId", model.ClassId),
                new SqlParameter("@StudentName", model.StudentName),
                new SqlParameter("@StudentDateOfBirth", model.StudentDateOfBirth),
                new SqlParameter("@Active", 1) //Active when created
            };
            using (var db = new Db(ConnectionString))
            {
                model.Id = db.CallProcWithReturnValue(StoredProcedures.CreateStudent, parameters);
            }
            return model;
        }
         Models.Student IRepository.GetStudent(int id)
        {
            var result = new Models.Student();
            var parameters = new[] { new SqlParameter("@Id", id) };
            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(StoredProcedures.GetStudent, parameters, reader =>
                {
                    while (reader.Read())
                    {
                        result.Id = reader.GetInt("Id");
                        result.ClassId = reader.GetInt("TeacherClassId");
                        result.ClassName = reader.GetString("ClassName");
                        result.StudentDateOfBirth = reader.GetDateTime("StudentDateOfBirth");
                        result.StudentName = reader.GetString("StudentName");
                        result.Active = reader.GetBool("Active");
                    }
                });
            }
            return result;
        }
         void IRepository.UpdateStudent(Models.Student model)
        {
            var parameters = new[] {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@TeacherClassId", model.ClassId),
                new SqlParameter("@StudentName", model.StudentName),
                new SqlParameter("@StudentDateOfBirth", model.StudentDateOfBirth),
                new SqlParameter("@Active", model.Active)
            };
            using (var db = new Db(ConnectionString))
            {
                db.CallProc(StoredProcedures.UpdateStudent, parameters);
            }
        }
         
         Models.StudentAssessment IRepository.CreateStudentAssessment(Models.StudentAssessment model)
        {
            var parameters = new[] { new SqlParameter("@StudentId", model.StudentId.Value), new SqlParameter("@AssessmentTypeId", model.AssessmentTypeId.Value) };
            using (var db = new Db(ConnectionString))
            {
                model.Id = db.CallProcWithReturnValue(StoredProcedures.CreateStudentAssessment, parameters);
            }
            return model;
        }
         Models.StudentAssessment IRepository.GetStudentAssessment(int id)
        {
            var result = new Models.StudentAssessment { Id = id };
            var parameters = new[] { new SqlParameter("@Id", id) };
            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(StoredProcedures.GetStudentAssessment, parameters, reader =>
                {
                    while (reader.Read())
                    { 
                        result.StudentId = reader.GetInt("StudentId");
                        result.AssessmentTypeId = reader.GetInt("AssessmentTypeId");
                        result.StudentName = reader.GetString("StudentName");
                        result.AssessmentTypeName = reader.GetString("AssessmentTypeName"); 
                    }
                });
            }
            return result;
        }

         List<Models.RecentAssessmentGridItem> IRepository.GetRecentAssessments(int teacherId) {
            var result = new List<Models.RecentAssessmentGridItem>();
            var parameters = new[] { new SqlParameter("@TeacherId", teacherId) };
            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(StoredProcedures.GetRecentAssessments, parameters, reader =>
                {
                    while (reader.Read())
                    {
                        result.Add(new Models.RecentAssessmentGridItem {Id = reader.GetInt("Id"), Updated = reader.GetDateTime("Updated"),  StudentName = reader.GetString("StudentName"), Assessment = reader.GetString("Assessment") });
                    }
                });
            }
            return result;
        }

         bool IRepository.IsChild<T>(int teacherId, T model)
        {

            if (typeof(T) == typeof(Models.Class))
            {
                return (int.Equals(teacherId, (model as Models.Class).TeacherId));
            }

            if (typeof(T) == typeof(Models.Student))
            {
                var studentId = (model as Models.Student).Id;
                var parameters = new[] {
                    new SqlParameter("@TeacherId",teacherId),
                    new SqlParameter("@StudentId", studentId)
                };

                using (var db = new Db(ConnectionString))
                {
                    return int.Equals(db.CallProcWithReturnValue(StoredProcedures.StudentIsTeachers, parameters), 1);
                }
            }


            throw new ArgumentException("There is no check for the type provided");

        }


        private List<Models.ListItem> GetListItems(string procName, SqlParameter[] parameters)
        {
            var result = new List<Models.ListItem>();

            using (var db = new Db(ConnectionString))
            {
                db.UseDataReader(procName, parameters, reader =>
                {
                    while (reader.Read())
                    {
                        result.Add(new Models.ListItem { Id = (int)reader[0], Text = (string)reader[1] });
                    }
                });
            }

            return result;

        }
         List<Models.ListItem> IRepository.GetActiveStudents(int teacherId)
        {
            var parameters = new[] { new SqlParameter("@TeacherId", teacherId) };
            return GetListItems(StoredProcedures.GetActiveStudentList, parameters);
        }
         List<Models.ListItem> IRepository.GetActiveClasses(int teacherId)
        {
            var parameters = new[] { new SqlParameter("@TeacherId", teacherId) };
            return GetListItems(StoredProcedures.GetActiveClasses, parameters);
        }
         List<Models.ListItem> IRepository.GetActiveAssessments(int teacherId)
        {
            var parameters = new[] { new SqlParameter("@TeacherId", teacherId) };
            return GetListItems(StoredProcedures.GetActiveAssessments, parameters);
        }



         T IRepository.GetAssmentData<T>(int id) {
            var parameters = new[] { new SqlParameter("@Id", id) };
            using (var db = new Db(ConnectionString))
            {
                var json = db.CallProcWithReturnScalar(StoredProcedures.GetAssessmentData, parameters).ToString();
                if (string.IsNullOrEmpty(json))
                    return default(T);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json); 
            }
        }
         void IRepository.SaveAssessmentData<T>(int id, T t1) {
            var parameters = new[] { new SqlParameter("@Id", id), new SqlParameter("@Data",  Newtonsoft.Json.JsonConvert.SerializeObject(t1))  };
            using (var db = new Db(ConnectionString))
            {
                db.CallProc(StoredProcedures.SaveAssessmentData, parameters); 
            }
        }
    }
}
