using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using PetHotelApplicationRazorPage;
using Repositories.Repositories.AccommodationRepo;
using Repositories.Repositories.BookingInformationRepo;
using Repositories.Repositories.FeedbackRepo;
using Repositories.Repositories.PaymentRecordRepo;
using Repositories.Repositories.PetCareServiceRepo;
using Repositories.Repositories.PetRepo;
using Repositories.Repositories.RoleRepo;
using Repositories.Repositories.ServiceBookingRepo;
using Repositories.Repositories.ServiceImageRepo;
using Repositories.Repositories.UserRepo;
using Services.MapperProfiles;
using Services.Services.AccommodationService;
using Services.Services.BookingInformationService;
using Services.Services.CloudinaryService;
using Services.Services.DashboardService;
using Services.Services.FeedbackService;
using Services.Services.PaymentRecordService;
using Services.Services.PetCareServices;
using Services.Services.PetService;
using Services.Services.RoleService;
using Services.Services.ServiceImageService;
using Services.Services.ServicesBookingService;
using Services.Services.UserService;
using Services.Services.VnPaymentServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add session
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
});

//builder.Services.AddControllersWithViews();
//--------------------------------------------------------------------------------------------------------------
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddHttpClient();

builder.Services.AddScoped<IAccommodationRepository, AccommodationRepository>();
builder.Services.AddScoped<IBookingInformationRepository, BookingInformationRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IPaymentRecordRepository, PaymentRecordRepository>();
builder.Services.AddScoped<IPetCareServiceRepository, PetCareServiceRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IServiceBookingRepository, ServiceBookingRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IServiceImageRepository, ServiceImageRepository>();
//--------------------------------------------------------------------------------------------------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccommodationService, AccommodationService>();
builder.Services.AddScoped<IBookingInformationService, BookingInformatonService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IPaymentRecordService, PaymenRecordService>();
builder.Services.AddScoped<IPetCareService, PetCareServices>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IServiceBookingService, ServiceBookingService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IServiceImageService, ServiceImageService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

//builder.Services.AddDbContext<PetHotelApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PetHotelApplication"));
//});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
