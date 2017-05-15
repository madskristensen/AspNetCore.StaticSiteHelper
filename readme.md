# ASP.NET Core Static Site Helper

[![Build status](https://ci.appveyor.com/api/projects/status/sm6gqiopehjf1g1n?svg=true)](https://ci.appveyor.com/project/madskristensen/aspnetcore-staticsitehelper)

Used by the static web template

## MimeType mappings
All standard mime types defined [here by ASP.NET Core](https://github.com/aspnet/StaticFiles/blob/dev/src/Microsoft.AspNetCore.StaticFiles/FileExtensionContentTypeProvider.cs) is enabled, but you can modify the supported file extensions/mime types.

By dropping a JSON file in the root of the project (not `wwwroot`) called `mimetypes.json` you can control the behavior. Here's an example:

```json
{
  ".foo": "text/plain",
  ".vsix": "application/octet-stream"
}
```

You can use this file to modify extension mappings or create new ones. A rebuild of the project might be required for any changes to take effect after modifying the file.