using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = true;
    opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly;
    //opt.SetExternalDbConnString = "Server=localhost;Database=testDb;User Id=postgres;Password=root;";
    //opt.DbDriverOption = WatchDogSqlDriverEnum.PostgreSql;
});
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddWatchDogLogger();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "admin";

    //Optional
    //opt.Blacklist = "Test/testPost, api/auth/login"; //Prevent logging for specified endpoints
    //opt.Serializer = WatchDogSerializerEnum.Newtonsoft; //If your project use a global json converter
    //opt.CorsPolicy = "MyCorsPolicy";
});

app.MapControllers();

app.Run();
