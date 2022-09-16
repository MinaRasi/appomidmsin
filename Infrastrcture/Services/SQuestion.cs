public class SQuestion : IQuestion
{
    private readonly Context db;
    public SQuestion(Context _db)
    {
        db = _db;
    }
    public VmMainQuestion ShowQuestion(string userid)
    {
         int idm=GetFinalQuestion(userid);

        var q = db.MainQuestions.Where(a => a.QuestionNumber == idm).FirstOrDefault();
         int countanser=0;
       
        if (db.answers.Any(x=>x.UserId==Convert.ToInt32(userid)))
        {
           



                     countanser=db.answers.Where(x=>x.UserId==Convert.ToInt32(userid) && x.Date.Date==DateTime.Now.Date).Count();

        }
        


        VmMainQuestion question = new VmMainQuestion()
        {
            Questinon = q.Questinon,
            Id = q.Id,
            QuestionNumber = countanser+1,
            Answer1 = q.Answer1,
            Answer2 = q.Answer2,
            Answer3 = q.Answer3,
            Answer4 = q.Answer4,
            CorrectAnswer = q.CorrectAnswer
        };

        return question;
    }

    public VmUser check(string phone, int code)
    {
        var quser = db.Users.Where(x => x.Phone == phone).FirstOrDefault();

        if (quser.Code == code)
        {
            VmUser us = new VmUser()
            {
                Id = quser.Id,
                FirstAndLastName = quser.FirstAndLastName,
                Phone = quser.Phone,
                Email = quser.Email,
                Code = quser.Code,
                Cart = quser.Cart,
                Url = quser.Url,
            };
            return us;
        }
        else
        {
            return null;
        }

    }
    public bool Savecode(string phone, int code)
    {
        var quser = db.Users.Where(x => x.Phone == phone).FirstOrDefault();
        if (quser != null)
        {
            quser.Code = code;
            db.Users.Update(quser);
            db.SaveChanges();
        }
        else
        {
            User u = new User()
            {
                FirstAndLastName = "user",
                Phone = phone,
                Email = null,
                Url = null,
                Code = code,
                Cart = null
            };
            db.Users.Add(u);
            db.SaveChanges();
        }
        return true;
    }

    public bool AddToAnswer(int id, string UserAnswer, string userid)
    {
       
        string ans;
        var qquestion = db.MainQuestions.Where(x => x.QuestionNumber == id).FirstOrDefault();
        if (qquestion.CorrectAnswer == Convert.ToInt32(UserAnswer))
        {
            ans = "Correct";
        }
        else if (UserAnswer == null)
        {
            ans = null;
        }
        else
        {
            ans = "InCorrect";
        }


        Answer answer = new Answer()
        {
            UserId = Convert.ToInt32(userid),
            Date = DateTime.Now,
            QuestionNumber = qquestion.QuestionNumber,
            UserAnswer = UserAnswer,
            status = ans
            
        };

        db.answers.Add(answer);
        db.SaveChanges();
        return true;
    }

    public int Rate(string userId)
    {
        int Correct = db.answers.Where(x => x.UserId == Convert.ToInt32(userId) && x.status == "Correct").Count();
        int InCorrect = db.answers.Where(x => x.UserId == Convert.ToInt32(userId) && x.status == "InCorrect").Count();
        int Total = (Correct * 5) - (InCorrect *(2));
        return Total;
    }

    public int MaxRate()
    {
         var q=db.answers.Where(x=>x.Date.Date==DateTime.Now.Date).ToList();
         var listuser=q.DistinctBy(x=>x.UserId).ToList();
        int max=0;
         if (listuser != null)
         {
            
            foreach (var item in listuser)
            {
                var x=db.answers.Where(x => x.UserId == Convert.ToInt32(item.UserId)).FirstOrDefault();
               
                    int Correct = db.answers.Where(x => x.UserId == Convert.ToInt32(item.UserId) && x.status == "Correct" && x.Date.Date==DateTime.Now.Date).Count();
                    int InCorrect = db.answers.Where(x => x.UserId == Convert.ToInt32(item.UserId) && x.status == "InCorrect" && x.Date.Date==DateTime.Now.Date).Count();
                    int Total = (Correct * 5) - (InCorrect *(2));
                    if (Total > max)
                    {
                        max=Total;
                    }
            }
         }
      
        return max;
    }
    public int GetFinalQuestion(string userId)
    {

        Random n = new Random();
        int lastid=db.MainQuestions.Count();
        int random;
        
        do
        {
             random=n.Next(1,lastid);
        } while (db.answers.Any(x=>x.QuestionNumber==random && x.UserId==Convert.ToInt32(userId)));
        var question=db.MainQuestions.Where(x=>x.QuestionNumber==random).SingleOrDefault();

        return question.QuestionNumber;
    }

    public List<Vmresult> ShowResult()
    {
        List<Vmresult> ListRate=new List<Vmresult>();

      if (DateTime.Now.Hour > 20 && DateTime.Now.Hour<24)
      {
          var q=db.answers.Where(x=>x.Date.Date==DateTime.Now.Date).ToList();
          var listuser=q.DistinctBy(x=>x.UserId).ToList();
          foreach (var item in listuser)
          {
             var phone=db.Users.Where(x=>x.Id==item.UserId).FirstOrDefault();
             Vmresult r=new Vmresult()
             {

                Phone=phone.Phone,
                Rate=CalcuteRate(item.UserId.ToString(),DateTime.Now.Date)

             };
             ListRate.Add(r);
          }

              return ListRate;

        
      }else
      {

         var q=db.answers.Where(x=>x.Date.Date==DateTime.Now.Date.AddDays(-1)).ToList();
          var listuser=q.DistinctBy(x=>x.UserId).ToList();
          foreach (var item in listuser)
          {
             var phone=db.Users.Where(x=>x.Id==item.UserId).FirstOrDefault();
             Vmresult r=new Vmresult()
             {

                Phone=phone.Phone,
                Rate=CalcuteRate(item.UserId.ToString(),DateTime.Now.Date.AddDays(-1))

             };
             ListRate.Add(r);
          }

              return ListRate;
        
      }
     
    }

    

    public int CalcuteRate(string userid,DateTime d)
    {
       var x=db.answers.Where(x => x.UserId == Convert.ToInt32(userid)).FirstOrDefault();
             
                    int Correct = db.answers.Where(x => x.UserId == Convert.ToInt32(userid) && x.status == "Correct" && x.Date.Date==d).Count();
                    int InCorrect = db.answers.Where(x => x.UserId == Convert.ToInt32(userid) && x.status == "InCorrect" && x.Date.Date==d).Count();
                    int Total = (Correct * 5) - (InCorrect *(2));
      return Total;
    }




}