namespace Entities.Exceptions
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(string name) : base($"The role with name : {name} could not found")
        {
        }
    }
}
