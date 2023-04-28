namespace UniversityCompetition.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UniversityCompetition.Core.Contracts;
    using UniversityCompetition.Models;
    using UniversityCompetition.Models.Contracts;
    using UniversityCompetition.Repositories;
    using UniversityCompetition.Utilities.Messages;

    public class Controller : IController
    {
        private readonly SubjectRepository subjects;
        private readonly StudentRepository students;
        private readonly UniversityRepository university;
        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            university = new UniversityRepository();
        }
        public string AddStudent(string firstName, string lastName)
        {
            var output = string.Empty;
            var name = $"{firstName} {lastName}";
            if (students.FindByName(name) != null)
                output = string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            else
            {
                IStudent student = new Student(this.students.Models.Count + 1, firstName, lastName);
                this.students.AddModel(student);
                output = string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, nameof(StudentRepository));
            }
            return output.TrimEnd();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            string output = string.Empty;
            if (subjectType != nameof(TechnicalSubject) && subjectType != nameof(EconomicalSubject) && subjectType != nameof(HumanitySubject))
                output = string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            else if (subjects.FindByName(subjectName) != null)
                output = string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            else
            {
                ISubject subject;
                int subjectId = subjects.Models.Count + 1;
                if (subjectType == nameof(TechnicalSubject))
                    subject = new TechnicalSubject(subjectId, subjectName);
                else if (subjectType == nameof(EconomicalSubject))
                    subject = new EconomicalSubject(subjectId, subjectName);
                else
                    subject = new HumanitySubject(subjectId, subjectName);
                this.subjects.AddModel(subject);
                output = string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, nameof(SubjectRepository));
            }
            return output.TrimEnd();
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            string output = string.Empty;
            if (university.FindByName(universityName) != null)
                output = string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            else
            {
                var catchUniversity = new List<int>();
                foreach (var subName in requiredSubjects)
                    catchUniversity.Add(this.subjects.FindByName(subName).Id);
                IUniversity university = new University(this.university.Models.Count + 1, universityName, category, capacity, catchUniversity);
                this.university.AddModel(university);
                output = string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, nameof(UniversityRepository));
            }
            return output.TrimEnd();
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            var outputMessage = string.Empty;
            string firstName = studentName.Split(" ")[0];
            string lastName = studentName.Split(" ")[1];
            var student = this.students.FindByName(studentName);
            var university = this.university.FindByName(universityName);
            if (student == null)
                outputMessage = string.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
            else if (university == null)
                outputMessage = string.Format(OutputMessages.UniversityNotRegitered, universityName);
            else if (!university.RequiredSubjects.All(x => student.CoveredExams.Any(e => e == x)))
                outputMessage = string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            else if (student.University != null && student.University.Name == universityName)
                outputMessage = string.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
            else
            {
                student.JoinUniversity(university);
                outputMessage = string.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);
            }
            return outputMessage.TrimEnd();
        }
        public string TakeExam(int studentId, int subjectId)
        {
            var outputMessage = string.Empty;
            if (this.students.FindById(studentId) == null)
                outputMessage = string.Format(OutputMessages.InvalidSubjectId);
            else if (this.subjects.FindById(subjectId) == null)
                outputMessage = string.Format(OutputMessages.InvalidStudentId);
            else if (this.students.FindById(studentId).CoveredExams.Any(x => x == subjectId))
                outputMessage = string.Format(OutputMessages.StudentAlreadyCoveredThatExam, this.students.FindById(studentId).FirstName, this.students.FindById(studentId).LastName, this.subjects.FindById(subjectId).Name);
            else
            {
                var student = this.students.FindById(studentId);
                var subject = this.subjects.FindById(subjectId);
                student.CoverExam(subject);
                outputMessage = string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
            }
            return outputMessage.TrimEnd();
        }
        public string UniversityReport(int universityId)
        {
            var sb = new StringBuilder();
            var university = this.university.FindById(universityId);
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {this.students.Models.Where(s => s.University == university).Count()}");
            sb.AppendLine($"University vacancy: {university.Capacity - this.students.Models.Where(s => s.University == university).Count()}");
            return sb.ToString().TrimEnd();
        }
    }
}
