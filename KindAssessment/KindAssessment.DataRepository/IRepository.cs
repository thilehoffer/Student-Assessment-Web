using System.Collections.Generic;
using AssessmentApp;

namespace AssessmentApp.Data
{
    public interface IRepository
    {
        Models.Teacher CreateTeacher(Data.Models.Teacher model);
        Models.Teacher GetTeacher(string userId);

        List<Data.Models.Class> GetTeachersClasses(int teacherId);
        Models.Class GetClass(int id);
        Models.Class CreateClass(Models.Class model);
        void UpdateClass(Models.Class model);

        List<Data.Models.Student> GetStudents(int classId);
        Models.Student CreateStudent(Models.Student model);
        Models.Student GetStudent(int id);
        void UpdateStudent(Models.Student model);


        Models.StudentAssessment CreateStudentAssessment(Models.StudentAssessment model);
        Models.StudentAssessment GetStudentAssessment(int id);
        List<Models.RecentAssessmentGridItem> GetRecentAssessments(int teacherId);

        bool CheckIfClassAlreadyExists(int teacherId, string className);
        bool IsChild<T>(int teacherId, T model);

        List<Models.ListItem> GetActiveAssessments(int teacherId);
        List<Models.ListItem> GetActiveClasses(int teacherId);
        List<Models.ListItem> GetActiveStudents(int teacherId);


        T GetAssmentData<T>(int id);
        void SaveAssessmentData<T>(int id, T t1);

    }
}
