public interface IQuestion
{
    VmMainQuestion ShowQuestion(string userid);
    VmUser check(string phone, int code);
    bool Savecode(string phone, int code);
    bool AddToAnswer(int id, string UserAnswer, string userid);
    int Rate(string userId);
    int GetFinalQuestion(string userId);
    int MaxRate();
    List<Vmresult>  ShowResult();


}