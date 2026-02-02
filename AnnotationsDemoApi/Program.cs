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

// Modulus 10_13 Check Digit Endpoints
app.MapPost("/modulus1013",
   (Modulus10_13Request request) => Results.Ok("UPC code is valid"));

app.MapPost("/modulus1013message",
   (Modulus10_13RequestCustomErrorMessage request) => Results.Ok("UPC code is valid"));

app.MapPost("/modulus1013global",
   (Modulus10_13RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
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