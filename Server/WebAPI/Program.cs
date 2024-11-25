using EfcRepositories;
using FileRepositories;
using RepositoryContracts;
using AppContext = System.AppContext;

var builder = WebApplication.CreateBuilder(args);


// Tilføjer services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repository
builder.Services.AddScoped<IPostRepository, EfcPostRepository>();
builder.Services.AddScoped<IUserRepository, EfcUserRepostory>();
builder.Services.AddScoped<ICommentRepostory, CommentFileRepository>();
builder.Services.AddDbContext<AppDbContext>();
// tilføjer controllersne 
builder.Services.AddControllers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

