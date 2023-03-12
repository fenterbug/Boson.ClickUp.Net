# Boson.ClickUp.Net
A simple .Net library for accessing the [ClickUp](https://www.clickup.com/) API.

See the ClickUp API reference [here](https://clickup.com/api/).

---

Basic development workflow:
1. Find an [API call](https://clickup.com/api/) that you want to implement.
2. Copy the response result and translate it into C# classes. ([Here](https://json2csharp.com/)'s a sample tool for that.)
3. Integrate (don't just copy-- use your brain cells) those generated classes into ClickUp Model.
4. Create a method on the ClickUpApi class to mimic the Api request. (Make sure to include required and optional parameters.)
5. Parse the method parameters into an Http request string and make the call. (See GetTaskComments() for an example.)
6. Create a test for the Api call you just implemented.
