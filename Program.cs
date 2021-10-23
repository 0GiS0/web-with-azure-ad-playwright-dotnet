// // See https://aka.ms/new-console-template for more information

using Microsoft.Playwright;

using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

//3. Authenticate with Azure AD
Console.WriteLine("Testing authenticated web page with Azure AD");

var page = await browser.NewPageAsync();
await page.GotoAsync("<WEB_URL>");

//Interact with the login form:

//email
await page.FillAsync("input[type='email']", "<VALID_USER_EMAIL>");
await page.ClickAsync("input[type='submit']");

await page.WaitForNavigationAsync();

//password
await page.ClickAsync("[placeholder='Password']");
await page.FillAsync("input[name='passwd']", "<VALID_USER_PASSWORD>");
await page.ClickAsync("input[type='submit']");


//Stay signed? Say no
await page.ClickAsync("text=No");

await page.WaitForNavigationAsync();

// await page.PauseAsync(); //Just for debugging

var title = await page.TitleAsync();

Console.WriteLine($"Title of the page {title}");