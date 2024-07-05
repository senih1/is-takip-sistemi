namespace is_takip_uygulamasi.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Status{ get; set; }
        public DateTime CreateTime { get; set; }
    }

}
