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

// Damm Check Digit Endpoints
app.MapPost("/alphanumericmod9710",
   (AlphanumericMod97_10Request request) => Results.Ok("Legal entity identifier is valid"));

app.MapPost("/alphanumericmod9710message",
   (AlphanumericMod97_10RequestCustomErrorMessage request) => Results.Ok("Legal entity identifier is valid"));

app.MapPost("/alphanumericmod9710global",
   (AlphanumericMod97_10RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// Damm Check Digit Endpoints
app.MapPost("/damm",
   (DammRequest request) => Results.Ok("Submission identifier is valid"));

app.MapPost("/dammmessage",
   (DammRequestCustomErrorMessage request) => Results.Ok("Submission identifier is valid"));

app.MapPost("/dammglobal",
   (DammRequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// ISO/IEC 7064 MOD 11,10 Check Digit Endpoints
app.MapPost("/iso7064mod1110",
   (Iso7064Mod11_10Request request) => Results.Ok("Item identifier is valid"));

app.MapPost("/iso7064mod1110message",
   (Iso7064Mod11_10RequestCustomErrorMessage request) => Results.Ok("Item identifier is valid"));

app.MapPost("/iso7064mod1110global",
   (Iso7064Mod11_10RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// ISO/IEC 7064 MOD 11-2 Check Digit Endpoints
app.MapPost("/iso7064mod112",
   (Iso7064Mod11_2Request request) => Results.Ok("Name identifier is valid"));

app.MapPost("/iso7064mod112message",
   (Iso7064Mod11_2RequestCustomErrorMessage request) => Results.Ok("Name identifier is valid"));

app.MapPost("/iso7064mod112global",
   (Iso7064Mod11_2RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// ISO/IEC 7064 MOD 1271-36 Check Digit Endpoints
app.MapPost("/iso7064mod127136",
   (Iso7064Mod1271_36Request request) => Results.Ok("ISO 7065 MOD 1271-36 Identifier is valid"));

app.MapPost("/iso7064mod127136message",
   (Iso7064Mod1271_36RequestCustomErrorMessage request) => Results.Ok("ISO 7065 MOD 1271-36 Identifier is valid"));

app.MapPost("/iso7064mod127136global",
   (Iso7064Mod1271_36RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// ISO/IEC 7064 MOD 27,26 Check Digit Endpoints
app.MapPost("/iso7064mod2726",
   (Iso7064Mod27_26Request request) => Results.Ok("ISO 7065 MOD 27,26 Identifier is valid"));

app.MapPost("/iso7064mod2726message",
   (Iso7064Mod27_26RequestCustomErrorMessage request) => Results.Ok("ISO 7065 MOD 27,26 Identifier is valid"));

app.MapPost("/iso7064mod2726global",
   (Iso7064Mod27_26RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// Luhn Check Digit Endpoints
app.MapPost("/luhn",
   (LuhnRequest request) => Results.Ok("Credit card number is valid"));

app.MapPost("/luhnmessage",
   (LuhnRequestCustomErrorMessage request) => Results.Ok("Credit card number is valid"));

app.MapPost("/luhnglobal",
   (LuhnRequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
   => Results.Ok(localizer["ValidRequest"].ToString()));

// Modulus 10_13 Check Digit Endpoints
app.MapPost("/modulus1013",
   (Modulus10_13Request request) => Results.Ok("UPC code is valid"));

app.MapPost("/modulus1013message",
   (Modulus10_13RequestCustomErrorMessage request) => Results.Ok("UPC code is valid"));

app.MapPost("/modulus1013global",
   (Modulus10_13RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
   => Results.Ok(localizer["ValidRequest"].ToString()));

// Modulus 10_1 Check Digit Endpoints
app.MapPost("/modulus101",
   (Modulus10_1Request request) => Results.Ok("CAS Registry Number is valid"));

app.MapPost("/modulus101message",
   (Modulus10_1RequestCustomErrorMessage request) => Results.Ok("CAS Registry Number is valid"));

app.MapPost("/modulus101global",
   (Modulus10_1RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// Modulus 10_2 Check Digit Endpoints
app.MapPost("/modulus102",
   (Modulus10_2Request request) => Results.Ok("IMO Number is valid"));

app.MapPost("/modulus102message",
   (Modulus10_2RequestCustomErrorMessage request) => Results.Ok("IMO Number is valid"));

app.MapPost("/modulus102global",
   (Modulus10_2RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
      => Results.Ok(localizer["ValidRequest"].ToString()));

// Modulus 11 Check Digit Endpoints
app.MapPost("/modulus11",
   (Modulus11Request request) => Results.Ok("ISBN is valid"));

app.MapPost("/modulus11message",
   (Modulus11RequestCustomErrorMessage request) => Results.Ok("ISBN is valid"));

app.MapPost("/modulus11global",
   (Modulus11RequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
   => Results.Ok(localizer["ValidRequest"].ToString()));

// NOID Check Digit Endpoints
app.MapPost("/noid",
   (NoidRequest request) => Results.Ok("ARK Identifier is valid"));

app.MapPost("/noidmessage",
   (NoidRequestCustomErrorMessage request) => Results.Ok("ARK Identifier number is valid"));

app.MapPost("/noidglobal",
   (NoidRequestGlobalizedErrorMessage request, IStringLocalizer<SharedStrings> localizer)
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