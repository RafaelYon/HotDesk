using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class GroupPermission
    {
        public int GroupId { get; set; }

        public int PermissionId { get; set; }

        public Group Group { get; set; }

        public Permission Permission { get; set; }
    }
}
