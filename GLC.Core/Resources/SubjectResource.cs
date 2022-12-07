using System.Collections.ObjectModel;

namespace GLC.Core.Resources
{
    public class SubjectResource
    {

        public Guid SubjectId { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public Guid? TeacherId { get; set; }
       
        public ICollection<VideoResource>? videos { get; set; } = new Collection<VideoResource>();
        public ICollection<GroupResource>? Groups { get; set; } = new Collection<GroupResource>();
        public ICollection<QuizResource>? Quizes { get; set; } = new Collection<QuizResource>();

    }
}
