using BookBurrow.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookAuthorRepository, BookAuthorRepository>();
builder.Services.AddTransient<ILoginViewModelRepository, LoginViewModelRepository>();
builder.Services.AddTransient<IPostCommentRepository, PostCommentRepository>();
builder.Services.AddTransient<IPostFavoriteRepository, PostFavoriteRepository>();
builder.Services.AddTransient<IPostLikeRepository, PostLikeRepository>();
builder.Services.AddTransient<IRatingRepository, RatingRepository>();
builder.Services.AddTransient<IRegisterViewModelRepository, RegisterViewModelRepository>();
builder.Services.AddTransient<ISeriesRepository, SeriesRepository>();
builder.Services.AddTransient<ISeriesBookRepository, SeriesBookRepository>();
builder.Services.AddTransient<IUserBookRepository, UserBookRepository>();
builder.Services.AddTransient<IUserFollowerRepository, UserFollowerRepository>();
builder.Services.AddTransient<IUserPostRepository, UserPostRepository>();
builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddTransient<IUserPronounRepository, UserPronounRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
