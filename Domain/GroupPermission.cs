using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class GroupPermission
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public Permission Permission { get; set; }
    }
}
