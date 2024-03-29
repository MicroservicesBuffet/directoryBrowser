﻿
namespace DirBrowserBL;

public class JsonFormatter : IDirectoryFormatter
{
    private const string TextHtmlUtf8 = "text/html; charset=utf-8";
    private readonly string root;
    private readonly string name;
    private HtmlEncoder _htmlEncoder;

    public JsonFormatter(string root, string name)
    {
        _htmlEncoder = HtmlEncoder.Default;
        this.root = root;
        this.name = name;
    }
    public virtual Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (contents == null)
        {
            throw new ArgumentNullException(nameof(contents));
        }
        contents = contents.OrderBy(f => f.LastModified);
        context.Response.ContentType = TextHtmlUtf8;

        if (HttpMethods.IsHead(context.Request.Method))
        {
            // HEAD, no response body
            return Task.CompletedTask;
        }

        var result = contents.Select(x => new
        {
            Name = x.Name,
            PhysicalPath = $"{context.Request.Path}{x.Name}",
            x.IsDirectory,
            x.LastModified
        });

        var jsonString = System.Text.Json.JsonSerializer.Serialize(result);
        context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
        return context.Response.WriteAsync(jsonString, Encoding.UTF8);
    }
}
/// <summary>
///
/// </summary>
public class PluginFormatterRoot : IDirectoryFormatter
{
    private const string TextHtmlUtf8 = "text/html; charset=utf-8";
    private readonly string root;
    private readonly string name;
    private HtmlEncoder _htmlEncoder;

    public PluginFormatterRoot(string root,string name)
    {
        _htmlEncoder = HtmlEncoder.Default;
        this.root = root;
        this.name = name;
    }

    /// <summary>
    /// Generates an HTML view for a directory.
    /// </summary>
    public virtual Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (contents == null)
        {
            throw new ArgumentNullException(nameof(contents));
        }
        contents = contents.OrderBy(f => f.LastModified);
        context.Response.ContentType = TextHtmlUtf8;

        if (HttpMethods.IsHead(context.Request.Method))
        {
            // HEAD, no response body
            return Task.CompletedTask;
        }

        PathString requestPath = context.Request.PathBase + context.Request.Path;

        var builder = new StringBuilder();

        builder.AppendFormat(
@"<!DOCTYPE html>
<html lang=""{0}"">", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

        builder.AppendFormat(@"
<head>
  <title>{0} {1}</title>", HtmlEncode("IndexOf"), HtmlEncode(requestPath.Value));

        builder.Append(@"
  <style>
    body {
        font-family: ""Segoe UI"", ""Segoe WP"", ""Helvetica Neue"", 'RobotoRegular', sans-serif;
        font-size: 14px;}
    header h1 {
        font-family: ""Segoe UI Light"", ""Helvetica Neue"", 'RobotoLight', ""Segoe UI"", ""Segoe WP"", sans-serif;
        font-size: 28px;
        font-weight: 100;
        margin-top: 5px;
        margin-bottom: 0px;}
    #index {
        border-collapse: separate; 
        border-spacing: 0; 
        margin: 0 0 20px; }
    #index th {
        vertical-align: bottom;
        padding: 10px 5px 5px 5px;
        font-weight: 400;
        color: #a0a0a0;
        text-align: center; }
    #index td { padding: 3px 10px; }
    #index th, #index td {
        border-right: 1px #ddd solid;
        border-bottom: 1px #ddd solid;
        border-left: 1px transparent solid;
        border-top: 1px transparent solid;
        box-sizing: border-box; }
    #index th:last-child, #index td:last-child {
        border-right: 1px transparent solid; }
    #index td.length, td.modified { text-align:right; }
    a { color:#1ba1e2;text-decoration:none; }
    a:hover { color:#13709e;text-decoration:underline; }
  </style>
</head>
<body>
  <section id=""main"">");
        builder.AppendFormat(@"
    <header><h1><a href=""/"">{0}</a>/", HtmlEncode("Home"));

        string cumulativePath = "/";
        foreach (var segment in requestPath.Value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
        {
            cumulativePath = cumulativePath + segment + "/";
            builder.AppendFormat(@"<a href=""{0}"">{1}/</a>",
                HtmlEncode(cumulativePath), HtmlEncode(segment));
        }

        builder.AppendFormat(CultureInfo.CurrentUICulture,
@"</h1></header>
    <table id=""index"" summary=""{0}"">
    <thead>
      <tr><th abbr=""{1}"">{1}</th><th abbr=""{2}"">{2}</th><th abbr=""{3}"">{4}</th></tr>
    </thead>
    <tbody>",
        HtmlEncode("Summary"),
        HtmlEncode("Name"),
        HtmlEncode("Size"),
        HtmlEncode("Modified"),
        HtmlEncode("LastModified"));
        
        foreach (var subdir in contents.Where(info => info.IsDirectory))
        {
            if (subdir == null || subdir.PhysicalPath == null)
                continue;
            string pathFileRel = subdir.PhysicalPath.Substring(root.Length+1);
            pathFileRel = pathFileRel.Replace(@"\", "/");
            if (!pathFileRel.StartsWith("/"))
                pathFileRel = "/" + pathFileRel;

            pathFileRel = $"/{name}{pathFileRel}";

            builder.AppendFormat(@"
      <tr class=""directory"">
        <td class=""name""><a href=""{0}/"">{1}/</a></td>
        <td><!--<a href=""javascript:window.alert('not implemented')"">Size!</a>--></td>
        <td class=""modified"">{2}</td>
      </tr>",
                HtmlEncode(pathFileRel),
                HtmlEncode(subdir.Name),
                HtmlEncode(subdir.LastModified.ToString(CultureInfo.CurrentCulture)));
        }

        foreach (var file in contents.Where(info => !info.IsDirectory))
        {
            if (file == null || file.PhysicalPath == null)
                continue;
            string pathFileRel = file.PhysicalPath.Substring(root.Length);
            pathFileRel = pathFileRel.Replace(@"\", "/");
            pathFileRel = $"/{name}{pathFileRel}";
            builder.AppendFormat(@"
      <tr class=""file"">
        <td class=""name""><a href=""{0}"">{1}</a></td>
        <td class=""length"">{2}</td>
        <td class=""modified"">{3}</td>
      </tr>",
                HtmlEncode(pathFileRel),
                HtmlEncode(file.Name),
                HtmlEncode(file.Length.ToString("n0", CultureInfo.CurrentCulture)),
                HtmlEncode(file.LastModified.ToString(CultureInfo.CurrentCulture)));
        }

        builder.Append(@"
    </tbody>
    </table>
  </section>
</body>
</html>");
        string data = builder.ToString();
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        context.Response.ContentLength = bytes.Length;
        return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
    }

    private string HtmlEncode(string body)
    {
        return _htmlEncoder.Encode(body);
    }
}