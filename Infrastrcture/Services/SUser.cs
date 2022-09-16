using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

public class SUser : IUser
{
    private readonly Context db;
    private readonly IWebHostEnvironment env;
    public SUser(Context _db, IWebHostEnvironment _env)
    {
       db=_db; 
       env=_env;
    }
        public string GetImageUrl(IFormFile file)
    {
        string FileExtension2 = Path.GetExtension(file.FileName);
        string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension2);
        var path = $"{env.WebRootPath}\\fileupload\\{NewFileName}";
        using (var stream = new FileStream(path, FileMode.Create))
            file.CopyTo(stream);

        return NewFileName;
    }
     public bool EditUserProfile(VmUser user)
    {
      var q= db.Users.Where(x=>x.Phone==user.Phone).FirstOrDefault();
         
          q.FirstAndLastName=user.FirstAndLastName;
          q.Phone=user.Phone;
          if ( user.Urlfirst != null)
          {
            q.Url=GetImageUrl(user.Urlfirst);   
          }
                 
          q.Email=user.Email;
          q.Cart=user.Cart; 
        
        db.Users.Update(q);
        db.SaveChanges();
        
      return true;
    }
    public VmUser ShowUser(int id)
    {
      var q= db.Users.Where(a=>a.Id==id).FirstOrDefault();
      VmUser u = new VmUser()
      {
         
          Cart=q.Cart,
          Url=q.Url,
          FirstAndLastName=q.FirstAndLastName,
          Phone=q.Phone,
          Email=q.Email,
          Code=q.Code,
         
      };
      
      return u;
    }

    public List<VmUser> ShowUserRate(int id)
    {
        throw new NotImplementedException();
    }
}