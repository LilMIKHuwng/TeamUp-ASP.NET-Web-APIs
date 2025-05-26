using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using TeamUp.API;
using TeamUp.Core.Utils;

var builder = WebApplication.CreateBuilder(args);

// config appsettings by env
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddConfigJWT(builder.Configuration);
builder.Services.AddCorsPolicyBackend();
builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
    });

//builder.Services.AddHostedService<RoomPlayerWorker>();
//builder.Services.AddHostedService<CoachWorker>();
builder.Services.AddHostedService<CoachBookingWorker>();
//builder.Services.AddHostedService<OwnerWorker>();
//builder.Services.AddHostedService<CourtBookingWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (builder.Environment.IsProduction() && builder.Configuration.GetValue<int?>("PORT") is not null)
{
    builder.WebHost.UseUrls($"http://*:{builder.Configuration.GetValue<int>("PORT")}");
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");


app.UseAuthorization();

app.MapControllers();

app.Run();
