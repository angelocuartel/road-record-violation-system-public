# Road Record Violation System

**Project Description**

Road Record Violation System is a web application where enforcer can now scan driver license to get the details of motorists, and enforce violations via web browser, administrator can also received the records of violators including with their license informations and manage their records accordingly

**Project structure**
>- Repository pattern
>- Dependency injection
>-  monolithic


**Backend framework used**
>- C-sharp (C#)
>- Asp.net Core  MVC and Web api

**Front-end language and framework used**
>- Html 5
>- Css3
>- Bootstrap 5
>- Jquery

**Database and framework used**
>- Mssql server 2019
>- Entity framework core (object relation mapping)

**Tools**
>- Adobe xd
>- postman 
>- just color picker
>- visual studio 2019
>- visual studio code

**Version control**
>- Git bash cli
>- Github
>- Vs Github

## Project Setup

**Database Migration**

1. Open the <mark>appsettings.json</mark> and place your  server with your own local or remote server,
(if you are gonna use a remote make sure to provide username and password for that server)
```json
  "ConnectionStrings": {
    "RoadRecordViolationConnection": "server=Your server here; database=RoadRecordViolationDatabase; trusted_connection = true; MultipleActiveResultSets=True;"
  }
```
2. Open package Manager console and type update-database to seed our database with our latest migration in <mark> Migrations Folder</mark>

```
PM> update-database
```
**Scanner API Setup**

In this project we used the IdAnalyzer CoreApi to scan the driver license by analyzing its image. in order to get your api key you can register at https://www.idanalyzer.com

Once you have your own key open the <mark>EnforcerController.cs</mark> under the <mark>Controllers</mark> -> <mark>api</mark> folder and place the key here at line 261
```c#
     public async Task<IActionResult> SendImage(string imageBase64)
        {
            var coreApi = new CoreAPI("YOUR API KEY HERE", "US");
            coreApi.EnableAuthentication(true, "2");
            coreApi.VerifyExpiry(true);

            try
            {       
               var result = await coreApi.Scan(imageBase64);
                return Ok(result);
            }
            catch(APIException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
```
**Mail and SMS notification**

In this project we used gmail and sms notification to updates violators regarding to their violations. <b>FluentEmail and itextmo</b> are used to provide mail and sms sending.

**Gmail setup**

To use gmail notification you need to provide your own gmail account in order to send notifications. first open <mark>Startup.cs</mark> and go to line 44, place your username and password in username and password variables.

```c#
  var username = "USERNAME HERE";
  var password = "PASSWORD HERE";

  services.AddFluentEmail("")
          .AddRazorRenderer()
          .AddSmtpSender(new System.Net.Mail.SmtpClient("smtp.gmail.com")
          {
              UseDefaultCredentials = false,
              Port = 587,
              EnableSsl = true,
              Credentials = new NetworkCredential(username,password)
          });
``` 
>**Take Note**: Google has announced that it's disabling the Less Secure Apps feature on some Google accounts from May 30th, 2022. according to google
>please visit https://stackoverflow.com/questions/72547853/unable-to-send-email-in-c-sharp-less-secure-app-access-not-longer-available to know on how to setup gmail password

**SMS setup**

To use SMS notification please visit https://itexmo.com to subscribe their sms sending service. if you have your itextmo account go to <mark>Utils</mark> => <mark>Implementation</mark> => <mark>ItextmoUtil.cs</mark> and place your credentials starting at line 22 to 23
```c#
  public static void SendMessage<T>(string number,MessageTemplate<T> messageTemplate)
        {
            object functionReturnValue = null;

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = "https://www.itexmo.com/php_api/api.php";
                parameter.Add("1", number);
                parameter.Add("2", messageTemplate.GetMessage());
                parameter.Add("3", "YOUR API KEY HERE");
                parameter.Add("passwd", "YOUR PASSWORD HERE");
                dynamic rpb = client.UploadValues(url, "POST", parameter);
                functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
            }

        }
```
