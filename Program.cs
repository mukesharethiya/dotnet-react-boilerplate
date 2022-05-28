using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions{
    Args=args,
    WebRootPath=Path.Combine("reactapp","build")
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (StreamReader iisUrlRewriteStreamReader = 
        File.OpenText("IISUrlRewrite.xml")) 
    {
        var options = new RewriteOptions()
                    //.AddRewrite("^(?!api)(?<!(.js|.css|.ico)$)$","/",true)
                    .AddIISUrlRewrite(iisUrlRewriteStreamReader);
                    
        app.UseRewriter(options);
    }

app.UseStaticFiles();

app.Run();
