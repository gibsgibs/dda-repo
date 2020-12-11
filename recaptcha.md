## How to validate a form with ReCaptcha
---
1. In the form you want to validate, add the following `div` to the end of the `form` element:
    
    ```html
    <div class="g-recaptcha" data-sitekey="Your-Site-Key"></div>
    ```
    Obviously, make sure "Your-Site-Key" is replaced with your actual site key provided by Google.

1. Once you have done that, you'll need to add the Google script to generate the ReCaptcha checkbox, so add the `script` to the scripts section at the bottom of the file like so:

    ```html
    @section Scripts {
        <script src='https://www.google.com/recaptcha/api.js'></script>
    }
    ```
     
