var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidation();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
   .AddDataAnnotationsLocalization(options =>
   {
      options.DataAnnotationLocalizerProvider = (type, factory) =>
         factory.Create(typeof(AnnotationsDemoApi.Resources.SharedStrings));
   });

var app = builder.Build();

var supportedCultures = new[] { "en-US", "de-DE", "es-ES" };

var localizationOptions = new RequestLocalizationOptions()
   .SetDefaultCulture(supportedCultures[0])
   .AddSupportedCultures(supportedCultures)
   .AddSupportedUICultures(supportedCultures);

localizationOptions.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.

// Luhn Check Digit Endpoints
app.MapPost("/luhn",
   (LuhnPaymentRequest request) => Results.Ok("Credit card number is valid"));

app.MapPost("/luhnmessage",
   (LuhnPaymentRequestCustomErrorMessage request) => Results.Ok("Credit card number is valid"));

app.MapPost("/luhnglobal",
   (LuhnPaymentRequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
   => Results.Ok(localizer["ValidRequest"].ToString()));

// Modulus 10_13 Check Digit Endpoints
app.MapPost("/modulus1013",
   (Modulus10_13ItemDetails request) => Results.Ok("UPC code is valid"));

app.MapPost("/modulus1013message",
   (Modulus10_13ItemDetailsCustomErrorMessage request) => Results.Ok("UPC code is valid"));

app.MapPost("/modulus1013global",
   (Modulus10_13ItemDetailsGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
   => Results.Ok(localizer["ValidRequest"].ToString()));

// Modulus 11 Check Digit Endpoints
app.MapPost("/modulus11",
   (Modulus11Publication request) => Results.Ok("ISBN is valid"));

app.MapPost("/modulus11message",
   (Modulus11PublicationCustomErrorMessage request) => Results.Ok("ISBN is valid"));

app.MapPost("/modulus11global",
   (Modulus11PublicationGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
   => Results.Ok(localizer["ValidRequest"].ToString()));

// Verhoeff Check Digit Endpoints
app.MapPost("/verhoeff",
   (VerhoeffRequest request) => Results.Ok("Aadhaar ID number is valid"));

app.MapPost("/verhoeffmessage",
   (VerhoeffRequestCustomErrorMessage request) => Results.Ok("Aadhaar ID number is valid"));

app.MapPost("/verhoeffglobal",
   (VerhoeffRequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer) 
   => Results.Ok(localizer["ValidRequest"].ToString()));

app.Run();


/// <summary>
///   This is required for the IStringLocalizer in the global resource lookup.
///   Note that this is different from the auto-generated SharedStrings class in Resources
///   and that the full namespace is AnnotationsDemoApi.Resources.SharedStrings
///   must be used when specifying the ErrorMessageResourceType and the
///   factory.Create() method in the DataAnnotationLocalizerProvider.
///   
///   There may be a better way to do this, but this works for now.
/// </summary>
public class SharedStrings
{
}