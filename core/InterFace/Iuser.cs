public interface IUser
{
   bool EditUserProfile(VmUser user); 
   VmUser ShowUser(int id);
   List<VmUser> ShowUserRate(int id);
   
}