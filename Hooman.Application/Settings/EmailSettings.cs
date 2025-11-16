namespace Hooman.Application.Settings;

public class EmailSettings
{
  public string SmtpHost {get;set;} = "smtp.gmail.com";
  public int SmtpPort {get;set;} = 587;
  public string SenderEmail {get;set;} = string.Empty;
  public string SenderName {get;set;} = "Hooman Interactive System";
  public string UserName {get;set;} =string.Empty;
  public string Password {get;set;} =string.Empty;
  public bool EnableSsl {get;set;} = true;
  public string BaseUrl {get;set;} = "https://localhost:5001";

}
