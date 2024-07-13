namespace TaskManager.Models.ViewModels
{
    public class AssignTaskViewModel
    {
        public int SelectedBatch { get; set; }
        public List<string> SelectedStudents { get; set; }
        public List<string> Students { get; set; }
        public string TaskDescription { get; set; }
        public IFormFile Attachment { get; set; }
    }

}
