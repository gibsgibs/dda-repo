## How to validate a form with ReCaptcha
---
1. In the form you want to validate, add the following `div` to the end of the `form` element:
    
    ```html
    <div class="g-recaptcha" data-sitekey="Your-Site-Key"></div>
    ```
    Obviously, make sure "Your-Site-Key" is replaced with your actual site key provided by Google.
    
    Note: if you are using the configuration to store and retrieve your site key, you will need to add a using statement and an injection to the view. E.g. 
    ```
    @using Microsoft.Extensions.Configuration
    @inject IConfiguration configuration
    ``` 

1. Once you have done that, you'll need to add the Google script to generate the ReCaptcha checkbox, so add the `script` to the scripts section at the bottom of the file like so:

    ```html
    @section Scripts {
        <script src='https://www.google.com/recaptcha/api.js'></script>
    }
    ```
1. The last thing you'll need to do is add the logic to the POST method to validate the user. It is simply
    ```csharp
    if (!ReCaptcha.IsValid(Request.Form["g-recaptcha-response"]).Result)
    {
        ModelState.AddModelError(string.Empty, "Failed CAPTCHA.");
        return Page();
    }
    ```
    Of course you can specify the model error, or add logging or whatever you want.
### Sources
- https://retifrav.github.io/blog/2017/08/23/dotnet-core-mvc-recaptcha/
