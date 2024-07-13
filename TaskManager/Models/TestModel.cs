using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class NewTask
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string AttachmentPath { get; set; }
        public List<TaskAssignment>? TaskAssignments { get; set; }
    }

    public class TaskAssignment
    {
        public int TaskAssignmentId { get; set; }
        public int TaskId { get; set; }
        public int? BatchId { get; set; }
        public Student? Student { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int BatchId { get; set; }
    }

    public class Batch
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        
        //public List<Student>? Students { get; set; }
    }

    public class BatchResponseDto
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public List<StudentResponseDto> Students { get; set; }
    }

    public class StudentResponseDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int BatchId { get; set; }
    }

    public class NewTaskDto
    {
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string AttachmentPath { get; set; }
        public List<TaskAssignmentDto>? TaskAssignmentsDto { get; set; }
    }

    public class TaskAssignmentDto
    {
        public int TaskAssignmentId { get; set; }
        public int TaskId { get; set; }
        //public NewTask? Task { get; set; }
        public int? BatchId { get; set; }
        //public Batch? Batch { get; set; }
        public int? StudentId { get; set; }
        public List<Student>? Students { get; set; }
    }
}
