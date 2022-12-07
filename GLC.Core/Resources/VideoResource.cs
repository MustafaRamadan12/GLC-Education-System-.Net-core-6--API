namespace GLC.Core.Resources
{
    public class VideoResource
    {

        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string MainSection { get; set; }
        public string MainSubject { get; set; }
        public string Link { get; set; }

        public Guid? TeacherId { get; set; }
   


        public Guid? SubjectId { get; set; }

    }
}
