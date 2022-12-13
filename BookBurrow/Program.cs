using BookBurrow.Repositories;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var BookBurrowApp = "_bookBurrowApp";
var builder = WebApplication.CreateBuilder(args);
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(builder.Configuration.GetValue<string>("GoogleCredentialPath"))
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: BookBurrowApp,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration.GetValue<string>("BackendPort"),
                                              builder.Configuration.GetValue<string>("FrontendPort"))
                                .AllowAnyHeader()
                                .WithMethods("GET", "POST", "PUT", "DELETE")
                                .WithExposedHeaders("*");
                      });
});

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
builder.Services.AddTransient<IUserProfileViewModelRepository, UserProfileViewModelRepository>();
builder.Services.AddTransient<IUserPronounRepository, UserPronounRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();

var firebaseProjectId = builder.Configuration.GetValue<string>("firebaseProjectId");
var googleTokenUrl = $"https://securetoken.google.com/{firebaseProjectId}";
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = googleTokenUrl;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = googleTokenUrl,
            ValidateAudience = true,
            ValidAudience = firebaseProjectId,
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(BookBurrowApp);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
