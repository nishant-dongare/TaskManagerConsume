namespace TaskManager.Models
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public Batch? Batch { get; set; }

        public List<TaskAssignmentDto>? TaskAssignmentsDto { get; set; }
    }
}
