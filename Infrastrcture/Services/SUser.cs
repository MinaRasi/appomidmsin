public class SUser : IUser
{
    private readonly Context db;
    public SUser(Context _db)
    {
       db=_db; 
    }
    public bool AddUser(VmUser user)
    {
        User u = new User()
        {
          UserName=user.UserName,
          FirstAndLastName=user.FirstAndLastName,
          Phone=user.Phone,
          Address=user.Address,
          Email=user.Email  
        };
        db.Users.Add(u);
        db.SaveChanges();
        
      return true;
    }

  
}