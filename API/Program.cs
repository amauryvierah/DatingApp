using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Calling  Application services method to connect to the database (Cleaner)
builder.Services.AddAplicationServices(builder.Configuration);

//Calling  Identity services method for JWT (Cleaner)
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseHttpsRedirection();


//Allow use cors from angular client
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("https://localhost:4200"));

//Add the authentication middleware before Map Controllers and after UseCores
app.UseAuthentication(); //Do you have a Valid Token?
app.UseAuthorization(); //Ok, do you have a valid token, What are you allowed to do?

app.MapControllers();

app.Run();
