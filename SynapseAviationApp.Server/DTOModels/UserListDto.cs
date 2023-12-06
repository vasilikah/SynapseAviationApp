namespace SynapseAviationApp.Server.DTOModels
{
    
    public class UserListDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty; 
        public int Age { get; set; }
        
    }
}
