using System.Collections.Generic;

namespace SAS.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int SkillSetId { get; set; }
        public int UserProfileId { get; set; }
        public List<RequestDTO> RequestDTOs { get; set; }
    }
}
