namespace Boson.ClickUp.Net.Model
{
    public class List
    {
        public bool archived { get; set; }
        public object assignee { get; set; }
        public string content { get; set; }
        public string due_date { get; set; }
        public Folder folder { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int orderindex { get; set; }
        public bool override_statuses { get; set; }
        public string permission_level { get; set; }
        public Priority priority { get; set; }
        public Space space { get; set; }
        public object start_date { get; set; }
        public Status status { get; set; }
        public object task_count { get; set; }
    }
}